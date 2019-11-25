using System.Windows.Controls;

namespace TriviaMazeGUI
{
    class Room
    {
        internal Button button;
        internal Panel north;
        internal Panel south;
        internal Panel east;
        internal Panel west;

        public override string ToString()
        {
            // just a stupid hash for debug testing to show instance
            return "Room@" + this.GetHashCode().ToString();
        }

        public Room()
        {
        }
    }
}
