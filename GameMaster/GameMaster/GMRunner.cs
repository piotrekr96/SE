using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MessageProject;

namespace GM
{
    class GMRunner
    {
        static void Main(string[] args)
        {            
            // Test
            Console.WriteLine("Hello GM!");

            GameMaster gm = new GameMaster();




            gm.launch();



            // dir = 1 -> left
            //for(int i=0; i < 5; i++)
            //{
            //    gm.game.HandleMoveRequest(1, 1);

            //}
            //// dir = 2 -> up 
            //// player 1 is blue, so he's bottom!
            //for (int i = 0; i < 5; i++)
            //{
            //    gm.game.HandleMoveRequest(1, 2);

            //}

            //Console.WriteLine();
            
            //// dir = 3 -> more right
            //gm.game.HandleMoveRequest(2, 3);

            //for (int i = 0; i < 10; i++)
            //{
            //    gm.game.HandleMoveRequest(2, 2);

            //}

            Console.ReadLine();

        }
    }
}
