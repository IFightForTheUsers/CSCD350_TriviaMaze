using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    class Player
    {
        private string name;
        private Room roomIn;
        public Room at
        {
            get => roomIn;
            set => roomIn = value;
        }

        public Player(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value != null)
                {
                    name = value;
                }
            }
        }
    }
}
