﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class RejectJoiningGame : Message
    {
        public int gameID { get; set; }
        public int playerID { get; set; }

        public RejectJoiningGame() { }

        public RejectJoiningGame(int gID, int pID) 
        {
            gameID = gID;
            playerID = pID;
        }
    }
}
