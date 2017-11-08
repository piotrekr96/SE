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
                MyGlobals.boardView1.Closed += (s, args) => this.Close();

                MyGlobals.boardView1.ShowDialog();
            }
        }
    }
}
