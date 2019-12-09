/* Entrance class.
 * all attempts to leave return the class it points to
 */

using System;

namespace TriviaMazeGUI.Panels
{
    [Serializable]
    class Entrance : Panel
    {
        private Room room;

        public bool locked { get => true; set { } }

        public int depth => 1;

        public Room Knock(Room from)
        {
            return from;
        }

        public Room Kick(Room from)
        {
            return from;
        }

        public Room Ghost(Room from)
        {
            if (from == null)
                return room;
            else
                return null;
        }

        public override string ToString() => "Entrance@" + GetHashCode().ToString();

        public void Rewire(Panel obj)
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
