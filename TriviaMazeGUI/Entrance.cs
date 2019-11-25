/* Entrance class.
 * all attempts to leave return the class it points to
 */

namespace TriviaMazeGUI
{
    class Entrance : Panel
    {
        private Room room;

        public bool locked { get => true; set { } }

        public int depth => 1;

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
            if (from == null)
                return room;
            else
                return null;
        }

        public override string ToString() => "Entrance@" + GetHashCode().ToString();

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
