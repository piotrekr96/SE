using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoftwareEngineering_project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;


namespace SoftwareEngineering_project.Tests
{
    [TestClass()]
    public class PieceTests
    {
        Mock<Piece> pieceMock;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            MyGlobals.Height = 15;
            MyGlobals.smallHeight = 3;
            MyGlobals.Width = 5;
            MyGlobals.nrGoals = 5;
            MyGlobals.nrPieces = 2;         
        }

        
        [TestInitialize()]
        public void Initialize()
        {
            // Set up 
            pieceMock  = new Mock<Piece>();   // mocked piece
        }
        

        [TestMethod()]
        public void PieceTest()
        {
            MyGlobals.rnd = new DeterministicRandom(new List<int> { 0, 2, 8}); // overrides Random()
            //Piece pi = new Piece();
            pieceMock.Setup(m => m.placePieceInit(It.IsAny<int>(), It.IsAny<int>())).Returns(true); // mocked method
            Piece pi = pieceMock.Object;

            Assert.AreEqual(false, pi.getSham()); // the piece should not be a sham
            Assert.AreEqual(2, pi.getPosX()); // first coord should be 2
            Assert.AreEqual(8, pi.getPosY()); // second coord should be 8
        }

        [TestMethod()]
        public void placePieceInitTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void findPieceTest()
        {
            Assert.Fail();
        }

 
    }
}