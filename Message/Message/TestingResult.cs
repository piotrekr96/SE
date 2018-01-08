using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class TestingResult : Message
    {
        public int playerID { get; set; }
        public bool isSham { get; set; }
        
        public TestingResult() { }

        public TestingResult(int pID, bool ifSham)
        {
            playerID = pID;
            isSham = ifSham;
        }

    }
}
