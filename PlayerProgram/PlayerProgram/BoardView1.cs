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
using MessageProject;

namespace PlayerProgram
{

    public partial class BoardView1 : Form
    {
        int myHeight, myWidth, smallHeight;
        public Player player;
        PictureBox test = new PictureBox();
     /*   public BoardView1()
        {
          
            InitializeComponent();
            CreateBoard();
            //AddPlayers();
            ShowPlayerGoals();
            PreventFlickering();
        }
        */
        public BoardView1(int width, int tasksHeight, int goalsHeight, Player p)
        {
            myHeight = 2 * goalsHeight + tasksHeight;
            myWidth = width;
            smallHeight = goalsHeight;
            player = p;
            InitializeComponent();
            CreateBoard();
            AddPlayers();
           // ShowPlayerGoals();
            PreventFlickering();
           // Console.WriteLine(p.colour +" " + p.getPosX()+ " " + p.getPosY());
        }

        public void CreateBoard()
        {
            BoardLayoutPanel.Controls.Clear();
            BoardLayoutPanel.RowStyles.Clear();
            BoardLayoutPanel.ColumnStyles.Clear();
            BoardLayoutPanel.Margin = new Padding(0);
            BoardLayoutPanel.Dock = DockStyle.Fill;
            BoardLayoutPanel.RowCount = myHeight;
            BoardLayoutPanel.ColumnCount = myWidth;
            for(int i=0; i<BoardLayoutPanel.RowCount; i++)
                BoardLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute,50));

            for (int i = 0; i < BoardLayoutPanel.ColumnCount; i++)
                BoardLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,50));

  
            this.MaximumSize = new System.Drawing.Size((BoardLayoutPanel.ColumnCount * 50) + 16, (BoardLayoutPanel.RowCount * 50)+62);

            // initialize array of distances
            MyGlobals.seenDistances = new int[myWidth, myHeight];
            for (int i = 0; i< myWidth; i++) {
                for (int j=0; j< myHeight; j++) {
                    MyGlobals.seenDistances[i, j] = -1;
                }
            }
        }


        private void BoardLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {

            //Color cells:
            if (e.Row < smallHeight)
                e.Graphics.FillRectangle(Brushes.Gray, e.CellBounds);
            else if (e.Row >= myHeight - smallHeight)
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

        public void AddPlayers()
        {
         
            // PictureBox d = new PictureBox();
            test.Image = player.getBitmap();
            test.Margin = new Padding(0);
            BoardLayoutPanel.Controls.Add(test, player.position_x, player.position_y);
           // MyGlobals.players.Add(bp);

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
        /*    foreach (Player item in MyGlobals.players) {
                if (item.getPosX() == x && item.getPosY() == y) {
                    return false;
                }
            }
*/
            // check pos of red players
           /* foreach (RedPlayer item in MyGlobals.redPlayers)
            {
                if (item.getPosX() == x && item.getPosY() == y)
                {
                    return false;
                }
            }
            */
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
                case Keys.P:
                    {
                        Player bp = MyGlobals.players.First();
                        bp.pickPiece();
                        break;
                    }
                case Keys.T:
                    {
                        Player bp = MyGlobals.players.First();
                        if (bp.getCarrying() != null) {
                            if (bp.testPiece())
                            {
                                Console.WriteLine("Piece is not sham.");
                            }
                            else {
                                Console.WriteLine("Piece is sham.");
                            }
                        }                       
                        break;
                    }
                case Keys.D:
                    {  
                        player.tryPlacePiece(player.position_x,player.position_y);
                        break;
                    }
                case Keys.M:
                    {
                        Player bp = MyGlobals.players.First();
                        bp.computeManDist();
                        ShowManhattanDistance();
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
            // MESSAGE, REQUEST A MOVE

            MessageProject.Move move = new Move(player.GameID,player.ID,MovementDirection.left);

            string MoveMessage = MessageProject.Message.messageIntoXML(move);
            byte[] toBytes = Encoding.ASCII.GetBytes(MoveMessage);
            try
            {
                Form1.master.Send(toBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            /*  player.MoveLeft();

              //remove previous label
             Control c = BoardLayoutPanel.GetControlFromPosition(player.position_x, player.position_y);
              BoardLayoutPanel.Controls.Remove(c);

              BoardLayoutPanel.Controls.Add(test, player.position_x, player.position_y);
              //bp.testDistConsole(); // just for checking
              ShowManhattanDistance();
              ShowPlayerGoals();*/
        }

        void MoveRight()
        {
            //MESSAGE, REQUEST A MOVE
            MessageProject.Move move = new Move(player.GameID, player.ID, MovementDirection.right);

            string MoveMessage = MessageProject.Message.messageIntoXML(move);
            byte[] toBytes = Encoding.ASCII.GetBytes(MoveMessage);
            try
            {
                Form1.master.Send(toBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            /*  player.MoveRight();

              //remove previous label
              Control c = BoardLayoutPanel.GetControlFromPosition(player.position_x, player.position_y);
              BoardLayoutPanel.Controls.Remove(c);

              BoardLayoutPanel.Controls.Add(test, player.position_x, player.position_y);
              //bp.testDistConsole(); // just for checking
              ShowManhattanDistance();
              ShowPlayerGoals();*/
        }

        void MoveUp()
        {
            //MESSAGE, REQUEST A MOVE
            MessageProject.Move move = new Move(player.GameID, player.ID, MovementDirection.up);

            string MoveMessage = MessageProject.Message.messageIntoXML(move);
            byte[] toBytes = Encoding.ASCII.GetBytes(MoveMessage);
            try
            {
                Form1.master.Send(toBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            /*  player.MoveUp();
              //remove previous label
              Control c = BoardLayoutPanel.GetControlFromPosition(player.position_x, player.position_y);
              BoardLayoutPanel.Controls.Remove(c);

              BoardLayoutPanel.Controls.Add(test, player.position_x, player.position_y);
              //bp.testDistConsole(); // just for checking
              ShowManhattanDistance();
              ShowPlayerGoals();*/
        }

        void MoveDown()
        {
            //MESSAGE, REQUEST A MOVE
            MessageProject.Move move = new Move(player.GameID, player.ID, MovementDirection.down);

            string MoveMessage = MessageProject.Message.messageIntoXML(move);
            byte[] toBytes = Encoding.ASCII.GetBytes(MoveMessage);
            try
            {
                Form1.master.Send(toBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            /* player.MoveDown();
             //remove previous label
             Control c = BoardLayoutPanel.GetControlFromPosition(player.position_x, player.position_y);
             BoardLayoutPanel.Controls.Remove(c);

             BoardLayoutPanel.Controls.Add(test, player.position_x, player.position_y);
             //bp.testDistConsole(); // just for checking
             ShowManhattanDistance();
             ShowPlayerGoals();
             */
        }

        public void ShowManhattanDistance()
        {
            Player p = player;

                //p.computeManDist();
                for (int i =0; i < MyGlobals.seenDistances.GetLength(0); i++)
                {
                    
                    for (int j = smallHeight; j < MyGlobals.seenDistances.GetLength(1) - smallHeight; j++)
                        {
                            Label l = new Label() {
                            AutoSize = false,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Dock = DockStyle.Fill,
                    };
                    l.Margin = new Padding(2);


                    // if no piece on the board, show nothing
                    if (MyGlobals.seenDistances[i, j] == -1)
                    {
                        l.Text = "";
                    }
                    // if a piece exists, show distance
                    else {
                        l.Text = MyGlobals.seenDistances[i, j].ToString();
                    }

                    Control c = BoardLayoutPanel.GetControlFromPosition(i, j);
                   /* foreach (Player item in MyGlobals.players) {
                        if (item.getPosX() == i && item.getPosY() == j)
                        {
                            continue;
                        }
                        if (c != null) {
                            BoardLayoutPanel.Controls.Remove(c);
                        }                  
                        BoardLayoutPanel.Controls.Add(l, i, j);
                    }
                    */
                }
                }

            

        }


        public void ShowPlayerGoals()
        {

            //Player p = MyGlobals.players.First();
            List<Goal> myGoals;
            if (player.colour == MessageProject.Team.blue)
            {
                myGoals = MyGlobals.goalsBlue;
            }
            else {
                myGoals = MyGlobals.goalsRed;
            }

            foreach (Goal g in myGoals) {
                if (g.getDiscoveror() == player) {
                    
                    // retrieve goal and its bitmap 
                    PictureBox temp;
                    temp = new PictureBox();
                    temp.Image = g.getDiscoveredBitmap();
                    temp.Margin = new Padding(0);

                    // only add bitmap if table cell is empty
                    Control c = BoardLayoutPanel.GetControlFromPosition(g.getPosX(), g.getPosY());
                    if (c == null)
                    {
                        BoardLayoutPanel.Controls.Add(temp, g.getPosX(), g.getPosY());
                    }
                }
            }

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
