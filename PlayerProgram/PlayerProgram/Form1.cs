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

namespace PlayerProgram
{
    public partial class Form1 : Form
    {
        RegisteredGames reg;
        public Form1()
        {
            InitializeComponent();
        }

        private void GetGamesButton_Click(object sender, EventArgs e)
        {
            GetGames getgames = new GetGames();
            string XMLmessage = messageIntoXML(getgames);
            // SERVER STUFF

            List<GameInfo> newList = new List<GameInfo>();
            newList.Add(new GameInfo("Game 1", 5, 5));
            newList.Add(new GameInfo("Game 2", 10, 10));
            newList.Add(new GameInfo("Game 3", 3, 3));
            newList.Add(new GameInfo("Game 4", 2, 8));

            reg = new RegisteredGames(newList);

            foreach (GameInfo gameinfo in reg.gameInfoList)
            {
                string s = "Name: " + gameinfo.name + " Blue: " + gameinfo.blueTeamPlayers + " Red: " + gameinfo.redTeamPlayers.ToString();
                GetGamesBOX.Items.Add(s);
            }
        }

        public string messageIntoXML(MessageProject.Message mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(MessageProject.Message), new Type[] {typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGames),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame)});
            //XmlSerializer xmlSerial = new XmlSerializer(mess.GetType());

            StringWriter textWriter = new StringWriter();
            xmlSerial.Serialize(textWriter, mess);

            return textWriter.ToString();
        }

        public MessageProject.Message xmlIntoMessage(String mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(MessageProject.Message), new Type[] {typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGames),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame)});

            StringReader textReader = new StringReader(mess);
            MessageProject.Message message = (MessageProject.Message)xmlSerial.Deserialize(textReader);

            return message;
        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            int index = GetGamesBOX.SelectedIndex;
            JoinGame joingame = new JoinGame(reg.gameInfoList[index].name,Role.leader,Team.red);
            string newXMLmessage = messageIntoXML(joingame);
            System.Diagnostics.Debug.WriteLine(newXMLmessage);

            // SERVER STUFF

            // if(confirm)
            //.... 
            MessageProject.Player newPlayer = new MessageProject.Player(23, Team.red, Role.member);
            ConfirmJoiningGame confirmation = new ConfirmJoiningGame(1, 23, Guid.NewGuid(), newPlayer);

            Player p = new Player(confirmation.player.team, confirmation.playerID, confirmation.gameID, confirmation.privateGUID, confirmation.player.role);
            // BoardView1 boardView1 = new BoardView1(); 
            List<MessageProject.Player> playerList = new List<MessageProject.Player>();
            playerList.Add(newPlayer);
            Board board = new Board(12, 3, 5);
            PlayerLocation pl = new PlayerLocation(0, 0);
            //else 
            GameMessage gamemessage = new GameMessage(p.ID, playerList, board, pl);

            BoardView1 boardview = new BoardView1(gamemessage.board.width, gamemessage.board.taskAreaHeight, gamemessage.board.goalAreaHeight);
            this.Hide();
            boardview.Show();

        }
    }
}
