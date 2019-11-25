/* Exit class.
 * all attempts to leave return null
 */

namespace TriviaMazeGUI
{
    class Exit : Panel
    {
        private Room room;

        public bool locked { get => true; set { } }

        public int depth => 1;

        public Room knock(Room from)
        {
            return null;
        }

        public Room ghost(Room from)
        {
            return null;
        }

        public Room kick(Room from)
        {
            return null;
        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show instance
            return "Exit@" + this.GetHashCode().ToString();
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

        public Exit(Room owner)
        {
            room = owner;
        }
    }
}
