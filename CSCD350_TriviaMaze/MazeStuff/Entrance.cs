using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Enterance class.
 * all attempts to leave return the class it points to
 */

namespace TriviaMaze
{
    class Entrance : Panel
    {
        private Room room;

        public bool locked { get { return true; } }

        public Room knock(Room from)
        {
            return from;
        }

        public Room kick(Room from)
        {
            return from;
        }

        public Room ghost(Room from)
        {
            return null;
        }

        public void setUIState(Room from)
        {
            //
        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show isntance
            return "Entrance@"+this.GetHashCode().ToString();
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
