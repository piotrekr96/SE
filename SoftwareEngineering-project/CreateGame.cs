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

            if (isNumber == false || isNumber2 == false || isNumber3 == false)
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

            this.Hide();

            if (MyGlobals.boardView1 == null)
            {
                MyGlobals.boardView1 = new BoardView1();
                /*  Console.WriteLine("BoardView constructor finished");
                  MyGlobals.boardView1.AddPlayers();
               */

                // test goals
                int NrGoals = 5;
                for (int i = 0; i < NrGoals; i++) {
                    new Goal();
                }

                for (int i = 0; i < NrGoals; i++)
                {
                    Console.WriteLine("Positions of goal in red area: " + MyGlobals.goalsRed[i].getPosX() + " " + MyGlobals.goalsRed[i].getPosY());
                    Console.WriteLine("Positions of goal in blue area: "+MyGlobals.goalsBlue[i].getPosX()+" "+ MyGlobals.goalsBlue[i].getPosY());
                }

                // test pieces
                int NrPieces = 6;
                for (int i = 0; i < NrPieces; i++)
                {
                    new Piece();
                }
                for (int i=0; i<NrPieces; i++) {
                    Console.WriteLine("Position of piece in task area: " + MyGlobals.pieces[i].getPosX() + " " + MyGlobals.pieces[i].getPosY()+" type: "+MyGlobals.pieces[i].getSham());

                }

                // test picking a piece, params for board: 10, 3, 5
                // piece at coords (1,4)
                // try to pick non-existent piece (in his own location)
                Console.WriteLine("Non-piece picked (should return false): "+MyGlobals.bluePlayers[0].pickPiece());
                Console.WriteLine("Move player down to piece");
                MyGlobals.bluePlayers[0].MoveDown();
                Console.WriteLine("Current position of player: "+ MyGlobals.bluePlayers[0].getPosX()+" "+MyGlobals.bluePlayers[0].getPosY());
                Console.WriteLine("Valid piece picked (should return true): " + MyGlobals.bluePlayers[0].pickPiece());
                Console.WriteLine("Move player to right (coords of piece carried should change as well)");
                MyGlobals.bluePlayers[0].MoveRight();
                Console.WriteLine("Current position of player: " + MyGlobals.bluePlayers[0].getPosX() + " " + MyGlobals.bluePlayers[0].getPosY());
                Console.WriteLine("Position of piece after it was carried by player: "+MyGlobals.bluePlayers[0].getCarrying().getPosX()+" "+ MyGlobals.bluePlayers[0].getCarrying().getPosY());

                // UPDATE BoardView1 to reflect changes, animation


                MyGlobals.boardView1.Closed += (s, args) => this.Close();
                MyGlobals.boardView1.ShowDialog();
            }
        }
    }
}
