using System;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace TriviaMazeGUI
{
    [Serializable()]
    class Room
    {
        [NonSerialized()]internal Button button;
        internal Panel north;
        internal Panel south;
        internal Panel east;
        internal Panel west;
        private bool flag = false;
        private bool _visted = false;
        public bool Visited
        {
            get { return _visted;}
            set
            {
                if (value == true) _visted = true;
            }

        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show instance
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
