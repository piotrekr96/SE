using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Represents a single task. Used both for task and goal area.

// TO DO: verify droping possibilities (goal/task area), drop conditions on fields with stuff

namespace GM
{
    public class Piece
    {
        public int pos_x;
        public int pos_y;
        public bool sham;
        public bool carried;
        public bool used_on_goal;

        public Piece()
        {
            sham = false;
            carried = false;
            used_on_goal = false;
        }


    }
}
