using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    class TestQuestion : PanelQuestion
    {
        public new Room knock(Room from)
        {
            // stub to fill in with a question prompt
            return base.knock(from);
        }
        public TestQuestion(Panel wrapping) : base(wrapping)
        {
        }
    }
}
