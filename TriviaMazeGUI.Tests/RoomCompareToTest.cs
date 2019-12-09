using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TriviaMazeGUI.Panels;

namespace TriviaMazeGUI.Tests
{
    [TestClass]
    public class RoomCompareToTest
    {
        private Room room1;
        private Room room2;
        [TestInitialize]
        public void SetUp()
        {
            room1 = new Room();
            room2 = new Room();

            room1.east = new Wall(room1);
            room1.west = new Wall(room1);
            room1.north = new Wall(room1);
            room1.south = new Wall(room1);

            room2.east = new Wall(room2);
            room2.west = new Wall(room2);
            room2.north = new Wall(room2);
            room2.south = new Wall(room2);
        }
        [TestCleanup]
        public void TearDown()
        {
            room1 = null;
            room2 = null;
        }
        [TestMethod]
        public void RoomsAreEqual()
        {
            room2 = room1;
            Assert.IsTrue(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void Room2IsNull()
        {
            room2 = null;
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void Room1IsExit()
        {
            Exit egress = new Exit(room1);
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void Room2IsExit()
        {
            Exit egress = new Exit(room2);
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void Room1IsEntrance()
        {
            Entrance igress = new Entrance(room1);
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void Room2IsEntrance()
        {
            Entrance igress = new Entrance(room2);
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void BotExitNotEqual()
        {
            Exit egress = new Exit(room1);
            Exit egress1 = new Exit(room2);
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }
        [TestMethod]
        public void BotEntranceNotEqual()
        {
            Entrance igress = new Entrance(room1);
            Entrance igress1 = new Entrance(room2);
            Assert.IsFalse(room1.CompareTo(room2) == 0);
        }

    }
}
