﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    class Wall : Panel
    {
        private Room room;

        public bool locked { get { return true; }  }

        public Room knock(Room from)
        {
            if (from == room)
                return from;
            else
                throw new WallHackException();
        }

        public Room kick(Room from) {
            if (from == room)
                return from;
            else
                throw new WallHackException();
        }

        public Room ghost(Room from)
        {
            return null;
        }

        public void setUIState(Room from)
        {
            // does nothing
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

        public Wall(Room owner)
        {
            room = owner;
        }
    }
}
