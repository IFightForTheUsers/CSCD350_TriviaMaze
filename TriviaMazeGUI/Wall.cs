using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    class Wall : Panel
    {
        private Room room;

        public bool locked { get { return true; } set { }  }

        public int depth { get { return 1; } }

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
            // just a stupid hash for debug testing to show isntance
            return "Wall@" + this.GetHashCode().ToString();
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
