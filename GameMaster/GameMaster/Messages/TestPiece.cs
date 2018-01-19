using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class TestPiece : Message
    {
        public int gameID { get; set; }
        public int playerID { get; set; }

        public TestPiece() { }

        public TestPiece(int gID, int pID)
        {
            gameID = gID;
            playerID = pID;
        }
    }
}
