using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class ManhattanResult : Message
    {
        public int playerID { get; set; }
        List<Area> areasAround { get; set; }

        public ManhattanResult()
        {
            areasAround = new List<Area>();
        }

        public ManhattanResult(int pID, List<Area> list)
        {
            playerID = pID;
            areasAround = new List<Area>();
            areasAround = list;
        }
    }
}
