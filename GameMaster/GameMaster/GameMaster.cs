using System;
using System.Xml;

namespace GM
{
    class GameMaster
    {
        Game game; // later into list / dic with ID's

   

        public void MakeGame(DataGame data)
        {
            
            game = new Game(data);

        }


        public void ReadGameinfo(string fileName)
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
            
        }

        

    }

}