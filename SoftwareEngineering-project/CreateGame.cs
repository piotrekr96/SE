using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareEngineering_project
{
    public partial class CreateGame : Form
    {
        public CreateGame()
        {
            InitializeComponent();

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (BoardHeightTextBox.Text == "" || BoardTeamHeightTextBox.Text == "" || BoardWidthTextBox.Text == "")
            {
                MessageBox.Show("Fill all the fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool isNumber = Int32.TryParse(BoardHeightTextBox.Text, out MyGlobals.Height);
            bool isNumber2 = Int32.TryParse(BoardTeamHeightTextBox.Text, out MyGlobals.smallHeight);
            bool isNumber3 = Int32.TryParse(BoardWidthTextBox.Text, out MyGlobals.Width);
            bool isNumber4 = Int32.TryParse(numberOfGoals.Text, out MyGlobals.nrGoals);

            if (isNumber == false || isNumber2 == false || isNumber3 == false || isNumber4 == false)
            {
                MessageBox.Show("Input of wrong type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MyGlobals.Height < 2 * MyGlobals.smallHeight)
            {
                MessageBox.Show("H < 2h", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MyGlobals.Height<=0 || MyGlobals.smallHeight <= 0 || MyGlobals.Width <= 0)
            {
                MessageBox.Show("Input of wrong type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MyGlobals.nrGoals <=0) {
                MessageBox.Show("The number of goals has to be positive", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Hide();

            if (MyGlobals.boardView1 == null)
            {
                MyGlobals.boardView1 = new BoardView1();
                /*  Console.WriteLine("BoardView constructor finished");
                  MyGlobals.boardView1.AddPlayers();
               */


                /*
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Positions of goal in red area: " + MyGlobals.goalsRed[i].getPosX() + " " + MyGlobals.goalsRed[i].getPosY());
                    Console.WriteLine("Positions of goal in blue area: "+MyGlobals.goalsBlue[i].getPosX()+" "+ MyGlobals.goalsBlue[i].getPosY());
                }
                */
                
                


                // test picking a piece, params for board: 10, 3, 5
                // piece at coords (1,4)
                // try to pick non-existent piece (in his own location)
  /*              Console.WriteLine("Non-piece picked (should return false): "+MyGlobals.bluePlayers[0].pickPiece());
                Console.WriteLine("Move player down to piece");
                MyGlobals.bluePlayers[0].MoveDown();
                Console.WriteLine("Current position of player: "+ MyGlobals.bluePlayers[0].getPosX()+" "+MyGlobals.bluePlayers[0].getPosY());
                Console.WriteLine("Valid piece picked (should return true): " + MyGlobals.bluePlayers[0].pickPiece());
                Console.WriteLine("Move player to right (coords of piece carried should change as well)");
                MyGlobals.bluePlayers[0].MoveRight();
                Console.WriteLine("Current position of player: " + MyGlobals.bluePlayers[0].getPosX() + " " + MyGlobals.bluePlayers[0].getPosY());
                Console.WriteLine("Position of piece after it was carried by player: "+MyGlobals.bluePlayers[0].getCarrying().getPosX()+" "+ MyGlobals.bluePlayers[0].getCarrying().getPosY());

                // UPDATE BoardView1 to reflect changes, animation


                // test placing at goal/ non-goal/ other team goal area (run one or the other only)
                
                // test placing a piece on goal at pos (2,9) in blue area
                Console.WriteLine("Try to place piece carried by blue payer in goals area, at valid goal cell (2,9)");
                Console.WriteLine("Success: "+MyGlobals.bluePlayers[0].tryPlacePiece(2, 9));

                // test placing a piece on non-goal at pos (2,8) in blue area
                //Console.WriteLine("Try to place piece carried by blue payer in goals area, at non-goal cell (2,8)");
                //Console.WriteLine("Success: " + MyGlobals.bluePlayers[0].tryPlacePiece(2, 8));

                // test placing a piece on at invalid pos (0,0) in red area
                //Console.WriteLine("Try to place piece carried by blue payer in red goals area, at invalid cell (0,0)");
                //Console.WriteLine("Success: " + MyGlobals.bluePlayers[0].tryPlacePiece(0, 0));
                */
                MyGlobals.boardView1.Closed += (s, args) => this.Close();
                MyGlobals.boardView1.Show();

                MyGlobals.gameMasterView = new GameMasterView();
                MyGlobals.gameMasterView.Show();
            }
        }

        private void BoardHeightTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void labelH_Click(object sender, EventArgs e)
        {

        }
    }
}
