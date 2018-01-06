using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{
    class NonGoal
    {
        protected int position_x, position_y;
        protected Bitmap bmp;

        public NonGoal(int x, int y) : base()
        {
            position_x = x;
            position_y = y;
            bmp = Properties.Resources.N;
        }

    }
}
