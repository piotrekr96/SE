using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareEngineering_project;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineering_project.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        static Player redP, blueP;
        static Piece piece1;

        [ClassInitialize()]
        public static void ClassInit(TestContext context) {
            
            MyGlobals.Height = 15;
            MyGlobals.smallHeight = 3;
            MyGlobals.Width = 5;
            MyGlobals.nrGoals = 5;
            MyGlobals.nrPieces = 2;
            MyGlobals.boardView1 = new BoardView1();
        }

        [TestInitialize()]
        public void Initialize() {
            MyGlobals.rnd = new Random(1);
            
            redP = new Player('r');
            blueP = new Player('b');
            piece1 = new Piece();

            //Red player coords: 1,3
            //Blue player coords: 2,9
            //Piece1 coods: 2,5
        }

        [TestCleanup()]
        public void Cleanup() {
            MyGlobals.players.Clear();
            MyGlobals.pieces.Clear();
            redP = null;
            blueP = null;
            piece1 = null;
        }

        [TestMethod()]
        public void PlayerTestRed()
        {
            Assert.AreEqual(redP.getColour(), 'r');
            Assert.IsNull(redP.getCarrying());
            Debug.WriteLine("Coords of red player: " + redP.getPosX() + "," + redP.getPosY());
            Debug.WriteLine("Coords of piece: " + piece1.getPosX() + "," + piece1.getPosY());
        }

        [TestMethod()]
        public void PlayerTestBlue()
        {
            Assert.AreEqual(blueP.getColour(), 'b');
            Assert.IsNull(blueP.getCarrying());
            Debug.WriteLine("Coords of blue player: " + blueP.getPosX() + "," + blueP.getPosY());
        }

        [TestMethod()]
        public void getPosXTestRed()
        {
            Debug.WriteLine("Coords of red player: " + redP.getPosX() + "," + redP.getPosY());
            Assert.IsNotNull(redP.getPosX());
            Assert.AreEqual(1, redP.getPosX());           
        }

        [TestMethod()]
        public void getPosYTestRed()
        {
            Debug.WriteLine("Coords of red player: " + redP.getPosX() + "," + redP.getPosY());
            Assert.IsNotNull(redP.getPosY());
            Assert.AreEqual(3, redP.getPosY());
        }

        [TestMethod()]
        public void getPosXTesBlue()
        {
            Debug.WriteLine("Coords of blue player: " + blueP.getPosX() + "," + blueP.getPosY());
            Assert.IsNotNull(blueP.getPosX());
            Assert.AreEqual(2, blueP.getPosX());
        }

        [TestMethod()]
        public void getPosYTestBlue()
        {
            Debug.WriteLine("Coords of blue player: " + blueP.getPosX() + "," + blueP.getPosY());
            Assert.IsNotNull(blueP.getPosY());
            Assert.AreEqual(9, blueP.getPosY());
        }

        [TestMethod()]
        public void setPosXTestRed()
        {
            redP.setPosX(0);
            Assert.AreEqual(0, redP.getPosX());           
        }

        [TestMethod()]
        public void setPosYTestRed()
        {
            redP.setPosY(0);
            Assert.AreEqual(0, redP.getPosY());
        }

        [TestMethod()]
        public void getColourTest()
        {
            Assert.AreEqual(redP.getColour(),'r');
            Assert.AreNotEqual(redP.getColour(),'b');
        }

        [TestMethod()]
        public void withinBoardBoundsTest()
        {
            Assert.IsTrue(redP.withinBoardBounds(1,1));
            Assert.IsFalse(redP.withinBoardBounds(MyGlobals.Width, MyGlobals.Height));
        }

        [TestMethod()]
        public void withinPlayerBoundsTestRed()
        {
            // move to cell right before border with blue player goals area
            Assert.IsTrue(redP.withinPlayerBounds(MyGlobals.Height - MyGlobals.smallHeight - 1));
            // try to step into blue player goals area
            Assert.IsFalse(redP.withinPlayerBounds(MyGlobals.Height - MyGlobals.smallHeight));
        }

        [TestMethod()]
        public void withinPlayerBoundsTestBlue()
        {
            // move to cell right before border with red player goals area
            Assert.IsTrue(blueP.withinPlayerBounds(MyGlobals.smallHeight));
            // try to step into red player goals area
            Assert.IsFalse(blueP.withinPlayerBounds(MyGlobals.smallHeight-1));
        }

        [TestMethod()]
        public void canMoveTest()
        {
            // red tries to move over the blue player
            Assert.IsFalse(redP.canMove(blueP.getPosX(), blueP.getPosY()));
            // blue tries to move over the red player
            Assert.IsFalse(blueP.canMove(redP.getPosX(), redP.getPosY()));
        }

        [TestMethod()]
        public void MoveUpTest()
        {
            //redP at 1,3 will move at 1,2
            Assert.IsTrue(redP.MoveUp());
            Assert.AreEqual(2,redP.getPosY());
        }

        [TestMethod()]
        public void MoveDownTest()
        {
            //redP at 1,3 will move at 1,4
            Assert.IsTrue(redP.MoveDown());
            Assert.AreEqual(4, redP.getPosY());
        }

        [TestMethod()]
        public void MoveLeftTest()
        {
            //redP at 1,3 will move at 0,3
            Assert.IsTrue(redP.MoveLeft());
            Assert.AreEqual(0, redP.getPosX());
        }

        [TestMethod()]
        public void MoveRightTest()
        {
            //redP at 1,3 will move at 2,3
            Assert.IsTrue(redP.MoveRight());
            Assert.AreEqual(2, redP.getPosX());
        }

        [TestMethod()]
        public void pickPieceTest()
        {
            Assert.IsFalse(redP.pickPiece()); // no piece at coords 1,3

            // move player to cell containing piece
            redP.setPosX(2);
            redP.setPosY(5);
            Assert.IsTrue(redP.pickPiece());
            Assert.IsNotNull(redP.getCarrying());
        }

        [TestMethod()]
        public void testPieceTest()
        {
            // move player to cell containing piece and pick it
            redP.setPosX(2);
            redP.setPosY(5);

            // the piece is a sham
            piece1.setSham(true);

            redP.pickPiece();
            Assert.IsFalse(redP.testPiece());
            // testPiece() returns true is piece is true
            // returns false if piece is sham

            // piece not sham
            piece1.setSham(false);
            Assert.IsTrue(redP.testPiece());

        }

        [TestMethod()]
        public void getCarryingTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void getBitmapTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void canPlacePlayerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void canPlacePieceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void placePieceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void tryPlacePieceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void discoverGoalTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void computeManDistTest()
        {
            Assert.Fail();
        }

    }
}