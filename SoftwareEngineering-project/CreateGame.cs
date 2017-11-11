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
               

                // test movement
                Console.WriteLine("Red player pos before moving: " + MyGlobals.redPlayers[0].getPosX() + " " + MyGlobals.redPlayers[0].getPosY());
                MyGlobals.redPlayers[0].MoveDown();
                Console.WriteLine("Red player pos after moving down: " + MyGlobals.redPlayers[0].getPosX() + " " + MyGlobals.redPlayers[0].getPosY());
                MyGlobals.redPlayers[0].MoveLeft();
                Console.WriteLine("Red player pos after moving left: " + MyGlobals.redPlayers[0].getPosX() + " " + MyGlobals.redPlayers[0].getPosY());
                MyGlobals.redPlayers[0].MoveRight();
                Console.WriteLine("Red player pos after moving right: " + MyGlobals.redPlayers[0].getPosX() + " " + MyGlobals.redPlayers[0].getPosY());

                Console.WriteLine("Blue player pos before moving: " + MyGlobals.bluePlayers[0].getPosX() + " " + MyGlobals.bluePlayers[0].getPosY());
                MyGlobals.bluePlayers[0].MoveUp();
                Console.WriteLine("Blue player pos after moving up: " + MyGlobals.bluePlayers[0].getPosX() + " " + MyGlobals.bluePlayers[0].getPosY());
                MyGlobals.bluePlayers[0].MoveLeft();
                Console.WriteLine("Blue player pos after moving left: " + MyGlobals.bluePlayers[0].getPosX() + " " + MyGlobals.bluePlayers[0].getPosY());
                MyGlobals.bluePlayers[0].MoveRight();
                Console.WriteLine("Blue player pos after moving right: " + MyGlobals.bluePlayers[0].getPosX() + " " + MyGlobals.bluePlayers[0].getPosY());
                */


                MyGlobals.boardView1.Closed += (s, args) => this.Close();
                MyGlobals.boardView1.ShowDialog();
            }
        }
    }
}
