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

        public Game game;       
        int listenerPortNumber = 5000;
        public string serverIp;
        public int serverPort = 4242;
        public Socket gmSocketGlobal;

        public void launch()
        {
            serverIp = GetIP4Address();
            // set threadpool initial params
            ThreadPool.SetMinThreads(3, 3);
            ThreadPool.SetMaxThreads(10, 10);

            MakeGame("..\\..\\GameSettings\\XMLgameSettings1.xml");
            gmSocketGlobal = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            RegisterGame();

            // Start listening for Game related connections (assumes game was registered)
            Console.WriteLine("GM started listening...");
            //TcpListener listener = new TcpListener(IPAddress.Parse(GetIP4Address()), listenerPortNumber);
            //listener.Start();
           
            while(true)
            {
                //gmSocketGlobal.Listen(0);
                // Gets socket, launches thread with it as param and HandleRequest which deserializes it
                //Socket newConnectionSocket = listener.AcceptSocket();
                // Socket newConnectionSocket = gmSocketGlobal.Accept();
                Console.WriteLine("Listening in while...");
                byte[] bufferIn = new byte[gmSocketGlobal.SendBufferSize];
                int readbytes = gmSocketGlobal.Receive(bufferIn); // blocks untill there's something to read
                ThreadPool.QueueUserWorkItem(HandleRequest,bufferIn);
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
           
        }

        public void RegisterGame()
        {
            // Registers game to the CS
            string registrationXml = MakeGameCreationMessage();
            //Socket gmSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          //  gmSocketGlobal = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] buffer = new byte[gmSocketGlobal.SendBufferSize];
            buffer = Encoding.ASCII.GetBytes(registrationXml);

            IPEndPoint localIp = new IPEndPoint(IPAddress.Parse(GetIP4Address()), listenerPortNumber); // thats local port
            IPEndPoint remote = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            //gmSock.Bind(localIp);
            //gmSock.Connect(remote);
            //gmSock.Send(buffer);
            gmSocketGlobal.Bind(localIp);
            gmSocketGlobal.Connect(remote);
            gmSocketGlobal.Send(buffer);

            while (true)
            {
                // read single response message
                //byte[] bufferIn = new byte[gmSock.SendBufferSize];
                //int readbytes = gmSock.Receive(bufferIn); 
                byte[] bufferIn = new byte[gmSocketGlobal.SendBufferSize];
                int readbytes = gmSocketGlobal.Receive(bufferIn);


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
            //gmSock.Disconnect(false);
            //gmSock.Close();
        }

        public void HandleRequest(object bufferIn)
        {
            
            byte[] buffer = new byte[gmSocketGlobal.SendBufferSize];
            buffer = (byte[])bufferIn;
            try
            {
                //buffer = new byte[gmSocketGlobal.SendBufferSize];
               // readBytes = gmSocketGlobal.Receive(buffer);

               // if (readBytes > 0) // assume all is read at once
               // {
                string messageString = Encoding.ASCII.GetString(buffer);
                // Deserialize message content   
                Message tempMessage = MessageProject.Message.xmlIntoMessage(messageString);
                Type typer = tempMessage.GetType();
                dynamic newMessage = Convert.ChangeType(tempMessage, typer);
                Console.WriteLine("Received message of type: {0}, from remote end point: {1}", newMessage.GetType(), gmSocketGlobal.RemoteEndPoint);
                // Selecting action on message type
                buffer = new byte[gmSocketGlobal.SendBufferSize]; // flush?

                switch(newMessage) // c# 7.0 -> switch on type
                {
                    case Move msg1:
                        Console.WriteLine("Parsing game movement request!");
                        Tuple <int, int> coordinatesMsg1 = game.HandleMoveRequest(msg1.playerID, msg1.direction); // Y, X either new or old depending on action result
                        string response1 = MakeMoveResponse(msg1.playerID, coordinatesMsg1);
                        buffer = Encoding.ASCII.GetBytes(response1);
                        gmSocketGlobal.Send(buffer);
                        break;

                    case JoinGame msg2:
                        Console.WriteLine("Parsing game join request!");
                        // player can join only a agame he sees, hence gameId = 1
                        Tuple<int, MessageProject.Team, MessageProject.Role, bool> newPlayer = game.MakePlayer(msg2.preferredRole, msg2.preferredTeam, msg2.playerID);
                        string response2 = MakeJoinGameResponse(msg2.playerID, newPlayer); // assume playerID is always correct
                        Console.WriteLine("Response being sent: {0}", response2);
                        buffer = Encoding.ASCII.GetBytes(response2);
                        gmSocketGlobal.Send(buffer);

                        //After reject/ confirm was sent, verify if the game has just been started(code - 999 as player ID)
                        if (newPlayer.Item4)
                        {
                            // Game just started!
                            Console.WriteLine("Game starts!");
                            ThreadPool.QueueUserWorkItem(StartGame, 100); // random object passed
                        }
                        break;

                }


              //  }
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

        private string MakeJoinGameResponse(int playerID, Tuple<int, MessageProject.Team, MessageProject.Role, bool> newPlayer)
        {
            if(null == newPlayer || newPlayer.Item1 < 0)
            {
                // or null
                // negative value as newPlayerId -> failure of player making, teams full
                RejectJoiningGame responseObj = new RejectJoiningGame(game.gameId);
                responseObj.playerID = playerID;
                return MessageProject.Message.messageIntoXML(responseObj);
            }
            else //if(newPlayer.Item1 >= 0)
            {
                ConfirmJoiningGame responseObj = new ConfirmJoiningGame(game.gameId, newPlayer.Item1, new MessageProject.Player(playerID, newPlayer.Item2, newPlayer.Item3));
                return MessageProject.Message.messageIntoXML(responseObj);
            }
            
        }

        private void StartGame(object smth)
        {
            lock(game.playersDictionary)
            {
                lock (game.board)
                {
                    Random rand = new Random();
                    List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>(); // Y, X pairs

                    for (int y = game.settings.GoalLen; y < game.settings.TaskLen + game.settings.GoalLen; y++)
                    {
                        for (int x = 0; x < game.settings.BoardWidth; x++)
                        {
                            Tuple<int, int> newCoord = new Tuple<int, int>(y, x);
                            coordinates.Add(newCoord);
                        }
                    }
                    // Fix players positions
                    foreach(int i in game.playersDictionary.Keys)
                    {
                        int indexToPop = GetRandomValue(coordinates.Count(), rand);
                        Tuple<int, int> randCoordinate = coordinates.ElementAt(indexToPop);
                        coordinates.RemoveAt(indexToPop);
                        Console.WriteLine("Coordinate picked: Y={0}, X={1}", randCoordinate.Item1, randCoordinate.Item2);
                        game.playersDictionary[i].posX = randCoordinate.Item2;
                        game.playersDictionary[i].posY = randCoordinate.Item1;
                        game.board[randCoordinate.Item1, randCoordinate.Item2].playerID = i; // assign players ID to board field
                    }

                    List<MessageProject.Player> formattedPlayersList = new List<MessageProject.Player>();

                    foreach (int j in game.playersDictionary.Keys)
                    {
                        Console.WriteLine("Player check: ID: {0}, posY: {1}, posX: {2}, team: {3}, role: {4}", j ,game.playersDictionary[j].posY, game.playersDictionary[j].posX, game.playersDictionary[j].team, game.playersDictionary[j].role);
                        Console.WriteLine("Associated field check: at pos {0}, {1}, playerID: {2}", game.playersDictionary[j].posY, game.playersDictionary[j].posX, game.board[game.playersDictionary[j].posY, game.playersDictionary[j].posX].playerID);
                        MessageProject.Player nextPlayer = new MessageProject.Player(j, game.playersDictionary[j].team, game.playersDictionary[j].role);
                        formattedPlayersList.Add(nextPlayer);
                    }
                    // Make all players positions, fields & goals

                    // Send the same message to all players
                    BroadcastGameMessage(formattedPlayersList);


                    lock (game.gameState)
                    {
                        game.gameState.gameStarted = true;
                    }

                }
            }
            Console.WriteLine("Game start finished!");
        }

        private static int GetRandomValue(int range, Random random)
        {
            return random.Next(range); // <0, range)
        }

        private void BroadcastGameMessage(List<MessageProject.Player> formattedPlayersList)
        {
            foreach(int playerId in game.playersDictionary.Keys)
            {
                string message = MakeGameMessage(playerId, game.playersDictionary[playerId].posY, game.playersDictionary[playerId].posX, game.playersDictionary.Keys.ToList(), formattedPlayersList);
                Console.WriteLine("Message created: {0}", message);
                byte[] buffer = new byte[gmSocketGlobal.SendBufferSize];
                buffer = Encoding.ASCII.GetBytes(message);
                gmSocketGlobal.Send(buffer);
            }
        }

        private string MakeGameMessage(int playerId, int posY, int posX, List<int> playerKeys, List<MessageProject.Player> formattedPlayersList)
        {
            GameMessage message = new GameMessage(playerId, formattedPlayersList, new MessageProject.Board(game.settings.BoardWidth, game.settings.GoalLen, game.settings.TaskLen), new PlayerLocation(posX, posY) );
            return MessageProject.Message.messageIntoXML(message);
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