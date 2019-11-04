using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Room
    {
        private Door north;
        private Door south;
        private Door east;
        private Door west;

        public Room(Door north, Door south, Door east, Door west)
        {
            this.north = north;
            this.south = south;
            this.east = east;
            this.west = west;
        }

        public Door North 
        { 
            get 
            { 
                return north; 
            } 
            set 
            {
                if (value != null)
                {
                    north = value;
                }
            } 
        }
        public Door south { 
            get 
            { 
                return south; 
            }
            set
            {
                if (value != null)
                {
                    south = value;
                }
            } 
        }
        public Door East 
        {
            get
            {
                return east;
            } 
            set
            {
                if (value != null)
                {
                    east = value;
                }
            }
        }
        public Door West 
        {
            get
            {
                return west;
            }
            set
            {
                if (value != null)
                {
                    west = value;
                }
            }
        }
    }
}
