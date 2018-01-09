using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GM
{
    class GMRunner
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello GM!");
            Console.WriteLine("Fancy");
            int[,] ta = new int[2, 2]; // rows first!

            GameMaster gm = new GameMaster();

            gm.ReadGameinfo("..\\..\\GameSettings\\XMLgameSettings1.xml");

            Console.ReadLine();

        }
    }
}
