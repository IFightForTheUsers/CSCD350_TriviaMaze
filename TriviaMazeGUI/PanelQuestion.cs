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
        public Boolean asked { get; private set; }

        public bool locked
        {
            get { return wrapped.locked; }
            set { wrapped.locked = value;  }
        }
        public int depth { get { return wrapped.depth + 1; } }
        public Room knock(Room from)
        {
            if (!asked)
            {
                asked = true;
                ask();
                return wrapped.knock(from);
            }
            else
            {
                return wrapped.knock(from);
            }
        }

        protected abstract void ask();

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
