using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Maze
    {
        private Room[,] rooms;

        public Maze()
        {
            rooms = new Room[4, 4];
            foreach (Room r in rooms)
            { // populate rooms with cheat doors
                r.north = new Door();
                r.south = new Door();
                r.west = new Door();
                r.east = new Door();
            }
        }
    }
}
