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
        private bool flag = false;

        public override string ToString()
        {
            // just a stupid hash for debug testing to show isntance
            return "Room@" + this.GetHashCode().ToString();
        }

        public Room()
        {
        }

        public bool Flag { get { return flag; } set { flag = value; } }

        public int CompareTo(object obj)
        {
            if(obj == null)
            {
                return 1;
            }
            Room otherRoom = obj as Room;
            if(otherRoom != null)
            {
                
                if(!(otherRoom.east == this.east))
                {
                    return 1;
                }
                if (!(otherRoom.west == this.west))
                {
                    return 1;
                }
                if (!(otherRoom.south == this.south))
                {
                    return 1;
                }
                if (!(otherRoom.north == this.north))
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
