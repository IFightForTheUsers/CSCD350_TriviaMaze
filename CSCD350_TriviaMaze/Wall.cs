using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Wall : Panel
    {
        private Room room;

        public bool isLocked() { return true;  }

        public Room knock(Room from)
        {
            if (from == room)
                return from;
            else
                throw new WallHackException();
        }

        public Room kick(Room from) {
            if (from == room)
                return from;
            else
                throw new WallHackException();
        }

        public Wall(Room owner)
        {
            room = owner;
        }
    }
}
