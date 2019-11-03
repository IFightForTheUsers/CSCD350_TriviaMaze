using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Door
    {
        private Question question;
        private bool locked = false;

        public bool isLocked() { return locked; }

        public bool knock()
        {
            if (!locked)
            {
                locked = question.ask();
                return locked;
            }
        }

        public void kick() { locked = false;  } // cheat to pass thru door

        Door()
        {
            this.question = new cheatQuestion();
        }
    }
}
