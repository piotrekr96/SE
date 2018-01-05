using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class RegisterGame : Message
    {

        public string name { get; set; }
        public int blueTeamPlayers { get; set; }
        public int redTeamPlayers { get; set; }

        public RegisterGame() { }

        public RegisterGame(String Name, int blue, int red)
        {
            name = Name;
            blueTeamPlayers = blue;
            redTeamPlayers = red;
        }
    }
}
