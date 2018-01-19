using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class PiecePicked : Message
    {
        public int playerID { get; set; }

        public PiecePicked() { }
        public PiecePicked(int pID)
        {
            playerID = pID;
        }

    }
}
