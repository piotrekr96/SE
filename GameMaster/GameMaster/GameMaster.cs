using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using MessageProject;

namespace GM
{
    class GameMaster
    {
        private class GameState
        {
            public bool gameStarted;
            public bool gameEnded;
        }

        public Game game;       
        int listenerPortNumber = 5000;
        public string serverIp;
        public int serverPort = 5100;
        GameState gameState;

        public void launch()
        {
            serverIp = GetIP4Address();
            // set threadpool initial params
            ThreadPool.SetMinThreads(3, 3);
            ThreadPool.SetMaxThreads(10, 10);

            MakeGame("..\\..\\GameSettings\\XMLgameSettings1.xml");

            RegisterGame();

            // Start listening for Game related connections (assumes game was registered)
            Console.WriteLine("GM started listening...");
            TcpListener listener = new TcpListener(IPAddress.Parse(GetIP4Address()), listenerPortNumber);
            listener.Start();
            while(true)
            {
                // Gets socket, launches thread with it as param and HandleRequest which deserializes it
                Socket newConnectionSocket = listener.AcceptSocket();
                ThreadPool.QueueUserWorkItem(HandleRequest, newConnectionSocket);
            }
        }

        public static string GetIP4Address()
        {
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());

            foreach (IPAddress i in ips)
            {
                if (i.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) return i.ToString();
            }

            return "";
        }

        public void MakeGame(string path)
        {
            // Responsible for initializing the only game that GM can have with passed config file and ID 1


            // Only 1 game at once
            game = new Game(ReadGameInfo(path));
            Console.WriteLine(game.settings.ToString());

            int newGameID = 1;
            game.gameId = newGameID;

            gameState = new GameState();
            gameState.gameStarted = false;
            gameState.gameEnded = false;
           
        }

        public void RegisterGame()
        {
            // Registers game to the CS
            string registrationXml = MakeGameCreationMessage();
            Socket gmSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] buffer = new byte[gmSock.SendBufferSize];
            buffer = Encoding.ASCII.GetBytes(registrationXml);

            IPEndPoint localIp = new IPEndPoint(IPAddress.Parse(GetIP4Address()), listenerPortNumber); // thats local port
            IPEndPoint remote = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            gmSock.Bind(localIp);
            gmSock.Connect(remote);
            gmSock.Send(buffer);

            while (true)
            {
                // read single response message
                byte[] bufferIn = new byte[gmSock.SendBufferSize];
                int readbytes = gmSock.Receive(bufferIn); 


                if (readbytes > 0)
                {
                    string messageString = Encoding.ASCII.GetString(bufferIn);
                    Message tempMessage = MessageProject.Message.xmlIntoMessage(messageString);
                    Type typer = tempMessage.GetType();
                    dynamic newMessage = Convert.ChangeType(tempMessage, typer);

                    if(newMessage.GetType().ToString().Equals("MessageProject.ConfirmGameRegistration"))
                    {
                        Console.WriteLine("Response message type correct! game registered with id: {0}", ((ConfirmGameRegistration)newMessage).gameID);
                    }
                    else
                    {
                        Console.WriteLine("Message received(response): {0}", messageString);
                        Console.WriteLine(newMessage.GetType().ToString());
                        Console.WriteLine("Response type incompatible!");
                    }

                    break;
                }
            }
            // The same port and IP will be reused in main listener
            gmSock.Disconnect(false);
            gmSock.Close();
        }

        public void HandleRequest(object cs)
        {
            // Already in new thread, ThreadStart starting mathed here !!!
            // Deserialize message
            // If message P2P - handle it here, else delegate to specific game.handleMoveReq/...
            // Decide upon move/pick/drop - message type
            // formulate response, type defined by message type from input and return values
            Socket clientSocket = (Socket)cs;
            byte[] buffer;
            int readBytes;
            try
            {
                buffer = new byte[clientSocket.SendBufferSize];
                readBytes = clientSocket.Receive(buffer);

                if (readBytes > 0) // assume all is read at once
                {
                    string messageString = Encoding.ASCII.GetString(buffer);


                    // get message from socket

                    Message tempMessage = MessageProject.Message.xmlIntoMessage(messageString);
                    Type typer = tempMessage.GetType();
                    dynamic newMessage = Convert.ChangeType(tempMessage, typer);
                    // Selecting action on message type
                    int gameID = -1, playerID = -1;
                    switch(newMessage) // c# 7.0 -> switch on type
                    {
                        case Move msg1:
                            gameID = msg1.gameID;
                            // Tuple <int, int> coordinatesMsg1 = gamesDictionary[gameID].HandleMoveRequest(msg1.playerID, (int)msg1.direction); // int by enum assigned values, internal switch
                            // string response1 = MakeMoveResponse(msg1.playerID, coordinatesMsg1);
                            // send response
                            break;
                        case JoinGame msg2:
                            gameID = msg2.gameID;
                            // Tuple<int, string> newPlayer = gamesDictionary[msg2.gameID].MakePlayer((msg2.preferredTeam.ToString()));
                            // string response2 = MakeJoinGameResponse(newPlayer);
                            break;

                    }


                }
            }

            catch
            {

            }
        }

        private string MakeMoveResponse(int playerID, Tuple<int, int> coordinates)
        {
            MoveResponse responseObj = new MoveResponse(playerID, new PlayerLocation(coordinates.Item2, coordinates.Item1)); // tupple in array format Y, X
            return MessageProject.Message.messageIntoXML(responseObj);

        }

        private string MakeGameCreationMessage()
        {
            // for main game only
            RegisterGame responseObj = new RegisterGame(game.gameId, game.settings.PlayersPerTeam, game.settings.PlayersPerTeam);
            // The same ammount of players in both teams atm!
            return MessageProject.Message.messageIntoXML(responseObj);
        }

        private string MakeJoinGameResponse(Tuple<int, string> newPlayer)
        {
            
            return "";
        }

        static public DataGame ReadGameInfo(string fileName)
        {
            // Deserializes XML with game config
            XmlDocument doc = new XmlDocument();
            
            doc.Load(fileName);

            XmlElement root = doc.DocumentElement;
            DataGame settings = new DataGame();
            settings.Name = root.SelectSingleNode("name").InnerXml;
            settings.BoardWidth = Int32.Parse(root.SelectSingleNode("board_width").InnerXml);
            settings.TaskLen = Int32.Parse(root.SelectSingleNode("task_len").InnerXml);
            settings.GoalLen = Int32.Parse(root.SelectSingleNode("goal_len").InnerXml);
            settings.InitialPieces = Int32.Parse(root.SelectSingleNode("initial_pieces").InnerXml);
            settings.PlayersPerTeam = Int32.Parse(root.SelectSingleNode("players_per_team").InnerXml);

            settings.DelayMove = Int64.Parse(root.SelectSingleNode("delay_move").InnerXml);
            settings.DelayPick = Int64.Parse(root.SelectSingleNode("delay_pick").InnerXml);
            settings.DelayDrop = Int64.Parse(root.SelectSingleNode("delay_drop").InnerXml);

            return settings;
        }

        



    }

}