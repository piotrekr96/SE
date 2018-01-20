using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM
{
    public class DataGame
    {
        // For storing only read XML config (avoid multiple IO)

        public string Name { get; set; }
        public int BoardWidth { get; set; }
        public int TaskLen { get; set; }
        public int GoalLen { get; set; }
        public int InitialPieces { get; set; }
        public int PlayersPerTeam { get; set; }


        // Delays - possibly another config?
        // TO DO: add schema to check first if data types correct in config! no floats plz
        public long DelayMove { get; set; }
        public long DelayPick { get; set; }
        public long DelayDrop { get; set; }

        public override string ToString()
        {


            return "Name: " + Name  + " BoardWidth: " + BoardWidth.ToString() + " TaskLen: " + TaskLen.ToString() + " GoalLen: " + GoalLen.ToString() + " InitialPieces: " + InitialPieces.ToString() + 
                " PlayersPerTeam: " + PlayersPerTeam.ToString() + " DelayMove: " + DelayMove.ToString() + " DelayPick: " + DelayPick.ToString() + " DelayDrop: " + DelayDrop.ToString();
        }

    }
}
