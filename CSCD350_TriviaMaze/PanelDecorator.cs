using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class PanelDecorator : Panel
    {
        private Panel panel;
        public bool isLocked()
        {
            return panel.isLocked();
        }
        public Room knock(Room from)
        {
            return panel.knock(from);
        }
        public Room kick(Room from)
        {
            return panel.kick(from);
        }

        public PanelDecorator(Panel wrapping)
        {
            panel = wrapping;
        }
    }
}
