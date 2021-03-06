﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class JoinGame : Message
      {
        public int playerID { get; set; }
        public int gameID { get; set; }
        public Role preferredRole { get; set; }
        public Team preferredTeam { get; set; }

        public JoinGame() { }

        public JoinGame(int gID, Role role, Team team)
        {
            gameID = gID;
            preferredRole = role;
            preferredTeam = team;
        }
    }
}
