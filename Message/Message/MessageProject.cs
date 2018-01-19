using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MessageProject
{
    public abstract class Message
    {
        static Type[] messageTypesList = new Type[]{typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGamesList),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame),
            typeof(DropPiece),typeof(DroppingResult),typeof(GetManhattanDistance),typeof(ManhattanResult),typeof(Move),typeof(MoveResponse),
            typeof(PickPiece),typeof(PiecePicked),typeof(TestingResult),typeof(TestPiece)};

        [XmlInclude(typeof(JoinGame))]
        [XmlInclude(typeof(RegisteredGames))]
        [XmlInclude(typeof(ConfirmGameRegistration))]
        [XmlInclude(typeof(ConfirmJoiningGame))]
        [XmlInclude(typeof(GameMessage))]
        [XmlInclude(typeof(GetGamesList))]
        [XmlInclude(typeof(RegisterGame))]
        [XmlInclude(typeof(RejectJoiningGame))]
        [XmlInclude(typeof(DropPiece))]
        [XmlInclude(typeof(DroppingResult))]
        [XmlInclude(typeof(GetManhattanDistance))]
        [XmlInclude(typeof(ManhattanResult))]
        [XmlInclude(typeof(Move))]
        [XmlInclude(typeof(MoveResponse))]
        [XmlInclude(typeof(PickPiece))]
        [XmlInclude(typeof(PiecePicked))]
        [XmlInclude(typeof(TestingResult))]
        [XmlInclude(typeof(TestPiece))]
        //Function messageIntoXML and xmlIntoMessage are supposed to be cutted and pasted into player, master and server project files.

        public static string messageIntoXML(Message mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(Message), messageTypesList);

            StringWriter textWriter = new StringWriter();
            xmlSerial.Serialize(textWriter, mess);

            return textWriter.ToString();
        }

        public static Message xmlIntoMessage(String mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(Message), messageTypesList);

            StringReader textReader = new StringReader(mess);
            Message message = (Message)xmlSerial.Deserialize(textReader);

            return message;
        }
    }

    public enum Role
    {
        member,
        leader
    }

    public enum Team
    {
        blue,
        red
    }

    public enum MovementDirection
    {
        up,
        down,
        left,
        right
    }

    public class GameInfo
    {
        public int gameID { get; set; }
        public int blueTeamPlayers { get; set; }
        public int redTeamPlayers { get; set; }

        public GameInfo() { }

        public GameInfo(int gID, int blue, int red)
        {
            gameID = gID;
            blueTeamPlayers = blue;
            redTeamPlayers = red;
        }
    }

    public class Player
    {
        public int playerID { get; set; }
        public Team team { get; set; }
        public Role role { get; set; }

        public Player() { }

        public Player(int id, Team tea, Role rol)
        {
            playerID = id;
            team = tea;
            role = rol;
        }
    }

    public class Board
    {
        public int width { get; set; }
        public int goalAreaHeight { get; set; }
        public int taskAreaHeight { get; set; }

        public Board() { }

        public Board(int wid, int goalHeight, int taskHeight)
        {
            width = wid;
            goalAreaHeight = goalHeight;
            taskAreaHeight = taskHeight;
        }
    }

    public class PlayerLocation
    {
        public int x { get; set; }
        public int y { get; set; }

        public PlayerLocation() { }

        public PlayerLocation(int xx, int yy)
        {
            x = xx;
            y = yy;
        }
    }

    public class Area
    {
        public int x { get; set; }
        public int y { get; set; }
        public int distance { get; set; }

        public Area() { }

        public Area(int xx, int yy, int dist)
        {
            x = xx;
            y = yy;
            distance = dist;
        }

    }

}
