using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Door : Panel
    {
        private bool locked = false; // flag for decorators to lock the door
        private Room room1;
        private Room room2;

        public Door(Question question)
        {

        }
        public bool isLocked() { return locked; }

        public Room knock(Room from)
        {
            if (!locked)
            {
                if (from == room1)
                    return room2;
                else if (from == room2)
                    return room1;
                else
                    throw new WallHackException();
            }
            else
                return from;
        }

        public Room kick(Room from) { // cheat to pass thru door
            locked = false;
            if (from == room1)
                return room2;
            else if (from == room2)
                return room1;
            else
                throw new WallHackException();
        }

        // constructors
        public Door(Room x, Room y)
        {
            room1 = x;
            room2 = y;
        }

        public Door(Room x, Room y, char dir) // dir is which door this will be on room x can be n, s, e ,w
        {
            room1 = x;
            room2 = y;
            switch (dir)
            {
                case 'n':
                    room1.north = this;
                    room2.south = this;
                    break;
                case 's':
                    room1.south = this;
                    room2.north = this;
                    break;
                case 'e':
                    room1.east = this;
                    room2.west = this;
                    break;
                case 'w':
                    room1.west = this;
                    room1.east = this;
                    break;
                default:
                    throw new ArgumentException("direction must be n, s, e || w");
            }
        }
    }
}
