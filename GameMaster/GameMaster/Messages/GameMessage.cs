using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class GameMessage : Message
    {
        public int playerID { get; set; }
        public List<Player> playerList { get; set; }
        public Board board { get; set; }
        public PlayerLocation coordinates { get; set; }

        public GameMessage() { }

        public GameMessage(int id, List<Player> list, Board boa, PlayerLocation coords) 
        {
            playerID = id;
            playerList = list;
            board = boa;
            coordinates = coords;
        }

    }
}
