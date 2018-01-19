using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class DroppingResult : Message
    {
        public int playerID { get; set; }
        public bool isGoal { get; set; }

        public DroppingResult() { }

        public DroppingResult(int pID, bool ifGoal)
        {
            playerID = pID;
            isGoal = ifGoal;
        }

    }
}
