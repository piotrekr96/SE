using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{
    public class Goal
    {
        protected int position_x, position_y;
        protected bool discovered = false;
        protected Bitmap bmp;

        public Goal () : base()
        {

            // generate a goal and try to place it
            do {
                position_x = MyGlobals.rnd.Next(0, MyGlobals.Width);
                position_y = MyGlobals.rnd.Next(0, MyGlobals.smallHeight);
            } while(!placeGoalInit(position_x, position_y));
            bmp = Properties.Resources.G;

        }

        // this one used for blue
        public Goal(int x, int y) : base()
        {
            position_x = x;
            position_y = y;
            bmp = Properties.Resources.G;
        }


        public bool placeGoalInit(int x, int y) {

            // if a goal with same coords is already there, return failure
            foreach (Goal item in MyGlobals.goalsRed) {
                if ( x == item.getPosX() && y == item.getPosY() ) {
                    return false;
                }
            }

            // if there is no duplicate, place the goal and return success
            MyGlobals.goalsRed.Add(this);
            MyGlobals.goalsBlue.Add(new Goal(this.position_x, this.position_y + (MyGlobals.Height - MyGlobals.smallHeight)));
            return true;
        }


        // getters
        public int getPosX()
        {
            return position_x;
        }
        public int getPosY()
        {
            return position_y;
        }
        public bool getDiscovered() {
            return discovered;
        }

        // setter for discovered
        public void setDiscovered() {
            this.discovered = true;          
        }


        public Bitmap getBitmap()
        {
            return bmp;
        }

        public Bitmap getDiscoveredBitmap()
        {
            return Properties.Resources.YG;
        }

    }
}
