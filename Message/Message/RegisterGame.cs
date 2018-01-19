using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class RegisterGame : Message
    {

        public int gameID { get; set; }
        public int blueTeamPlayers { get; set; }
        public int redTeamPlayers { get; set; }

        public RegisterGame() { }

        public RegisterGame(int gID, int blue, int red)
        {
            gameID = gID;
            blueTeamPlayers = blue;
            redTeamPlayers = red;
        }
    }
}
