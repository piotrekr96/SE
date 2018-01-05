using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class JoinGame : Message
      {

        public string name { get; set; }
        public Role preferredRole { get; set; }
        public Team preferredTeam { get; set; }

        public JoinGame() { }

        public JoinGame(String Name, Role role, Team team)
        {
            name = Name;
            preferredRole = role;
            preferredTeam = team;
        }
    }
}
