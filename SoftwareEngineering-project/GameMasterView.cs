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
    public partial class GameMasterView : Form
    {
        PictureBox test = new PictureBox();
        public GameMasterView()
        {
            InitializeComponent();
            CreateBoard();
            PreventFlickering();
            ReadPlayers();
            CreateGoals(MyGlobals.nrGoals);
            AddGoals();
            Console.WriteLine("Added goals");
            CreatePieces(MyGlobals.nrPieces);
            AddPieces();
            Console.WriteLine("Added pieces");
        }

        private void BoardLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            //Color cells:
            if (e.Row < MyGlobals.smallHeight)
                e.Graphics.FillRectangle(Brushes.Gray, e.CellBounds);
            else if (e.Row >= MyGlobals.Height - MyGlobals.smallHeight)
                e.Graphics.FillRectangle(Brushes.Gray, e.CellBounds);


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


        public void CreateBoard()
        {
            BoardLayoutPanel.Controls.Clear();
            BoardLayoutPanel.RowStyles.Clear();
            BoardLayoutPanel.ColumnStyles.Clear();
            BoardLayoutPanel.RowCount = MyGlobals.Height;
            BoardLayoutPanel.ColumnCount = MyGlobals.Width;
            for (int i = 0; i < BoardLayoutPanel.RowCount; i++)
                BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            for (int i = 0; i < BoardLayoutPanel.ColumnCount; i++)
                BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));


            this.MaximumSize = new System.Drawing.Size((BoardLayoutPanel.ColumnCount * 50) + 16, (BoardLayoutPanel.RowCount * 50) + 62);
        }

        public void PreventFlickering()
        {
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, BoardLayoutPanel, new object[] { true });
        }

        public void ReadPlayers()
        {
            Player p = MyGlobals.players.First();
            test.Image = p.getBitmap();
            test.Margin = new Padding(0);

            // if thre is already a bitmap on the cell, remove it first
            // then add the player's bitmap
            Control c = BoardLayoutPanel.GetControlFromPosition(p.getPosX(), p.getPosY());
            BoardLayoutPanel.Controls.Remove(c);
            BoardLayoutPanel.Controls.Add(test, p.getPosX(), p.getPosY());

        }

        public void CreateGoals(int n) {
            for (int i = 0; i < n; i++)
            {
                new Goal();
            }
        }

        public void CreatePieces(int n) {
            for (int i = 0; i < n; i++)
            {
                Piece p = new Piece();
                MyGlobals.pieces.Add(p);
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Position of piece in task area: " + MyGlobals.pieces[i].getPosX() + " " + MyGlobals.pieces[i].getPosY() + " sham: " + MyGlobals.pieces[i].getSham());

            }
        }

        public void AddGoals() {
            for (int i =0; i< MyGlobals.nrGoals; i++) {

                // retrieve goal and its bitmap (Blue)
                PictureBox temp;
                Goal blueG = MyGlobals.goalsBlue[i];
                temp = new PictureBox();
                if (blueG.getDiscovered())
                {
                    temp.Image = blueG.getDiscoveredBitmap();
                }
                else
                {
                    temp.Image = blueG.getBitmap();
                }
                temp.Margin = new Padding(0);
                
                // only add bitmap if table cell is empty
                Control c = BoardLayoutPanel.GetControlFromPosition(blueG.getPosX(), blueG.getPosY());
                if (c == null)
                {
                    BoardLayoutPanel.Controls.Add(temp, blueG.getPosX(), blueG.getPosY());
                }

                // retrieve goal and its bitmap (Red)
                Goal redG = MyGlobals.goalsRed[i];
                temp = new PictureBox();
                if (redG.getDiscovered())
                {
                    temp.Image = redG.getDiscoveredBitmap();
                }
                else {
                    temp.Image = redG.getBitmap();
                }
                temp.Margin = new Padding(0);

                // only add bitmap if table cell is empty
                c = BoardLayoutPanel.GetControlFromPosition(redG.getPosX(), redG.getPosY());
                if (c == null)
                {
                    BoardLayoutPanel.Controls.Add(temp, redG.getPosX(), redG.getPosY());
                }
            }

        }

        public void AddPieces()
        {
            for (int i = 0; i < MyGlobals.nrPieces; i++)
            {

                // retrieve piece and its bitmap (Blue)
                PictureBox temp;
                Piece pi = MyGlobals.pieces[i];
                temp = new PictureBox(); 
                temp.Image = pi.getBitmap();
                temp.Margin = new Padding(0);

                // only add bitmap if table cell is empty
                Control c = BoardLayoutPanel.GetControlFromPosition(pi.getPosX(), pi.getPosY());
                if (c == null)
                {
                    BoardLayoutPanel.Controls.Add(temp, pi.getPosX(), pi.getPosY());
                }
            }

        }


        private void BoardLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
