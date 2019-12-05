using System;

namespace TriviaMazeGUI.Panels
{
    [Serializable]
    class Door : Panel
    {
        private bool _locked = false; // flag for decorators to lock the door
        public bool locked
        {
            get => _locked;
            set
            {
                if (value == true)
                    _locked = true;
            }
        }

        public int depth => 1;
        private readonly Room room1;
        private readonly Room room2;

        public Room knock(Room from)
        {
            if (!_locked)
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

        public Room ghost(Room from)
        {
            if (from == room1)
                return room2;
            else if (from == room2)
                return room1;
            else
                throw new WallHackException();
        }

        public Room kick(Room from) { // cheat to pass thru door
            _locked = false;
            if (from == room1)
                return room2;
            else if (from == room2)
                return room1;
            else
                throw new WallHackException();
        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show isntance
            return "Door@" + this.GetHashCode().ToString();
        }

        public void rewire(Panel obj)
        {
            if (room1.north == this)
            {
                room1.north = obj;
                room2.south = obj;
            }
            else if (room1.south == this)
            {
                room1.south = obj;
                room2.north = obj;
            }
            else if (room1.east == this)
            {
                room1.east = obj;
                room2.west = obj;
            }
            else
            {
                room1.west = obj;
                room2.east = obj;
            }
        }

        // constructors
        public Door(Room x, Room y)
        {
            _locked = false;
            room1 = x;
            room2 = y;
        }

        public Door(Room room1, Room room2, char dir) // dir is which door this will be on room x can be n, s, e ,w
        {
            this.room1 = room1;
            this.room2 = room2;
            _locked = false;
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
                    room2.east = this;
                    break;
                default:
                    throw new ArgumentException("direction must be n, s, e || w");
            }
        }
    }
}
