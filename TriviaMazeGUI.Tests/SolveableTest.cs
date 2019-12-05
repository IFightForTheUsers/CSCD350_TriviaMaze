// <copyright file="SolveableTest.cs">Copyright ©  2019</copyright>
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriviaMazeGUI;
using TriviaMazeGUI.Panels;

namespace TriviaMazeGUI.Tests
{
    /// <summary>This class contains parameterized unit tests for Solveable</summary>
    [TestClass]
    public class SolveableTest
    {
        private Room[,] maze;
        private Entrance ingress;
        internal Entrance Entry { get { return ingress; } }
        private Exit egress;

        [TestInitialize]
        public void SetUp()
        {
            maze = new Room[4, 4];
            Entrance ingress;
            Exit egress;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    maze[i, j] = new Room();
                }
            }
            for (int i = 0; i < 4; i++)
            { // add walls to maze edges
                maze[i, 0].north = new Wall(maze[i, 0]);
                maze[0, i].west = new Wall(maze[0, i]);
                maze[4 - 1, i].east = new Wall(maze[4 - 1, i]);
                maze[i, 4 - 1].south = new Wall(maze[i, 4 - 1]);
            }
            for (int y = 0; y < 4 - 1; y++)
            { // now we add doors
                for (int x = 0; x < 4; x++)
                {
                    _ = new Door(maze[x, y], maze[x, y + 1], 'e');
                }
            }
            for (int y = 0; y < 4; y++)
            { // now we add doors
                for (int x = 0; x < 4 - 1; x++)
                {
                    _ = new Door(maze[x, y], maze[x + 1, y], 's');
                }
            }
            // hard add the entry and exit
            ingress = new Entrance(maze[0, 0]);
            maze[0, 0].west = ingress;
            egress = new Exit(maze[4 - 1, 4 - 1]);
            maze[4 - 1, 4 - 1].east = egress;
        }
        [TestCleanup]
        public void TearDown()
        {
            maze = null;
            ingress = null;
            egress = null;
            Solveable.Reset();
        }
        [TestMethod]
        public void BeginningDoorsLocked()
        {
            //bool result = Solveable.CheckIfSolveable(maze);

            maze[0, 0].east.locked = true;
            maze[0, 0].south.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsFalse(test);
            maze = null;
            egress = null;
            ingress = null;
            // TODO: add assertions to method SolveableTest.CheckIfSolveableTest(Room[,])
        }
        [TestMethod]
        public void NoRoomsLocked()
        {
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsTrue(test);
        }
        [TestMethod]
        public void DiagnoalMaze()
        {
            maze[0, 0].south.locked = true;
            maze[0, 1].east.locked = true;
            maze[2, 1].north.locked = true;
            maze[2, 1].east.locked = true;
            maze[3, 2].north.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsTrue(test);

        }
        [TestMethod]
        public void ExitRoomLocked()
        {
            maze[3, 3].west.locked = true;
            maze[3, 3].north.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsFalse(test);

        }
        [TestMethod]
        public void RoomsSurrondingStartAreLocked()
        {
            maze[1, 0].south.locked = true;
            maze[1, 0].east.locked = true;
            maze[0, 0].east.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsFalse(test);

        }
        [TestMethod]
        public void LShapedMaze()
        {
            maze[0, 0].east.locked = true;
            maze[1, 0].east.locked = true;
            maze[2, 0].east.locked = true;
            maze[3, 1].north.locked = true;
            maze[3, 2].north.locked = true;
            maze[3, 3].north.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsTrue(test);

        }
        [TestMethod]
        public void CenterFourSqauresLocked()
        {
            maze[1, 1].west.locked = true;
            maze[1, 1].north.locked = true;
            maze[1, 2].north.locked = true;
            maze[1, 2].east.locked = true;
            maze[2, 1].west.locked = true;
            maze[2, 1].south.locked = true;
            maze[2, 2].south.locked = true;
            maze[2, 2].east.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void FinalColumnWestLocked()
        {
            maze[0, 3].west.locked = true;
            maze[1, 3].west.locked = true;
            maze[2, 3].west.locked = true;
            maze[3, 3].west.locked = true;

            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsFalse(test);

        }
        [TestMethod]
        public void OneDoorLocked()
        {
            maze[0, 0].east.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsTrue(test);

        }
        [TestMethod]
        public void FinalRowNorthLocked()
        {
            maze[3, 0].north.locked = true;
            maze[3, 1].north.locked = true;
            maze[3, 2].north.locked = true;
            maze[3, 3].north.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsFalse(test);

        }
        [TestMethod]
        public void LShapedMazeDifferentDirectoin()
        {

            maze[0, 0].south.locked = true;
            maze[0, 1].south.locked = true;
            maze[0, 2].south.locked = true;

            maze[1, 3].west.locked = true;
            maze[2, 3].west.locked = true;
            bool test = Solveable.CheckIfSolveable(ref maze);
            Assert.IsTrue(test);

        }
    }
}
