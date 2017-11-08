using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{



   abstract class Player
    {
        protected int position_x, position_y;
        protected Bitmap bmp;

        public Player()
        {
            bmp = Properties.Resources.test;
        }

        public abstract int getPosX();
        public abstract int getPosY();
        public abstract Bitmap getBitmap();
     /*   public abstract int MoveUp();
        public abstract int MoveDown();
        public abstract int MoveLeft();
        public abstract int MoveRight();
        */
    }

    class BluePlayer : Player
    {
        Random rnd = new Random();
        public BluePlayer() : base()
        {
            position_x = rnd.Next(0, MyGlobals.Width);
            position_y = rnd.Next(MyGlobals.Height - MyGlobals.smallHeight, MyGlobals.Height);
         //   bmp = Properties.Resources.test;
        }

        public override Bitmap getBitmap()
        {
            return bmp;
        }

        public override int getPosX()
        {
            return position_x;
        }

        public override int getPosY()
        {
            return position_y;
        }


    }

    class RedPlayer : Player
    {
        Random rnd = new Random();
       public RedPlayer() : base()
        {
            position_x = rnd.Next(0, MyGlobals.Width);
            position_y = rnd.Next(0, MyGlobals.smallHeight);

        }
        public override Bitmap getBitmap()
        {
            return bmp;
        }

        public override int getPosX()
        {
            return position_x;
        }

        public override int getPosY()
        {
            return position_y;
        }


    }

}
