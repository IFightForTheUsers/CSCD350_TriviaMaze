using System;
using System.CodeDom;
using System.Runtime.Serialization;
using System.Windows.Controls;
using Panel = TriviaMazeGUI.Panels.Panel;

namespace TriviaMazeGUI
{
    [Serializable()]
    class Room
    {
        [NonSerialized()] internal Button button;
        internal Panel north;
        internal Panel south;
        internal Panel east;
        internal Panel west;
        public bool Flag { get; set; }
        private bool _visted = false;
        public bool Visited
        {
            get => _visted;
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

        public int CompareTo(object obj)
        {
            if (obj == null || this == null)
            {
                return 1;
            }
            if (obj is Room otherRoom)
            {

                if (otherRoom.east != this.east)
                {
                    return 1;
                }
                if (otherRoom.west != this.west)
                {
                    return 1;
                }
                if (otherRoom.south != this.south)
                {
                    return 1;
                }
                if (otherRoom.north != this.north)
                {
                    return 1;
                }
            }
            else
            {
                throw new NotImplementedException("Room.CompareTo only compares Rooms with Rooms");
            }
            return 0;
        }
    }
}
