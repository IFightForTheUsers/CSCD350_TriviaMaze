using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Player
    {
        private string name;
        private Room roomIn;

        public void setAt(Room room)
        {
            roomIn = room;
        }

        public Room getAt()
        {
            return roomIn;
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
