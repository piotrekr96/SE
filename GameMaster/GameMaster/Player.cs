using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM
{
    public class Player
    {
        public int posX;
        public int posY;
        public Piece piece = null;
        public MessageProject.Team team;
        public MessageProject.Role role;

        public Player()
        {

        }
    }
}
