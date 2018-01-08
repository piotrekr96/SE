using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class MoveResponse : Message
    {
        public int playerID { get; set; }
        public PlayerLocation playerLocation { get; set; }

        public MoveResponse() { }

        public MoveResponse(int pID, PlayerLocation loc)
        {
            playerID = pID;
            playerLocation = loc;
        }
    }
}
