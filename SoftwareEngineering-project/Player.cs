using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{



    public abstract class Player
    {
        protected int position_x, position_y;
        protected Bitmap bmp;

        public Player()
        {
            bmp = Properties.Resources.test;
        }

        // setters and getters for x and y coords
        public int getPosX()
        {
            return position_x;
        }
        public int getPosY()
        {
            return position_y;
        }
        public void setPosX(int x) {
            position_x = x;
        }
        public void setPosY(int y) {
            position_y = y;
        }

        public abstract Bitmap getBitmap();

        public bool canMove(int x, int y)
        {
            // if pos is out of board or player bounds, or occupied
            if (withinBoardBounds(x, y) == false)
            {
                return false;
            }
            if (withinPlayerBounds(y) == false)
            {
                return false;
            }
            if (!MyGlobals.boardView1.isFreeOfPlayer(x, y))
            {
                return false;
            }

            // if checks passed, it can move
            return true;
        }

        public bool MoveUp()
        {

            // check if it can move
            if (!canMove(getPosX(), getPosY() - 1))
            {
                return false;
            }

            // if it can move, set new position
            this.setPosY(getPosY() - 1);

            // return success
            return true;
        }

        public bool MoveDown()
        {

            // check if it can move
            if (!canMove(getPosX(), getPosY() + 1))
            {
                return false;
            }

            // if it can move, set new position
            this.setPosY(getPosY() + 1);

            // return success
            return true;
        }

        public bool MoveLeft()
        {
            // check if it can move
            if (!canMove(getPosX() - 1, getPosY()))
            {
                return false;
            }

            // if it can move, set new position
            this.setPosX(getPosX() - 1);

            // return success
            return true;
        }

        public bool MoveRight()
        {
            // check if it can move
            if (!canMove(getPosX() + 1, getPosY()))
            {
                return false;
            }

            // if it can move, set new position
            this.setPosX(getPosX() + 1);

            // return success
            return true;
        }
        
        // check if position is out of the board bounds
        public bool withinBoardBounds(int x, int y) {
            // if coord x is out of bounds
            if (x < 0 || x > MyGlobals.Width -1) {
                return false;
            }
            // if coord y is out of bounds
            if (y < 0 || y > MyGlobals.Height - 1)
            {
                return false;
            }
            // if all inside bounds, return success
            return true;
        }

        public abstract bool withinPlayerBounds(int y);

    }

    public class BluePlayer : Player
    {
        Random rnd = new Random();
        public BluePlayer() : base()
        {
            position_x = rnd.Next(0, MyGlobals.Width);
            position_y = rnd.Next(MyGlobals.Height - MyGlobals.smallHeight, MyGlobals.Height);
            MyGlobals.bluePlayers.Add(this);
            //   bmp = Properties.Resources.test;
        }

        public override Bitmap getBitmap()
        {
            return bmp;
        }

        // check if blue plyer does not step into red player's goal area
        public override bool withinPlayerBounds(int y) {
            if (y < MyGlobals.smallHeight)
            {
                return false;
            }
            return true;
        }

    }

    public class RedPlayer : Player
    {
        Random rnd = new Random();
       public RedPlayer() : base()
        {
            position_x = rnd.Next(0, MyGlobals.Width);
            position_y = rnd.Next(0, MyGlobals.smallHeight);
            MyGlobals.redPlayers.Add(this);

        }
        public override Bitmap getBitmap()
        {
            return bmp;
        }

        // check if red plyer does not step into blue player's goal area
        public override bool withinPlayerBounds(int y) {
            if (y > (MyGlobals.Height - MyGlobals.smallHeight-1) ) {
                return false;
            }
            return true;
        }

        





    }

}
