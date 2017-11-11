using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareEngineering_project
{

    public partial class BoardView1 : Form
    {

        PictureBox test = new PictureBox();
        public BoardView1()
        {
            InitializeComponent();
            CreateBoard();
            AddPlayers();
            PreventFlickering();
        }

        public void CreateBoard()
        {
            BoardLayoutPanel.Controls.Clear();
            BoardLayoutPanel.RowStyles.Clear();
            BoardLayoutPanel.ColumnStyles.Clear();
            BoardLayoutPanel.RowCount = MyGlobals.Height;
            BoardLayoutPanel.ColumnCount = MyGlobals.Width;
            for(int i=0; i<BoardLayoutPanel.RowCount; i++)
                BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute,50));

            for (int i = 0; i < BoardLayoutPanel.ColumnCount; i++)
                BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,50));

  
            this.MaximumSize = new System.Drawing.Size((BoardLayoutPanel.ColumnCount * 50) + 16, (BoardLayoutPanel.RowCount * 50)+62);         
        }


        private void BoardLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

            //Color cells:
            if (e.Row < MyGlobals.smallHeight)
                e.Graphics.FillRectangle(Brushes.Red, e.CellBounds);
            else if (e.Row >= MyGlobals.Height - MyGlobals.smallHeight)
                e.Graphics.FillRectangle(Brushes.Blue, e.CellBounds);


            //Border:
            Graphics g = e.Graphics;
            Rectangle r = e.CellBounds;
            using (Pen pen = new Pen(Color.Black, 0 /*1px width despite of page scale, dpi, page units*/ ))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                // define border style
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

                // decrease border rectangle height/width by pen's width for last row/column cell
                if (e.Row == (BoardLayoutPanel.RowCount - 1))
                {
                    r.Height -= 1;
                }

                if (e.Column == (BoardLayoutPanel.ColumnCount - 1))
                {
                    r.Width -= 1;
                }

                // use graphics mehtods to draw cell's border
                e.Graphics.DrawRectangle(pen, r);
            }


        }

        public void AddPlayers()
        {
            BluePlayer bp = new BluePlayer();
           // PictureBox d = new PictureBox();
            test.BackgroundImage = bp.getBitmap();
            BoardLayoutPanel.Controls.Add(test, bp.getPosX(), bp.getPosY());
            MyGlobals.bluePlayers.Add(bp);

          /*  RedPlayer rp = new RedPlayer();
            PictureBox z = new PictureBox();
            z.Image = rp.getBitmap();
            BoardLayoutPanel.Controls.Add(z, rp.getPosX(), rp.getPosY());
            MyGlobals.redPlayers.Add(rp);
            */
        }

        public void PreventFlickering()
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, BoardLayoutPanel, new object[] { true });
        }

        private void BoardLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            //scroll using mousewheel
          //  BoardLayoutPanel.Focus();
        }

        private void BoardLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        // check if position is empty of any player
        public bool isFreeOfPlayer(int x, int y) {

            // check pos of blue players
            foreach (BluePlayer item in MyGlobals.bluePlayers) {
                if (item.getPosX() == x && item.getPosY() == y) {
                    return false;
                }
            }

            // check pos of red players
            foreach (RedPlayer item in MyGlobals.redPlayers)
            {
                if (item.getPosX() == x && item.getPosY() == y)
                {
                    return false;
                }
            }

            // cell free if no matching pos
            return true;
        }

        private void BoardLayoutPanel_MouseClick(Object sender, MouseEventArgs e) {
            Console.Write("Clicked");
        }

        private void BoardView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Left:
                    {
                        MoveLeft();
                        break;
                    }
                case Keys.Right:
                    {
                        MoveRight();
                        break;
                    }
                case Keys.Down:
                    {
                        MoveDown();
                        break;
                    }
                case Keys.Up:
                    {
                        MoveUp();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void MoveLeft()
        {
            BluePlayer bp = MyGlobals.bluePlayers.First();
            bp.MoveLeft();
            BoardLayoutPanel.Controls.Add(test, bp.getPosX(), bp.getPosY());
            MyGlobals.gameMasterView.ReadPlayers();
        }

        void MoveRight()
        {
            BluePlayer bp = MyGlobals.bluePlayers.First();
            bp.MoveRight();
            BoardLayoutPanel.Controls.Add(test, bp.getPosX(), bp.getPosY());
            MyGlobals.gameMasterView.ReadPlayers();
        }

        void MoveUp()
        {
            BluePlayer bp = MyGlobals.bluePlayers.First();
            bp.MoveUp();
            BoardLayoutPanel.Controls.Add(test, bp.getPosX(), bp.getPosY());
            MyGlobals.gameMasterView.ReadPlayers();
        }

        void MoveDown()
        {
            BluePlayer bp = MyGlobals.bluePlayers.First();
            bp.MoveDown();
            BoardLayoutPanel.Controls.Add(test, bp.getPosX(), bp.getPosY());
            MyGlobals.gameMasterView.ReadPlayers();
        }
    }
}
