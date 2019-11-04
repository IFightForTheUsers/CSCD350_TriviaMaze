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
        private Entrance ingress;
        public Panel entrance => (Panel)ingress;
        public Room startingRoom => ingress.knock(null);
        private Exit egress;
        public Panel exit => (Panel)egress;

        public Exit getExit() { return egress; }

        // constructors
        public Maze()
        {
            rooms = new Room[4, 4];
            for (int x=0; x<4; x++)
            { // add walls to maze edges
                rooms[x, 0].north = new Wall(rooms[x, 0]);
                rooms[x, 3].south = new Wall(rooms[x, 3]);
                rooms[0, x].west = new Wall(rooms[0, x]);
                rooms[3, x].east = new Wall(rooms[3, x]);
            }
            for (int x=0; x<3; x++)
            { // now we add doors
                for (int y=0; y<3; y++)
                {
                    new Door(rooms[x, y], rooms[x + 1, y], 'e');
                    new Door(rooms[x, y], rooms[x, y + 1], 's');
                }
            }
            // hard add the entry and exit
            ingress = new Entrance(rooms[0, 0]);
            egress = new Exit(rooms[3, 3]);
            foreach (Room r in rooms)
            { // wrap all the Doors with TestQuestions... hopefully
                if (!(r.east is Wall) && !(r.east is Exit) && !(r.east is Entrance))
                    new TestQuestion(r.east);
                if (!(r.east is Wall) && !(r.east is Exit) && !(r.east is Entrance))
                    new TestQuestion(r.south);
            }
        } // end Maze()
    }
}
