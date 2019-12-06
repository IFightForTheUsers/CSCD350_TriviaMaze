using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TriviaMazeGUI.Tests
{
    [TestClass]
    public class IsExitTest
    {
        private Room room;
        [TestInitialize]
        public void SetUp()
        {
            room = new Room();
        }
        [TestCleanup]
        public void TearDown()
        {
            room = null;
        }
        [TestMethod]
        public void RoomIsExit()
        {
            Exit egress = new Exit(room);
            room.east = egress;
            Assert.IsTrue(UILock.Instance.IsExit(room));
        }
        [TestMethod]
        public void RoomDoorsNotDefined()
        {
            Assert.IsFalse(UILock.Instance.IsExit(room));
        }
        [TestMethod]
        public void RoomIsEntrance()
        {
            Entrance igress = new Entrance(room);
            room.east = igress;
            Assert.IsFalse(UILock.Instance.IsExit(room));
        }
        [TestMethod]
        public void RoomWestIsEntrance()
        {
            Entrance igress = new Entrance(room);
            room.west = igress;
            Assert.IsFalse(UILock.Instance.IsExit(room));
        }
        [TestMethod]
        public void RoomWestIsExit()
        {
            Exit egress = new Exit(room);
            room.west = egress;
            Assert.IsFalse(UILock.Instance.IsExit(room));
        }
    }
}
