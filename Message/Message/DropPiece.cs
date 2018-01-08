using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class DropPiece : Message
    {
        public int gameID { get; set; }
        public int playerID { get; set; }

        public DropPiece() { }

        public DropPiece(int gID, int pID)
        {
            gameID = gID;
            playerID = pID;
        }

    }
}
