using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
   public class Move : Message
    {
       public int gameID { get; set; }
       public int playerID { get; set; }
       public MovementDirection direction { get; set; }

       public Move() { }


       public Move(int gID, int pID, MovementDirection dir) 
       {
           gameID = gID;
           playerID = pID;
           direction = dir;
       }
    }
}
