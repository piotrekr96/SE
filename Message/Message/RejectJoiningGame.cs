using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class RejectJoiningGame : Message
    {
        public int playerID { get; set; }
        public int gameID { get; set; }

        public RejectJoiningGame() { }

        public RejectJoiningGame(int gID) 
        {
            gameID = gID;
        }
    }
}
