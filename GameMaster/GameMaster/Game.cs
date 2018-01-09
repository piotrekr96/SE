using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM
{
    public class Game
    {
        public DataGame settings;
        public Field[,] board;
        Dictionary<int, Player> playersDictionary; // Fast mapping from XML message player ID to actual object

        public Game(DataGame data)
        {
            // Make new game with config
            settings = data;

            board = new Field[2 * settings.GoalLen + settings.TaskLen, settings.BoardWidth];
            playersDictionary = new Dictionary<int, Player>();

            Piece piece1 = new Piece();
            piece1.pos_x = 0;
            piece1.pos_y = 4;

            Player player1 = new Player();
            player1.posX = 3;
            player1.posY = 3;
            player1.team = "blue"; // down
            playersDictionary.Add(1, player1); // To DO: reconsider ID inside player (any use?)
        }

        // Initial game setup

        private void MakePieces()
        {

        }

        private void MakePlayers()
        {

        }

        // Board restrictions check

        private bool CheckMovementBoardBoundaries(int playerID, string direction)
        {
            // Board width restriction
            // Team wise restriction
            Player player = playersDictionary[playerID]; // Refer to the player object
            if(direction.Equals("up"))
            {
                if(player.team.Equals("blue"))
                {
                    // First neg row is GAlen + TAlen 
                    if(player.posY >= settings.GoalLen+settings.TaskLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Red area starts.", player.posY, player.posY + 1);
                        return false;
                    }
                }
                else if(player.team.Equals("red"))
                {
                    // First neg row is 2*GAlen + TAlen 
                    if (player.posY >= 2*settings.GoalLen + settings.TaskLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Board ends.", player.posY, player.posY + 1);
                        return false;
                    }
                }
            }
            else if(direction.Equals("down"))
            {
                if (player.team.Equals("blue"))
                {
                    // First neg row is -1
                    if (player.posY <= 0)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Board ends", player.posY, player.posY - 1);
                        return false;
                    }
                }
                else if (player.team.Equals("red"))
                {
                    // First neg row is GAlen - 1 
                    if (player.posY < settings.GoalLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Blue area starts", player.posY, player.posY - 1);
                        return false;
                    }
                }
            }
            else if(direction.Equals("left") || direction.Equals("right"))
            {
                if(player.posX - 1 < 0 || player.posX + 1 >= settings.BoardWidth)
                {
                    Console.WriteLine("Move from X: {0} forbidden. Board width.", player.posX);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Unknown move direction: {0}", direction);
                return false;
            }

            return true;
        }

        public void HandleRequest()
        {
            // Already in new thread, ThreadStart 
            // Deserialize message - only type check
            // Decide upon move/pick/drop - message type
        }

        public void HandleMoveRequest() // pass 
        {
            // Cast message only on single class (final type decision)
            // Check direction of movement
            // Map player ID, extract his coords
            // Check if coords allow for locking (without checking if player)
            // Lock on cur field, movement direction field - requires prior check if move valid - board bounds, goal area
        }

        
    }
}
