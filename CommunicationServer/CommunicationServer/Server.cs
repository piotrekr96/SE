using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MessageProject;

namespace CommunicationServer
{
    class Server
    {
        static Dictionary<Type, int> typeDict = new Dictionary<Type, int>
        {
            {typeof(ConfirmGameRegistration),0},
            {typeof(ConfirmJoiningGame),1},
            {typeof(RejectJoiningGame),2},
            {typeof(JoinGame),3},
            {typeof(GameMessage),4},           
            {typeof(RegisterGame),5},
            {typeof(GetGamesList),6},
            {typeof(RegisteredGames),7},
            {typeof(Move),8 },
            {typeof(MoveResponse),9 }
        };

        static int gmID, counter = 0;
        static Socket listenerSocket;
        static List<GameInfo> gameList = new List<GameInfo>();
        static List<ClientData> clients;
        static Socket gmSocket;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting server on " + GetIP4Address());
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<ClientData>();

            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(GetIP4Address()), 4242);
            listenerSocket.Bind(ip);

            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();
        }

        static void ListenThread()
        {
            while (true)
            {
                listenerSocket.Listen(0);
                clients.Add(new ClientData(listenerSocket.Accept()));
            }
        }

        public static void DataIn(object clientData)
        {
            Console.Write("Connection Established \n");

            ClientData client = (ClientData)clientData;
            Socket clientSocket = client.clientSocket;

            byte[] buffer;
            int readBytes;

            while (true)
            {
                try
                {
                    buffer = new byte[clientSocket.SendBufferSize];
                    readBytes = clientSocket.Receive(buffer);

                    if (readBytes > 0)
                    {
                        string xml = Encoding.ASCII.GetString(buffer);
                        DataManager(xml, client);
                    }
                }

                catch
                {

                }
                
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

        public static void DataManager(string xml, ClientData client)
        {
            Message temp = Message.xmlIntoMessage(xml);
            dynamic msg = Convert.ChangeType(temp, temp.GetType());

            switch (typeDict[msg.GetType()])
            {
                case 0:
                    
                    break;

                case 1:

                    foreach(ClientData c in clients)
                    {
                        if(c.id == msg.playerID)
                        {
                            string confirmJoining = Message.messageIntoXML(msg);
                            byte[] sendConfirmJoining = Encoding.ASCII.GetBytes(confirmJoining);
                            c.clientSocket.Send(sendConfirmJoining);
                        }
                    }

                    break;

                case 2:
                    foreach (ClientData c in clients)
                    {
                        if (c.id == msg.playerID)
                        {
                            string reject = Message.messageIntoXML(msg);
                            byte[] sendReject = Encoding.ASCII.GetBytes(reject);
                            c.clientSocket.Send(sendReject);
                        }
                    }
                    break;

                case 3:
               
                    client.id = counter;
                    msg.playerID = counter;
                    counter++;

                    string join = Message.messageIntoXML(msg);
                    byte[] sendJoin = Encoding.ASCII.GetBytes(join);
                    try
                    {
                        gmSocket.Send(sendJoin);
                    }
                    catch(Exception e)
                    {
                        Console.Write(e);
                    }


                    break;

                case 4:
                    string game = Message.messageIntoXML(msg);
                    byte[] sendGame = Encoding.ASCII.GetBytes(game);
                    Console.WriteLine();
                    foreach (ClientData c in clients)
                    {
                        if (c.id == msg.playerID)
                        {
                            Console.WriteLine("Received GameMessage! Sending to playerID: {0}", msg.playerID);
                            c.clientSocket.Send(sendGame);
                        }
                    }

                    break;
                case 5:
                    GameInfo info = new GameInfo(msg.gameID, msg.blueTeamPlayers, msg.redTeamPlayers);
                    gameList.Add(info);
                    int id = msg.gameID;
                    gmID = counter;
                    client.id = counter;
                    gmSocket = client.clientSocket;
                    counter++;

                    ConfirmGameRegistration confirmation = new ConfirmGameRegistration(id);
                    string conf = Message.messageIntoXML(confirmation);

                    byte[] toSend = Encoding.ASCII.GetBytes(conf);
                    client.clientSocket.Send(toSend);
                    break;

                case 6:
                    RegisteredGames registered = new RegisteredGames(gameList);
                    string registeredSend = Message.messageIntoXML(registered);

                    byte[] registeredToSend = Encoding.ASCII.GetBytes(registeredSend);
                    client.clientSocket.Send(registeredToSend);
                    break;

                case 8:
                    string move = Message.messageIntoXML(msg);
                    byte[] sendMove = Encoding.ASCII.GetBytes(move);
                    try
                    {
                        gmSocket.Send(sendMove);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e);
                    }
                    break;

                case 9:
                    Console.WriteLine("Response from GM");
                    foreach (ClientData c in clients)
                    {
                        if (c.id == msg.playerID)
                        {
                            string movementResponse = Message.messageIntoXML(msg);
                            byte[] sendmovementResponse = Encoding.ASCII.GetBytes(movementResponse);
                            c.clientSocket.Send(sendmovementResponse);
                        }
                    }
                    break;

                default:
                    Console.Write("dostalem cos");
                    byte[] sendEmpty = Encoding.ASCII.GetBytes("");
                    client.clientSocket.Send(sendEmpty);
                    break;
            }

        }
       
    }

    class ClientData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public int id;

        public ClientData()
        {
            // id = Guid.NewGuid();
            clientThread = new Thread(Server.DataIn);
            clientThread.Start(this);
        }
        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            //id = Guid.NewGuid();
            clientThread = new Thread(Server.DataIn);
            clientThread.Start(this);
        }


    }
}
