using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MessageProject;

namespace SEtestServer
{
    class Server
    {
        public Socket listener;
        public void run()
        {
            
            // start pooling workers, each receives new socket, the old is listening
            // each new thread prints all seen inside (size 100)
            // and wait 10k ms
            // eventualy print connection info

            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(GetIP4Address()), 5100); // thats local port
            listener.Bind(ip);
            listener.Listen(0);

            ThreadPool.QueueUserWorkItem(SendMsgToGm, 100);
            ThreadPool.QueueUserWorkItem(SendMsgToGm, 100);
            ThreadPool.QueueUserWorkItem(SendMsgToGm, 100);
            ThreadPool.QueueUserWorkItem(SendMsgToGm, 100);
            ThreadPool.QueueUserWorkItem(SendMsgToGm, 100);


            while (true)
            {
                Socket newsock = listener.Accept();
                ThreadPool.QueueUserWorkItem(HandleRequest, newsock);
            }


        }

        public void HandleRequest(object insock)
        {
            Socket s = (Socket)insock;
            byte[] buffer;
            int readBytes;

            buffer = new byte[s.SendBufferSize];
            readBytes = s.Receive(buffer);

            if (readBytes > 0) // assume all is read at once
            {
                string messageString = Encoding.ASCII.GetString(buffer);
                //Console.WriteLine("Thread: {0}, ListenerInfo: LEP: {1}, REP: {2}", Thread.CurrentThread.ManagedThreadId, listener.LocalEndPoint, listener.RemoteEndPoint);
                Console.WriteLine("Thread: {0}, LEP: {1}, REP: {2}", Thread.CurrentThread.ManagedThreadId, s.LocalEndPoint, s.RemoteEndPoint);

                Message tempMessage = MessageProject.Message.xmlIntoMessage(messageString);
                Type typer = tempMessage.GetType();
                dynamic newMessage = Convert.ChangeType(tempMessage, typer);
                Console.WriteLine(newMessage.GetType().ToString());

                RegisterGame mes = (RegisterGame)newMessage;
                Console.WriteLine("GameID: {0}, PlayersBlue: {1}, PlayersRed: {2}", mes.gameID, mes.blueTeamPlayers.ToString(), mes.redTeamPlayers.ToString());
                ConfirmGameRegistration gameConfirmation = new ConfirmGameRegistration(mes.gameID);
                string respStr = MessageProject.Message.messageIntoXML(gameConfirmation);
                Console.WriteLine("Response type: {0}", gameConfirmation.GetType());

                byte[] response = Encoding.ASCII.GetBytes(respStr);
                s.Send(response);
            }
            Thread.Sleep(10000);
        }

        public void SendMsgToGm(object smth)
        {
            // send and wait for response
            Thread.Sleep(10000); // 10 sec to register game first
            Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint ipSender = new IPEndPoint(IPAddress.Parse(GetIP4Address()), 5200); // local port used to send to gm
            //sender.Bind(ipSender);
            // pick random local avalable port 

            Console.WriteLine("SendMsgToGM here!");
            IPEndPoint gmIP = new IPEndPoint(IPAddress.Parse(GetIP4Address()), 5000);
            sender.Connect(gmIP);
            JoinGame jg = new JoinGame(1, MessageProject.Role.member, MessageProject.Team.red);
            byte[] buffer = Encoding.ASCII.GetBytes(MessageProject.Message.messageIntoXML(jg));
            Console.WriteLine("Sending msg of type: {0}", jg.GetType());
            sender.Send(buffer);

            // Wait for join confirm / join reject

            byte[] bufferIn;
            int readBytes;

            bufferIn = new byte[sender.SendBufferSize];
            readBytes = sender.Receive(bufferIn); // blocking

            if (readBytes > 0) // assume all is read at once
            {
                string messageString = Encoding.ASCII.GetString(bufferIn);
                //Console.WriteLine("Thread: {0}, ListenerInfo: LEP: {1}, REP: {2}", Thread.CurrentThread.ManagedThreadId, listener.LocalEndPoint, listener.RemoteEndPoint);
                Console.WriteLine("Thread: {0}, LEP: {1}, REP: {2}", Thread.CurrentThread.ManagedThreadId, sender.LocalEndPoint, sender.RemoteEndPoint);

                Message tempMessage = MessageProject.Message.xmlIntoMessage(messageString);
                Type typer = tempMessage.GetType();
                dynamic newMessage = Convert.ChangeType(tempMessage, typer);
                Console.WriteLine(newMessage.GetType().ToString());

                switch(newMessage)
                {
                    case ConfirmJoiningGame msg1:
                        Console.WriteLine("Join confirmation recieved, GameId: {0}, PlayerId:{1}, PlayerRole: {2}, PlayerTeam: {3}", msg1.gameID.ToString(), msg1.playerID.ToString(), msg1.player.role.ToString(), msg1.player.team.ToString());
                        break;
                    case RejectJoiningGame msg2:
                        Console.WriteLine("Join rejected! To gameId: {0}", msg2.gameID);
                        break;
                }
            }

            sender.Disconnect(false);
            sender.Dispose();
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
    }
}
