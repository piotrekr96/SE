﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM
{
    public class Game
    {
        public class GameState
        {
            public bool gameStarted;
            public bool gameEnded;
        }

        public GameState gameState;
        public DataGame settings;
        public string gameName;
        public int gameId;
        public Field[,] board;
        public Dictionary<int, Player> playersDictionary; // Fast mapping from XML message player ID to actual object
        public List<int> redTeam;
        public List<int> blueTeam;
        public int redLeaderId;
        public int blueLeaderId;

        public Game(DataGame data)
        {
            InitializeNulls();
            // Make new game with config
            settings = data;

            board = new Field[2 * settings.GoalLen + settings.TaskLen, settings.BoardWidth];
            for(int y=0; y< 2 * settings.GoalLen + settings.TaskLen; y++)
            {
                for (int x=0; x<settings.BoardWidth; x++)
                {
                    board[y, x] = new Field();
                   
                }
            }
            playersDictionary = new Dictionary<int, Player>(2*settings.PlayersPerTeam);
            redTeam = new List<int>(settings.PlayersPerTeam); // used for broadcast ip reference, fast lookup without plDict iterating
            blueTeam = new List<int>(settings.PlayersPerTeam);
            gameName = settings.Name;


            // Only when board is ready and set up with fields, start adding players
            Piece piece1 = new Piece();
            piece1.pos_x = 0;
            piece1.pos_y = 4;



            Console.WriteLine("Rows:{0}, Cols:{1}", board.GetLength(0), board.GetLength(1));

        }

        private void InitializeNulls()
        {
            gameName = "";
            gameId = -1;
            board = null;
            playersDictionary = new Dictionary<int, Player>();
            redTeam = new List<int>();
            blueTeam = new List<int>();
            redLeaderId = -1;
            blueLeaderId = -1;
            gameState = new GameState();
            gameState.gameEnded = false;
            gameState.gameStarted = false;
        }

       

        // Initial game setup

        private void MakePieces()
        {

        }

        private MessageProject.Team CheckTeamPreference(MessageProject.Team preferredTeam)
        {
            // Checks which team to add player to, assumes correct data options
            if ( (preferredTeam.Equals(MessageProject.Team.red) && redTeam.Count() < settings.PlayersPerTeam) || 
                (preferredTeam.Equals(MessageProject.Team.blue) && blueTeam.Count() >= settings.PlayersPerTeam) ) { return MessageProject.Team.red; }
            else { return MessageProject.Team.blue; }
        }

        private MessageProject.Role CheckRolePreference(MessageProject.Team teamToAdd, MessageProject.Role preferredRole)
        {
            if (teamToAdd.Equals(MessageProject.Team.blue))
            {
                if(preferredRole.Equals(MessageProject.Role.leader) && blueLeaderId == -1)
                {
                    return MessageProject.Role.leader;
                }
                else
                {
                    return MessageProject.Role.member;
                }
            }
            else
            {
                if (preferredRole.Equals(MessageProject.Role.member) && redLeaderId == -1)
                {
                    return MessageProject.Role.leader;
                }
                else
                {
                    return MessageProject.Role.member;
                }
            }
        }

        public Tuple<int, MessageProject.Team, MessageProject.Role, bool> MakePlayer(MessageProject.Role preferredRole, MessageProject.Team preferredTeam, int playerID)
        {
            // Returns new ID associated with player created, his team, his final role
            lock(gameState)
            {
                if(gameState.gameStarted)
                {
                    Console.WriteLine("Rejecting game join since game has already started!");
                    return new Tuple<int, MessageProject.Team, MessageProject.Role, bool>(-999, MessageProject.Team.red , MessageProject.Role.member, false);
                }
            }
            lock (playersDictionary) // to avoid overwriting adds and current ammount read
            {
                
                // Now spot is guaranteed, whether as preferred or not
                // Make returned structure equivalent to response message
                Player newPlayer = new Player();
                int newPlayerID = playerID; // acts as GUID since 1 game atm, increasing order
                playersDictionary.Add(newPlayerID, newPlayer);
                Console.WriteLine("Added new player ID: {0}", newPlayerID);
                MessageProject.Team teamToAdd = CheckTeamPreference(preferredTeam);
                newPlayer.team = teamToAdd;
                if (teamToAdd.Equals(MessageProject.Team.red)) { redTeam.Add(newPlayerID); }
                else { blueTeam.Add(newPlayerID); }

                MessageProject.Role roleToGive = CheckRolePreference(teamToAdd, preferredRole);
                if(roleToGive.Equals(MessageProject.Role.leader))
                {
                    if(teamToAdd.Equals(MessageProject.Team.blue)) { blueLeaderId = newPlayerID; }
                    else { redLeaderId = newPlayerID; }
                }
                newPlayer.role = roleToGive;
                // Add PosX, PosY, randomize, update both playerobj and field containing player info
                // Global update when dict full, just before sending Game message to start it
                bool gameJustStarted = false;
                if(playersDictionary.Count() >= 2*settings.PlayersPerTeam) { gameJustStarted = true; }
                return Tuple.Create(newPlayerID, teamToAdd, roleToGive, gameJustStarted);
            }
        }

        // Board restrictions check (board size, team restrictions) - edge cases? 

        private bool CheckMovementBoardBoundaries(int playerID, MessageProject.MovementDirection direction)
        {
            // note that board is numerated (0,0) in upper left corner, image coords!
            // Board width restriction
            // Team wise restriction
            // Dir: 1=left, 2=top, 3=right, 4=down
            Player player = playersDictionary[playerID]; // Refer to the player object
            if(direction.Equals(MessageProject.MovementDirection.up)) // move up
            {
                if(player.team.Equals(MessageProject.Team.blue))
                {
                    // First neg row is GAlen -1
                    if (player.posY <= settings.GoalLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Red area starts", player.posY, player.posY - 1);
                        return false;
                    }
                   
                }
                else if(player.team.Equals(MessageProject.Team.red))
                {
                    // First neg row is -1
                    if (player.posY <= 0)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Border end top.", player.posY, player.posY - 1);
                        return false;
                    }
                }
            }
            else if(direction.Equals(MessageProject.MovementDirection.down)) // move down
            {
                if (player.team.Equals(MessageProject.Team.blue))
                {
                    // First neg row is 2*ga + ta
                    if (player.posY + 1 >= 2 * settings.GoalLen + settings.TaskLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Board ends", player.posY, player.posY + 1);
                        return false;
                    }
                }
                else if (player.team.Equals(MessageProject.Team.red))
                {
                    if (player.posY + 1 >= settings.GoalLen + settings.TaskLen)
                    {
                        Console.WriteLine("Move from Y: {0} to {1} forbidden. Blue area starts.", player.posY, player.posY + 1);
                        return false;
                    }
                    // First neg row is GAlen - 1 
                    
                }
            }
            else if(direction.Equals(MessageProject.MovementDirection.left))
            {
                if(player.posX - 1 < 0)
                {
                    Console.WriteLine("Move from X: {0} forbidden. Board width left edge.", player.posX);
                    return false;
                }
            }
            else if(direction.Equals(MessageProject.MovementDirection.right))
            {
                if(player.posX + 1 >= settings.BoardWidth)
                {
                    Console.WriteLine("Move from X: {0} forbidden. Board width right edge.", player.posX);
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
                    Console.WriteLine("Field left occupied by another player!");
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
                    Console.WriteLine("Field right occupied by another player!");
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
            lock (board[posY - 1, posX])
            {
                if (board[posY - 1, posX].playerID != -1)
                {
                    Console.WriteLine("Field up occupied by another player!");
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

        public void MoveDown(int playerID)
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
                    Console.WriteLine("Field down occupied by another player!");
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

        

        public Tuple<int, int> HandleMoveRequest(int playerID, MessageProject.MovementDirection direction)
        {
            // Waiting from settings
            lock(gameState)
            {
                // If game hs not started, return invalid Y, X coords to client
                if(gameState.gameStarted == false)
                {
                    return Tuple.Create(-1, -1);
                }
            }
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
                    case MessageProject.MovementDirection.left:
                        Console.WriteLine("Left switch");
                        MoveLeft(playerID);
                        // In general the result is not interesting. In either case - send new/old == current, coordinates
                        break;
                    case MessageProject.MovementDirection.up:
                        Console.WriteLine("Top switch!");
                        MoveUp(playerID);
                        break;
                    case MessageProject.MovementDirection.right:
                        Console.WriteLine("Right switch!");
                        MoveRight(playerID);
                        break;
                    case MessageProject.MovementDirection.down:
                        Console.WriteLine("Down switch!");
                        MoveDown(playerID);
                        break;
                }
            }

            return Tuple.Create(playersDictionary[playerID].posY, playersDictionary[playerID].posX);

        }

        
    }
}
