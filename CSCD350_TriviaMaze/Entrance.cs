using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Entrance : Panel
    {
        private Room room;

        public bool isLocked() { return true; }

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

        public Entrance(Room owner)
        {
            room = owner;
        }
    }
}
