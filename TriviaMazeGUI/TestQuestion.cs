using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TriviaMazeGUI
{
    class TestQuestion : PanelQuestion
    {
        public new Room knock(Room from)
        {
            // stub to fill in with a question prompt
            StackPanel sp = new StackPanel();
            return base.knock(from);
        }

        public TestQuestion(Panel wrapping) : base(wrapping)
        {
        }
    }
}
