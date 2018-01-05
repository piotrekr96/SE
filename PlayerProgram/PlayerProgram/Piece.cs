using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerProgram
{
    public class Piece
    {
        protected int position_x, position_y;
        protected bool sham = false;
        protected Player owner = null;
        protected Bitmap bmp;
        protected bool spent = false;
        
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
            } while (!placePieceInit(position_x, position_y));
        }


        public bool placePieceInit(int x, int y)
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

        // look up the piece in the list
        public static Piece findPiece(int x, int y) {
            foreach (Piece item in MyGlobals.pieces) {
                if (x == item.position_x && y == item.position_y) {
                    return item;
                }
            }
            return null;
        }


        // setter and getters
        public void setOwner(Player pl) {
            this.owner = pl;
        }
        public void setPosX(int x) {
            this.position_x = x;
        }
        public void setPosY(int y) {
            this.position_y = y;
        }
        public Player getOwner() {
            return this.owner;
        }
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

        public Bitmap getBitmap()
        {
             return Properties.Resources.P;           
        }

        public void setSpent() {
            this.spent = true;
        }
        public bool getSpent() {
            return spent;
        }

        public Bitmap getBmpNonGoal() {
            return Properties.Resources.N;
        }
    }
}
