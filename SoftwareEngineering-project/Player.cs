using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{
    public class Player
    {
        int position_x, position_y;
        char colour;
        Bitmap bmp;
        Piece carrying = null;
        List<Goal> discoveredGoals; // what the player discovered/received info from other player
        List<NonGoal> discoveredNonGoals; // what the player discovered/received info received info from other players

        public Player(char _team)
        {
            colour = _team;

            if (_team == 'b')
            {
                bmp = Properties.Resources.B;
                do
                {
                    position_x = MyGlobals.rnd.Next(0, MyGlobals.Width);
                    position_y = MyGlobals.rnd.Next(MyGlobals.smallHeight, MyGlobals.Height - MyGlobals.smallHeight - 1);

                } while (!canPlacePlayer(position_x, position_y));
            }
            else
            {
                bmp = Properties.Resources.R;
                do
                {
                    position_x = MyGlobals.rnd.Next(0, MyGlobals.Width);
                    position_y = MyGlobals.rnd.Next(MyGlobals.smallHeight, MyGlobals.Height - MyGlobals.smallHeight - 1);

                } while (!canPlacePlayer(position_x, position_y));
            }
            MyGlobals.players.Add(this);

        }

        // setters and getters 
        public int getPosX()
        {
            return position_x;
        }
        public int getPosY()
        {
            return position_y;
        }
        public void setPosX(int x)
        {
            position_x = x;
        }
        public void setPosY(int y)
        {
            position_y = y;
        }
        public Piece getCarrying()
        {
            return carrying;
        }

        public Bitmap getBitmap()
        {
            return bmp;
        }

        public char getColour() {
            return this.colour;
        }

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

        // movement also updates coords of pieces if they are being carried by the player that is moving
        public bool MoveUp()
        {

            // check if it can move
            if (!canMove(getPosX(), getPosY() - 1))
            {
                return false;
            }

            // if it can move, set new position
            this.setPosY(getPosY() - 1);

            // if the player carries a piece, update piece location as well
            if (this.carrying != null)
            {
                //new coord Y of piece will be equal to new coord Y of player
                carrying.setPosY(this.getPosY());
            }

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

            // if the player carries a piece, update piece location as well
            if (this.carrying != null)
            {
                //new coord Y of piece will be equal to new coord Y of player
                carrying.setPosY(this.getPosY());
            }


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

            // if the player carries a piece, update piece location as well
            if (this.carrying != null)
            {
                //new coord X of piece will be equal to new coord X of player
                carrying.setPosX(this.getPosX());
            }

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

            // if the player carries a piece, update piece location as well
            if (this.carrying != null)
            {
                //new coord X of piece will be equal to new coord Y of player
                carrying.setPosX(this.getPosX());
            }

            // return success
            return true;
        }

        // check if position is out of the board bounds
        public bool withinBoardBounds(int x, int y)
        {
            // if coord x is out of bounds
            if (x < 0 || x > MyGlobals.Width - 1)
            {
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

        public bool withinPlayerBounds(int y)
        {
            if (colour == 'b')
            {
                if (y < MyGlobals.smallHeight)
                {
                    return false;
                }
                return true;
            }
            else
            {
                if (y > (MyGlobals.Height - MyGlobals.smallHeight - 1))
                {
                    return false;
                }
                return true;
            }
        }
        public bool canPlacePlayer(int x, int y)
        {

            // if a player or piece with same coords is already there, return failure
            foreach (Player item in MyGlobals.players)
            {
                if (x == item.getPosX() && y == item.getPosY())
                {
                    return false;
                }
            }
            foreach (Piece item in MyGlobals.pieces)
            {
                if (x == item.getPosX() && y == item.getPosY())
                {
                    return false;
                }
            }


            // if there is no duplicate return success
            return true;
        }

        // try to pick piece from own location
        public bool pickPiece()
        {
            Piece pi = Piece.findPiece(position_x, position_y);

            // if the piece does not exist, return failure
            if (pi == null)
            {
                return false;
            }

            //if the piece is spent (placed in a goal cell), can't pick it
            if (pi.getSpent() == true) {
                return false;
            }

            // if the player carries a piece, and he also sits in a cell with a piece,
            // he won't be able to pick the other piece
            if (this.carrying != null) {
                return false;
            }

            // if the piece exists but it is carried by someone else, return failure
            if (pi.getOwner() != null)
            {
                return false;
            }    

            //if the piece exists and is not carried by another player, pick it and update icon of player
            this.carrying = pi;
            pi.setOwner(this);
            if (this.colour == 'b')
            {
                this.bmp = Properties.Resources.BP;
            }
            else {
                this.bmp = Properties.Resources.RP;
            }

            return true;
        }

        // does not take coords
        // a player can test a piece it it has been picked by him
        // returns true if piece is a true piece
        public bool testPiece()
        {
            return !this.carrying.getSham();
        }

        public bool canPlacePiece(int x, int y)
        {
            if (colour == 'b')
            {
                // if x coord out out bounds, return failure
                if (x < 0 || x > (MyGlobals.Width - 1))
                {
                    return false;
                }

                int min = MyGlobals.Height - MyGlobals.smallHeight;
                int max = MyGlobals.Height - 1;

                /*
                // if y coord is out of blue goals area bounds, return failure
                if (y < min || y > max)
                {
                    return false;
                }
                */

                // if all checks pass, return success
                return true;
            }
            else
            {
                // if x coord out out bounds, return failure
                if (x < 0 || x > (MyGlobals.Width - 1))
                {
                    return false;
                }

                int max = MyGlobals.smallHeight - 1;

                /*
                // if y coord is out of red goals area bounds, return failure
                if (y < 0 || y > max)
                {
                    return false;
                }
                */

                // if all checks pass, return success
                return true;
            }
        }

        // can be placed without checking it is a sham
        // returns true if at (x,y) it was a goal
        // feedback is returned in discoverGoal()
        public void placePiece(int x, int y)
        {
            // place piece at coords and update attributes
            this.carrying.setPosX(x);
            this.carrying.setPosY(y);
            this.carrying.setOwner(null);
            this.carrying = null;

            if (this.colour == 'b')
            {
                this.bmp = Properties.Resources.B;
            }
            else {
                this.bmp = Properties.Resources.R;
            }
        }

        public bool tryPlacePiece(int x, int y)
        {
            // trying to place a piece without possessing one
            if (this.carrying == null) {
                return false;
            }

            // check if there is a piece already at those coords
            foreach (Piece item in MyGlobals.pieces) {
                if (item.getPosX() == x && item.getPosY() == y && item != this.carrying) {
                    return false;
                }
            }


            if (!canPlacePiece(x, y))
            {
                Console.WriteLine("Index for placing on goal area is out of bounds!");
                return false;
            }

            // if check passed, place piece 
            bool wasSham = this.carrying.getSham();
           

            // placing a piece which is a sham results in getting no information
            if (wasSham)
            {
                Console.WriteLine("TESTING PURPOSES: The piece was a sham!");
                
            }


            // if the piece is not a sham, check if there is a goal at those coords, if yes, 
            // mark the goal as discovered and the piece as spent (unable to pick it again)
            // otherwise, the goal is not discovered
            if (!wasSham && discoverGoal(x, y))
            {
                Console.WriteLine("There was goal at those coordinates.");
                this.carrying.setSpent();
            }
            else
            {
                Console.WriteLine("There was no goal at those coordinates.");
            }
            placePiece(x, y);
            Console.WriteLine("The piece has been placed!");
            return true;
        }

        public bool discoverGoal(int x, int y)
        {
            if (colour == 'b')
            {
                foreach (Goal item in MyGlobals.goalsBlue)
                {

                    // if there exists a goal at (x,y) and it has not been discovered yet, discover it
                    if (item.getPosX() == x && item.getPosY() == y && item.getDiscovered() != true)
                    {
                        item.setDiscovered();
                        this.addGoal(item.getPosX(), item.getPosY());
                        item.setDiscoveror(this);
                        return true;
                    }
                }

                // if at (x,y) there was no goal, return failure
                return false;
            }
            else
            {
                // check if cell at coords holds a goal or non-goal
                foreach (Goal item in MyGlobals.goalsRed)
                {

                    // if there exists a goal at (x,y) and it has not been discovered yet, discover it
                    if (item.getPosX() == x && item.getPosY() == y && item.getDiscovered() != true)
                    {
                        item.setDiscovered();
                        this.addGoal(item.getPosX(), item.getPosY());
                        item.setDiscoveror(this);
                        return true;
                    }
                }

                // if at (x,y) there was no goal, return failure
                return false;
            }
        }

        // method to use when information about discovered goals is exchanged with other players
        public void addGoal(int x, int y) {
            Goal g = new Goal(x,y);
            discoveredGoals.Add(g);
        }

        // method to use when information about discovered nongoals is exchanged with other players
        public void addNonGoal(int x, int y)
        {
            NonGoal g = new NonGoal(x, y);
            discoveredNonGoals.Add(g);
        }


        public void computeManDist()
        {
            Piece closestPiece = null;
            int minimumDist = 10000; //very large value, with low prob that it takes place

            // iterate through all pieces that are NOT owned by other players, and find the closes one
            foreach (Piece item in MyGlobals.pieces) {
                if (item.getOwner() == null && item.getSpent() == false) {
                    // look only at pieces in the task area
                    if (item.getPosY() >= MyGlobals.smallHeight && item.getPosY() <= (MyGlobals.Height - MyGlobals.smallHeight - 1)) {
                        int temp = Math.Abs(this.position_x - item.getPosX()) + Math.Abs(this.position_y - item.getPosY());
                        if (temp < minimumDist)
                        {
                            minimumDist = temp;
                            closestPiece = item;
                        }
                    }
                }
            }

            // for each neighbouring cell of the player, within a given radius: here includes 8 neighbouring cells
            for (int i =(- MyGlobals.radius); i<=(+ MyGlobals.radius); i++) {
                for (int j = (- MyGlobals.radius); j <= (+ MyGlobals.radius); j++) {
                    if (withinBoardBounds(this.position_x + i, this.position_y + j)) {
                        int d;
                        if (closestPiece == null)
                        {
                            d = -1;
                        }
                        else {
                            // offset i and j added, because it saves the distance from the player's neighbour to the closest piece
                            d = Math.Abs((this.position_x + i) - closestPiece.getPosX()) + Math.Abs((this.position_y + j) - closestPiece.getPosY());
                        }
                        
                        // update the array of distances at the positions where the neighbours and the player exist on the board
                        // as player moves and keeps asking, the array will continue to be populated
                        MyGlobals.seenDistances[this.position_x + i, this.position_y + j] = d;
                    }
                                
                }
            } 
        }

        public void testDistConsole() {
            this.computeManDist();
            for (int i=0; i< MyGlobals.seenDistances.GetLength(1); i++) {
                Console.WriteLine("");
                for (int j=0; j<MyGlobals.seenDistances.GetLength(0); j++) {
                    Console.Write(MyGlobals.seenDistances[j,i]);
                    Console.Write(" ");
                }
            }
            Console.WriteLine("");
        }


    }


    /*  public abstract class Player
      {
          protected int position_x, position_y;
          protected Bitmap bmp;
          protected Piece carrying = null;

          public Player()
          {
          }

          // setters and getters 
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
          public Piece getCarrying() {
              return carrying;
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

          // movement also updates coords of pieces if they are being carried by the player that is moving
          public bool MoveUp()
          {

              // check if it can move
              if (!canMove(getPosX(), getPosY() - 1))
              {
                  return false;
              }

              // if it can move, set new position
              this.setPosY(getPosY() - 1);

              // if the player carries a piece, update piece location as well
              if (this.carrying != null) {
                  //new coord Y of piece will be equal to new coord Y of player
                  carrying.setPosY(this.getPosY()); 
              }

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

              // if the player carries a piece, update piece location as well
              if (this.carrying != null)
              {
                  //new coord Y of piece will be equal to new coord Y of player
                  carrying.setPosY(this.getPosY());
              }


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

              // if the player carries a piece, update piece location as well
              if (this.carrying != null)
              {
                  //new coord X of piece will be equal to new coord X of player
                  carrying.setPosX(this.getPosX());
              }

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

              // if the player carries a piece, update piece location as well
              if (this.carrying != null)
              {
                  //new coord X of piece will be equal to new coord Y of player
                  carrying.setPosX(this.getPosX());
              }

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

          public bool canPlacePlayer(int x, int y)
          {

              // if a player or piece with same coords is already there, return failure
              foreach (Player item in MyGlobals.redPlayers)
              {
                  if (x == item.getPosX() && y == item.getPosY())
                  {
                      return false;
                  }
              }
              foreach (Player item in MyGlobals.bluePlayers)
              {
                  if (x == item.getPosX() && y == item.getPosY())
                  {
                      return false;
                  }
              }
              foreach (Piece item in MyGlobals.pieces)
              {
                  if (x == item.getPosX() && y == item.getPosY())
                  {
                      return false;
                  }
              }


              // if there is no duplicate return success
              return true;
          }

          // try to pick piece from own location
          public bool pickPiece() {
              Piece pi = Piece.findPiece(position_x, position_y);

              // if the piece does not exist, return failure
              if (pi == null) {
                  return false;
              }

              // if the piece exists but it is carried by someone else, return failure
              if (pi.getOwner() != null) {
                  return false;
              }

              //if the piece exists and is not carried by another player, pick it
              this.carrying = pi;
              pi.setOwner(this);
              return true;
          }

          // does not take coords
          // a player can test a piece it it has been picked by him
          public bool testPiece() {
              return this.carrying.getSham();
          }

          public abstract bool canPlacePiece(int x, int y);

          // can be placed without checking it is a sham
          // returns true if at (x,y) it was a goal
          // feedback is returned in discoverGoal()
          public void placePiece(int x, int y)
          {
              // place piece at coords and update attributes
              this.carrying.setPosX(x);
              this.carrying.setPosY(y);
              this.carrying.setOwner(null);
              this.carrying = null;
          }

          public bool tryPlacePiece(int x, int y) {
              if (!canPlacePiece(x, y))
              {
                  Console.WriteLine("Index for placing on goal area is out of bounds!");
                  return false;
              }

              // if check passed, place piece 
              bool wasSham = this.carrying.getSham();
              placePiece(x, y);

              // placing a piece which is a sham results in getting no information
              if (wasSham) {
                  Console.WriteLine("TESTING PURPOSES: The piece was a sham, but the piece has been placed!");
                  return true;
              }
              // get feedback
              if (discoverGoal(x, y))
              {
                  Console.WriteLine("There was goal at those coordinates, and the piece has been placed!");
              }
              else {
                  Console.WriteLine("There was no goal at those coordinates, but the piece has been placed!");
              }
              return true;
          }

          public abstract bool discoverGoal(int x, int y);

      }

      public class BluePlayer : Player
      {
          public BluePlayer() : base()
          {
              // try to place player in tasks area
              do
              {
                  position_x = MyGlobals.rnd.Next(0, MyGlobals.Width);
                  position_y = MyGlobals.rnd.Next(MyGlobals.smallHeight, MyGlobals.Height - MyGlobals.smallHeight - 1);

              } while (!canPlacePlayer(position_x, position_y));
              MyGlobals.bluePlayers.Add(this);
              bmp = Properties.Resources.B;
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

          public override bool canPlacePiece(int x, int y)
          {
              // if x coord out out bounds, return failure
              if (x < 0 || x > (MyGlobals.Width - 1))
              {
                  return false;
              }

              int min = MyGlobals.Height - MyGlobals.smallHeight;
              int max = MyGlobals.Height - 1;

              // if y coord is out of blue goals area bounds, return failure
              if (y < min || y > max)
              {
                  return false;
              }

              // if all checks pass, return success
              return true;
          }

          public override bool discoverGoal(int x, int y)
          {
              // check if cell at coords holds a goal or non-goal
              foreach (Goal item in MyGlobals.goalsBlue)
              {

                  // if there exists a goal at (x,y) and it has not been discovered yet, discover it
                  if (item.getPosX() == x && item.getPosY() == y && item.getDiscovered() != true)
                  {
                      item.setDiscovered();
                      return true;
                  }
              }

              // if at (x,y) there was no goal, return failure
              return false;
          }

      }

      public class RedPlayer : Player
      {
         public RedPlayer() : base()
          {
              // try to place player in tasks area
              do {
                  position_x = MyGlobals.rnd.Next(0, MyGlobals.Width);
                  position_y = MyGlobals.rnd.Next(MyGlobals.smallHeight, MyGlobals.Height - MyGlobals.smallHeight - 1);

              } while(!canPlacePlayer(position_x, position_y));
              bmp = Properties.Resources.R;
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

          public override bool canPlacePiece(int x, int y)
          {
              // if x coord out out bounds, return failure
              if (x < 0 || x > (MyGlobals.Width - 1))
              {
                  return false;
              }

              int max = MyGlobals.smallHeight - 1;

              // if y coord is out of red goals area bounds, return failure
              if (y < 0 || y > max)
              {
                  return false;
              }

              // if all checks pass, return success
              return true;
          }

          public override bool discoverGoal(int x, int y)
          {
              // check if cell at coords holds a goal or non-goal
              foreach (Goal item in MyGlobals.goalsRed)
              {

                  // if there exists a goal at (x,y) and it has not been discovered yet, discover it
                  if (item.getPosX() == x && item.getPosY() == y && item.getDiscovered() != true)
                  {
                      item.setDiscovered();
                      return true;
                  }
              }

              // if at (x,y) there was no goal, return failure
              return false;
          }




      }
  */
}
