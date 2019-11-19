using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            // just a stupid hash for debug testing to show isntance
            return "Room@" + this.GetHashCode().ToString();
        }

        public Room()
        {
        }
    }
}
