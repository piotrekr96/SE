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
        public string gameName;
        public Field[,] board;
        Dictionary<int, Player> playersDictionary; // Fast mapping from XML message player ID to actual object
        List<int> redTeam;
        List<int> blueTeam;

        public Game(DataGame data)
        {
            // Make new game with config
            settings = data;

            board = new Field[2 * settings.GoalLen + settings.TaskLen, settings.BoardWidth];
            playersDictionary = new Dictionary<int, Player>(2*settings.PlayersPerTeam);
            redTeam = new List<int>(settings.PlayersPerTeam); // used for broadcast ip reference, fast lookup without plDict iterating
            blueTeam = new List<int>(settings.PlayersPerTeam);
            gameName = settings.Name;


            // Only when board is ready and set up with fields, start adding players
            Piece piece1 = new Piece();
            piece1.pos_x = 0;
            piece1.pos_y = 4;

            Player player1 = new Player();
            player1.posX = 3;
            player1.posY = 3;
            player1.team = "blue"; // down
            playersDictionary.Add(1, player1); // To DO: reconsider ID inside player (any use?)
            blueTeam.Add(1);

            Player player2 = new Player();
            player2.team = "red";
            player2.posY = 2;
            player2.posX = 1;
            playersDictionary.Add(2, player2); // make an add player method to make sure XY generated are correct, + that he's added to dictionary!!!
            redTeam.Add(2);

            Console.WriteLine("Rows:{0}, Cols:{1}", board.GetLength(0), board.GetLength(1));

        }

       

        // Initial game setup

        private void MakePieces()
        {

        }

        private string CheckTeamPreference(string preferredTeam)
        {
            // Checks which team to add player to, assumes correct data options
            if ( (preferredTeam.Equals("red") && redTeam.Count() < settings.PlayersPerTeam) || (preferredTeam.Equals("blue") && blueTeam.Count() >= settings.PlayersPerTeam) ) { return "red"; }
            else { return "blue"; }
        }

        public Tuple<int, string> MakePlayer(string preferredTeam)
        {
            // Returns new ID associated with player created, his team
            // NOTE: Deserializer should check if all fields are in correct format! team only blue/red
            if(playersDictionary.Count() >= 2*settings.PlayersPerTeam)
            {
                Console.WriteLine("Reached players limit.");
                // Send reject joining game (with no ID associated to player!)
                return null;
            }
            // Now spot is guaranteed, whether as preferred or not
            // Make returned structure equivalent to response message
            Player newPlayer = new Player();           
            int newPlayerID = playersDictionary.Keys.Count() + 1; // acts as GUID since 1 game atm, increasing order
            playersDictionary.Add(newPlayerID, newPlayer);

            string teamToAdd = CheckTeamPreference(preferredTeam);
            if(teamToAdd.Equals("red")) { redTeam.Add(newPlayerID); }
            else { blueTeam.Add(newPlayerID); }

            newPlayer.team = teamToAdd;
            // Add PosX, PosY, randomize, update both playerobj and field containing player info
            // Global update when dict full, just before sending Game message to start it
            return Tuple.Create(newPlayerID, teamToAdd);
        }

        // Board restrictions check (board size, team restrictions) - edge cases? 

        private bool CheckMovementBoardBoundaries(int playerID, int direction)
        {
            // Board width restriction
            // Team wise restriction
            // Dir: 1=left, 2=top, 3=right, 4=down
            Player player = playersDictionary[playerID]; // Refer to the player object
            if(direction == 2) // move up
            {
                if(player.team.Equals("blue"))
                {
                    // First neg row is GAlen + TAlen 
                    if(player.posY + 1 >= settings.GoalLen + settings.TaskLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Red area starts.", player.posY, player.posY + 1);
                        return false;
                    }
                }
                else if(player.team.Equals("red"))
                {
                    // First neg row is 2*GAlen + TAlen 
                    if (player.posY + 1 >= 2*settings.GoalLen + settings.TaskLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Board ends.", player.posY, player.posY + 1);
                        return false;
                    }
                }
            }
            else if(direction == 4) // move down
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
                    if (player.posY <= settings.GoalLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Blue area starts", player.posY, player.posY - 1);
                        return false;
                    }
                }
            }
            else if(direction == 1 || direction == 3)
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

        public void MoveLeft(int playerID)
        {
            int posX = playersDictionary[playerID].posX;
            int posY = playersDictionary[playerID].posY;
            
            Console.WriteLine("Current pos: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);
            // lock on cur pos and left pos
            // NOTE: to avoid deadlocks keep the order of locking the same everywhere!
            lock(board[posY, posX-1])
            {
                if(board[posY, posX -1].playerID != -1)
                {
                    // Destination occupied by another player
                    // Return current position anyway in the response message
                    return;
                }
                lock(board[posY, posX])
                {
                    // Both spots free to modify
                    board[posY, posX].playerID = -1;
                    board[posY, posX - 1].playerID = playerID;
                    playersDictionary[playerID].posX -= 1;
                }
            }
            // update player coords and game field
            Console.WriteLine("Position after move: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);
        }

        public void MoveRight(int playerID)
        {
            int posX = playersDictionary[playerID].posX;
            int posY = playersDictionary[playerID].posY;

            Console.WriteLine("Current pos: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);
            // lock on cur pos and left pos
            // NOTE: to avoid deadlocks keep the order of locking the same everywhere!
            lock (board[posY, posX + 1])
            {
                if (board[posY, posX + 1].playerID != -1)
                {
                    // Destination occupied by another player
                    // Return current position anyway in the response message
                    return;
                }
                lock (board[posY, posX])
                {
                    // Both spots free to modify
                    board[posY, posX].playerID = -1;
                    board[posY, posX + 1].playerID = playerID;
                    playersDictionary[playerID].posX += 1;
                }
            }
            // update player coords and game field
            Console.WriteLine("Position after move: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);

        }

        public void MoveUp(int playerID)
        {
            int posX = playersDictionary[playerID].posX;
            int posY = playersDictionary[playerID].posY;

            Console.WriteLine("Current pos: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);
            // lock on cur pos and left pos
            // NOTE: to avoid deadlocks keep the order of locking the same everywhere!
            lock (board[posY + 1, posX])
            {
                if (board[posY + 1, posX].playerID != -1)
                {
                    // Destination occupied by another player
                    // Return current position anyway in the response message
                    return;
                }
                lock (board[posY, posX])
                {
                    // Both spots free to modify
                    board[posY, posX].playerID = -1;
                    board[posY + 1, posX].playerID = playerID;
                    playersDictionary[playerID].posY += 1;
                }
            }
            // update player coords and game field
            Console.WriteLine("Position after move: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);

        }

        public void MoveDown(int playerID)
        {
            int posX = playersDictionary[playerID].posX;
            int posY = playersDictionary[playerID].posY;

            Console.WriteLine("Current pos: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);
            // lock on cur pos and left pos
            // NOTE: to avoid deadlocks keep the order of locking the same everywhere!
            lock (board[posY - 1, posX])
            {
                if (board[posY - 1, posX].playerID != -1)
                {
                    // Destination occupied by another player
                    // Return current position anyway in the response message
                    return;
                }
                lock (board[posY, posX])
                {
                    // Both spots free to modify
                    board[posY, posX].playerID = -1;
                    board[posY - 1, posX].playerID = playerID;
                    playersDictionary[playerID].posY -= 1;
                }
            }
            // update player coords and game field
            Console.WriteLine("Position after move: X:{0}, Y:{1}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);

        }

        

        public Tuple<int, int> HandleMoveRequest(int playerID, int direction) // pass 
        {
            // Waiting from settings

            if (!CheckMovementBoardBoundaries(playerID, direction))
            {
                Console.WriteLine("Invalid move {0} from position: x:{1}, y:{2}", playersDictionary[playerID].posX, playersDictionary[playerID].posY);
            }
            // Cast message only on single class (final type decision)
            // Check direction of movement
            // Map player ID, extract his coords
            // Check if coords allow for locking (without checking if player inside goal cell)
            // Lock on cur field, movement direction field - requires prior check if move valid - board bounds, goal area
            // Assume direction correct! XML valid
            else
            {
                switch (direction)
                {
                    case 1:
                        Console.WriteLine("Left switch");
                        MoveLeft(playerID);
                        // In general the result is not interesting. In either case - send new/old == current, coordinates
                        break;
                    case 2:
                        Console.WriteLine("Top switch!");
                        MoveUp(playerID);
                        break;
                    case 3:
                        Console.WriteLine("Right switch!");
                        MoveRight(playerID);
                        break;
                    case 4:
                        Console.WriteLine("Down switch!");
                        MoveDown(playerID);
                        break;
                }
            }

            return Tuple.Create(playersDictionary[playerID].posY, playersDictionary[playerID].posY);

        }

        
    }
}
