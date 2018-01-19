using System;
using System.Xml;
using Messages;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace GM
{
    class GameMaster
    {
        public Game game; // later into list / dic with ID's
        // public only for now, later Game object actions will be handled inside by threads
        // If multiple games will be handled - thread start method must be here sincce game ID has to be extracted prior to delegating to specific game object
        public Dictionary<int, Game> gamesDictionary;

        public void launch()
        {
            // set threadpool initial params
            TcpListener listener = new TcpListener(IPAddress.Parse(GetIP4Address()), 4240);
            listener.Start();
            while(true)
            {
                // Gets socket, aunches thread with it as param and HandleRequest
                // read until delimiter
                Socket s = listener.AcceptSocket();
                ThreadPool.QueueUserWorkItem(HandleRequest, s);

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
            Socket csSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(GetIP4Address()), 4242);
            csSocket.Bind(ip);

            Game newGame = new Game(ReadGameinfo(path));
            Console.WriteLine(game.settings.ToString());
            int newGameID = gamesDictionary.Count + 1; // Possibly predefined gameid's ?
            gamesDictionary.Add(newGameID, newGame);

            string makeGameResponse = MakeGameCreationResponse(newGameID);

            byte[] toBytes = Encoding.ASCII.GetBytes(makeGameResponse);

            csSocket.Send(toBytes);
            // send 


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

                    Message tempMessage = Messages.Message.xmlIntoMessage(messageString);
                    Type typer = tempMessage.GetType();
                    dynamic newMessage = Convert.ChangeType(tempMessage, typer);
                    // Selecting action on message type
                    int gameID = -1, playerID = -1;
                    switch(newMessage) // c# 7.0 -> switch on type
                    {
                        case Move msg1:
                            gameID = msg1.gameID;
                            Tuple <int, int> coordinatesMsg1 = gamesDictionary[gameID].HandleMoveRequest(msg1.playerID, (int)msg1.direction); // int by enum assigned values, internal switch
                            string response1 = MakeMoveResponse(msg1.playerID, coordinatesMsg1);
                            // send response
                            break;
                        case JoinGame msg2:
                            gameID = msg2.gameID;
                            Tuple<int> newPlayerID = gamesDictionary[msg2.gameID].MakePlayer((msg2.preferredTeam.ToString()));
                            string response2 = MakeJoinGameResponse(newPlayerID);
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
            return Messages.Message.messageIntoXML(responseObj);
        }

        private string MakeGameCreationResponse(int gameID)
        {
            RegisterGame responseObj = new RegisterGame(gamesDictionary[gameID].gameName, gamesDictionary[gameID].settings.PlayersPerTeam, gamesDictionary[gameID].settings.PlayersPerTeam);
            // The same ammount of players in both teams atm!
            return Messages.Message.messageIntoXML(responseObj);
        }

        private string MakeJoinGameResponse(Tuple<int> playerID)
        {
            return "";
        }

        static public DataGame ReadGameinfo(string fileName)
        {
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