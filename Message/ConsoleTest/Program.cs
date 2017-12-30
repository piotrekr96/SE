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
            JoinGame newJoin = new JoinGame("Default game", Role.leader, Team.red);
            string information = newJoin.messageIntoXML(newJoin);
            Console.Write(information);
            

            Console.WriteLine();
            Console.WriteLine();

            List<GameInfo> newList = new List<GameInfo>();
            newList.Add(new GameInfo("Game 1", 5, 5));
            newList.Add(new GameInfo("Game 2", 10, 10));
            newList.Add(new GameInfo("Game 3", 3, 3));
            newList.Add(new GameInfo("Game 4", 2, 8));

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
            
            Console.Write("Name: " + newMessage.name + " and role: " + newMessage.preferredRole + " and team: " + newMessage.preferredTeam);
            
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static public Message xmlToMessage(String mess)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(Message), new Type[] {typeof(JoinGame),typeof(ConfirmGameRegistration),typeof(ConfirmJoiningGame),
            typeof(GameMessage),typeof(GetGames),typeof(RegisteredGames),typeof(RegisterGame),typeof(RejectJoiningGame)});

            StringReader textReader = new StringReader(mess);
            Message message = (Message) xmlSerial.Deserialize(textReader);

            return message;
        }
    }
}
