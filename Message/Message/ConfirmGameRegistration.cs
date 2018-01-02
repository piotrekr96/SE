using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageProject
{
    public class ConfirmGameRegistration : Message
    {
        public int gameID { get; set; }

        public ConfirmGameRegistration() { }

        public ConfirmGameRegistration(int id)
        {
            gameID = id;
        }
    }
}
