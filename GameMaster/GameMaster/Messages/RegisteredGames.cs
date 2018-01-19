using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class RegisteredGames : Message
    {
        public List<GameInfo> gameInfoList { get; set; }

        public RegisteredGames() 
        {
            gameInfoList = new List<GameInfo>();
        }

        public RegisteredGames(List<GameInfo> list)
        {
            gameInfoList = new List<GameInfo>();
            gameInfoList = list;
        }
    }
}
