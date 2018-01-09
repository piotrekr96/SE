using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Represents one cell of Board object. Counts both for task and goal area.
// Purpose: locking for asynchronous Board actions.

// TO DO: getters and setters

namespace GM
{
    public class Field
    {
        int pieceID;
        int playerID;
        int goalID;

        public Field()
        {
            pieceID = -1; // no free piece on cell (player with piece held is not adequate)
            playerID = -1; // no player on field
            goalID = -1; // no goal on the cell
        }
    }
}


