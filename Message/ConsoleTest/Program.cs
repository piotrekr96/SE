using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageProject;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            JoinGame newJoin = new JoinGame(1, Role.leader, Team.red);
            string information = newJoin.messageIntoXML(newJoin);
            Console.Write(information);
            

            Console.WriteLine();
            Console.WriteLine();

            List<GameInfo> newList = new List<GameInfo>();
            newList.Add(new GameInfo(1, 5, 5));
            newList.Add(new GameInfo(2, 10, 10));
            newList.Add(new GameInfo(3, 3, 3));
            newList.Add(new GameInfo(4, 2, 8));

            RegisteredGames reg = new RegisteredGames(newList);
            Console.Write(reg.messageIntoXML(reg));

            Console.WriteLine();
            Console.WriteLine();

            Player newPlayer = new Player(23, Team.red, Role.member);
            ConfirmJoiningGame confirmation = new ConfirmJoiningGame(1, 23, "blablalbla", newPlayer);
            Console.Write(confirmation.messageIntoXML(confirmation));

            Console.WriteLine();
            Console.WriteLine();

            Message tempMessage = xmlToMessage(information);
            Type typer = tempMessage.GetType();
            dynamic newMessage = Convert.ChangeType(tempMessage, typer);
            
            Console.Write("Game ID: " + newMessage.gameID + " and role: " + newMessage.preferredRole + " and team: " + newMessage.preferredTeam);
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static public Message xmlToMessage(String mess)
        {
            Type[] messageTypesList = new Type[]{typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGamesList),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame),
            typeof(DropPiece),typeof(DroppingResult),typeof(GetManhattanDistance),typeof(ManhattanResult),typeof(Move),typeof(MoveResponse),
            typeof(PickPiece),typeof(PiecePicked),typeof(TestingResult),typeof(TestPiece)};

            XmlSerializer xmlSerial = new XmlSerializer(typeof(Message), messageTypesList);

            StringReader textReader = new StringReader(mess);
            Message message = (Message) xmlSerial.Deserialize(textReader);

            return message;
        }
    }
}
