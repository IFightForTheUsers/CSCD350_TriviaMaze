using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    class MultipleChoiceQuestion : PanelQuestion
    {
        public MultipleChoiceQuestion(Panel wrapping) : base(wrapping)
        {
        }

        public new Room knock(Room from)
        {
            // stub to fill in with a question prompt
            return base.knock(from);
        }

    }
}