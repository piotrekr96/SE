using MessageProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace PlayerProgram
{
    public partial class Form1 : Form
    {
        static Dictionary<Type, int> typeDict = new Dictionary<Type, int>
        {
            {typeof(ConfirmGameRegistration),0},
            {typeof(ConfirmJoiningGame),1},
            {typeof(RejectJoiningGame),2},
            {typeof(JoinGame),3},
            {typeof(GameMessage),4},
            {typeof(RegisteredGames),5},
            {typeof(GetGamesList),6},
            {typeof(RegisterGame),7}
        };

        int gameID, playerID;
        Team playerTeam;
        Role playerRole;

        public static Socket master;
        RegisteredGames reg;
        public Form1()
        {
            InitializeComponent();
        }

        private void GetGamesButton_Click(object sender, EventArgs e)
        {
            GetGamesList getgames = new GetGamesList();
            string XMLmessage = MessageProject.Message.messageIntoXML(getgames);
            // SERVER STUFF

            List<GameInfo> newList = new List<GameInfo>();
          /*  newList.Add(new GameInfo("Game 1", 5, 5));
            newList.Add(new GameInfo("Game 2", 10, 10));
            newList.Add(new GameInfo("Game 3", 3, 3));
            newList.Add(new GameInfo("Game 4", 2, 8));
            */
            reg = new RegisteredGames(newList);

            foreach (GameInfo gameinfo in reg.gameInfoList)
            {
                string s = "ID: " + gameinfo.gameID + " Blue: " + gameinfo.blueTeamPlayers + " Red: " + gameinfo.redTeamPlayers.ToString();
                GetGamesBOX.Items.Add(s);
            }
        }


        private void JoinButton_Click(object sender, EventArgs e)
        {
           // int index = GetGamesBOX.SelectedIndex;
            JoinGame joingame = new JoinGame(2,Role.leader,Team.red);
            string newXMLmessage = MessageProject.Message.messageIntoXML(joingame);
          //  System.Diagnostics.Debug.WriteLine(newXMLmessage);

            // SERVER STUFF
            A: Console.Clear();
            Console.Write("Enter host IP address: ");
            string ip = Console.ReadLine();

            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), 4242);

            try
            {
                master.Connect(ipe);
            }
            catch
            {
                Console.WriteLine("Could not connect");
                Thread.Sleep(1000);
                goto A;
            }

            Console.Clear();

            Thread t = new Thread(DataIn);
            t.Start();



            byte[] toBytes = Encoding.ASCII.GetBytes(newXMLmessage);

            master.Send(toBytes);

            
    

            

            // if(confirm)
            //.... 
           /* MessageProject.Player newPlayer = new MessageProject.Player(23, Team.red, Role.member);
            ConfirmJoiningGame confirmation = new ConfirmJoiningGame(1, 23, Guid.NewGuid(), newPlayer);

            Player p = new Player(confirmation.player.team, confirmation.playerID, confirmation.gameID, confirmation.privateGUID, confirmation.player.role);
            // BoardView1 boardView1 = new BoardView1(); 
            List<MessageProject.Player> playerList = new List<MessageProject.Player>();
            playerList.Add(newPlayer);
            Board board = new Board(12, 3, 5);
            PlayerLocation pl = new PlayerLocation(0, 0);
            //else 

            GameMessage gamemessage = new GameMessage(p.ID, playerList, board, pl);

            BoardView1 boardview = new BoardView1(gamemessage.board.width, gamemessage.board.taskAreaHeight, gamemessage.board.goalAreaHeight, p);
            this.Hide();
            boardview.Show();*/

        }


        void DataIn()
        {
            byte[] buffer;
            int readBytes;

            while (true)
            {
                try
                {


                    buffer = new byte[master.ReceiveBufferSize];
                    readBytes = master.Receive(buffer);

                    if (readBytes > 0)
                    {

                        string xml = Encoding.ASCII.GetString(buffer);
                        DataManager(xml);                       
                    }
                }
                catch
                {

                }
            }
        }

        private void DataManager(string xml)
        {
            MessageProject.Message temp = MessageProject.Message.xmlIntoMessage(xml);
            dynamic msg = Convert.ChangeType(temp, temp.GetType());

            switch (typeDict[msg.GetType()])
            {
                case 0:

                    break;
                case 1:

                    playerID = msg.playerID;
                    gameID = msg.gameID;
                    try
                    {
                        playerRole = msg.player.role;
                        playerTeam = msg.player.team;
                    }
                    catch
                    {
                        Console.Write("Fuck you");
                    }

                    System.Diagnostics.Debug.Write("Player id " + playerID + " gameID " + gameID + "Player role: " + playerRole + " PLayer team: " + playerTeam);
                    break;
                case 2:

                    break;
                default:
                    break;
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
