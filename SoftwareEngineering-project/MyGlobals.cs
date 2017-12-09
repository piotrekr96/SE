using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project
{
   public class MyGlobals
    {
        public static Random rnd = new Random(1); // seed used only for testing
        public static Int32 Height;
        public static Int32 smallHeight;
        public static Int32 Width;
        public static BoardView1 boardView1;
        // public static List<RedPlayer> redPlayers = new List<RedPlayer>();
        // public static List<BluePlayer> bluePlayers = new List<BluePlayer>();
        public static List<Player> players = new List<Player>();
        public static Int32 nrGoals = 5;
        public static Int32 nrPieces = 2;
        public static List<Goal> goalsRed = new List<Goal>();
        public static List<Goal> goalsBlue = new List<Goal>();
        public static List<Piece> pieces = new List<Piece>();
        public static GameMasterView gameMasterView;
        public static int[,] seenDistances;
        public static int radius = 1;
    }
}
