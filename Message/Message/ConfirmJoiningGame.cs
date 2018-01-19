using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class ConfirmJoiningGame : Message
    {
        public int gameID { get; set; }
        public int playerID { get; set; }
        public Player player { get; set; }

        public ConfirmJoiningGame() { }

        public ConfirmJoiningGame(int gID, int pID, Player pla)
        {
            player = new Player();

            gameID = gID;
            playerID = pID;
            player.playerID = pla.playerID;
            player.role = pla.role;
            player.team = pla.team;
        }
    }
}
