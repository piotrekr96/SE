using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{
    public class Piece
    {
        protected int position_x, position_y;
        protected bool sham = false;

        
        
        public Piece() : base()
        {
            // randomly turn the piece into a sham
            int r = MyGlobals.rnd.Next(0, 2);
            if (r == 1) {               
                sham = true;
            }

            // make a piece and plce it if possible
            do
            {
                position_x = MyGlobals.rnd.Next(0, MyGlobals.Width);
                position_y = MyGlobals.rnd.Next(MyGlobals.smallHeight, MyGlobals.Height-MyGlobals.smallHeight-1);
            } while (!placePiece(position_x, position_y));


        }


        public bool placePiece(int x, int y)
        {

            // if a piece with same coords is already there, return failure
            foreach (Piece item in MyGlobals.pieces)
            {
                if (x == item.getPosX() && y == item.getPosY())
                {
                    return false;
                }
            }

            // if there is no duplicate, place the piece and return success
            MyGlobals.pieces.Add(this);
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
        public bool getSham()
        {
            return sham;
        }


    }
}
