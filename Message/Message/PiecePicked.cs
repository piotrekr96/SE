using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class PiecePicked : Message
    {
        public int playerID { get; set; }
        public bool isPiecePicked { get; set; }

        public PiecePicked() { }
        public PiecePicked(int pID, bool ifPicked)
        {
            playerID = pID;
            isPiecePicked = ifPicked;
        }

    }
}
