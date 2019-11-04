using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Room
    {
        private Door door;

        public Room(Door door)
        {
            this.door = door;
        }

        public Door Door {
            get { return door; } 
            set { 
                if (value != null)
                {
                    door = value;
                }
            } 
        }
    }
}
