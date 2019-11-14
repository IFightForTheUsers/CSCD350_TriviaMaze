﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    abstract class PanelQuestion : Panel
    {
        private Panel wrapped;
        public bool locked
        {
            get { return wrapped.locked; }
            set { wrapped.locked = value;  }
        }
        public Room knock(Room from)
        {
            return wrapped.knock(from);
        }
        public Room kick(Room from)
        {
            return wrapped.kick(from);
        }

        public Room ghost(Room from)
        {
            return wrapped.ghost(from);
        }

        public void rewire(Panel obj)
        {
            wrapped.rewire(obj);
        }

        protected PanelQuestion(Panel wrapping)
        {
            wrapped = wrapping;
            wrapped.rewire(this);
        }
    }
}
