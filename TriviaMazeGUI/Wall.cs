using System;

namespace TriviaMazeGUI
{
    [Serializable]
    class Wall : Panel
    {
        private readonly Room room;

        public bool locked { get => true; set { } }

        public int depth => 1;

        public Room knock(Room from)
        {
            if (from == room)
                return from;
            else
                throw new WallHackException();
        }

        public Room kick(Room from)
        {
            if (from == room)
                return from;
            else
                throw new WallHackException();
        }

        public Room ghost(Room from)
        {
            return null;
        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show instance
            return "Wall@" + GetHashCode().ToString();
        }

        public void rewire(Panel obj)
        {
            if (room.north == this)
                room.north = obj;
            else if (room.south == this)
                room.south = obj;
            else if (room.west == this)
                room.west = obj;
            else
                room.east = obj;
        }

        public Wall(Room owner)
        {
            room = owner;
        }
    }
}
