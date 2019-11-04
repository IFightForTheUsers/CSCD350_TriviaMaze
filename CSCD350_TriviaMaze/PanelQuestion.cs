﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    abstract class PanelQuestion : Panel
    {
        private Panel wrapped;
        public bool isLocked()
        {
            return wrapped.isLocked();
        }
        public Room knock(Room from)
        {
            return wrapped.knock(from);
        }
        public Room kick(Room from)
        {
            return wrapped.kick(from);
        }

        public void rewire(Panel obj)
        {
            wrapped.rewire(obj);
        }

        public PanelQuestion(Panel wrapping)
        {
            wrapped = wrapping;
            wrapped.rewire(this);
        }
    }
}
