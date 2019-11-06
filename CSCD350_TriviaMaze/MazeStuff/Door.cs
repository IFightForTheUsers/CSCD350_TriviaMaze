using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    class Door : Panel
    {
        private bool _locked = false; // flag for decorators to lock the door
        public bool locked { get; }
        private Room room1;
        private Room room2;
        public bool isLocked() { return locked; }

        public Room knock(Room from)
        {
            if (!locked)
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

        public void setUIState(Room from)
        {
            if (from == room1)
            {// change room2 button
                if (_locked)
                {
                    room2.button.IsEnabled = false;
                    room2.button.Background = Regulations.disabledColor;
                    room2.button.Content = null;
                }
                else
                {
                    room2.button.IsEnabled = true;
                    room2.button.Background = Regulations.validMoveColor;
                    room2.button.Content = null;
                }
            }
                
            else if (from == room2)
            {// change room1 button
                if (_locked)
                {
                    room1.button.IsEnabled = false;
                    room1.button.Background = Regulations.disabledColor;
                    room1.button.Content = null;
                }
                else
                {
                    room1.button.IsEnabled = true;
                    room1.button.Background = Regulations.validMoveColor;
                    room1.button.Content = null;
                }
            }
            else if (from == null)
            {
                room1.button.IsEnabled = false;
                room2.button.IsEnabled = false;
            }
            else
                throw new WallHackException();
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
