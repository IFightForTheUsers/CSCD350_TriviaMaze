using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TriviaMazeGUI
{
    class Solveable
    {
        private Queue<Room> order = new Queue<Room>();
        private bool solved = false;

        public void CheckIfSolveable(Room[,] maze, int row, int column)
        {
            if (order.Count == 0)
            {
                order.Enqueue(maze[row, column]);
                maze[row, column].flag = true;
            }
            if (!maze[row,column].north.locked && !maze[row + 1,column].flag)
            {
                order.Enqueue(maze[row + 1, column]);
                maze[row + 1, column].flag = true;
            }
            if (!maze[row, column].north.locked && !maze[row, column + 1].flag)
            {
                order.Enqueue(maze[row, column + 1]);
                maze[row, column + 1].flag = true;
            }
            if (!maze[row, column].north.locked && !maze[row, column - 1].flag)
            {
                order.Enqueue(maze[row, column - 1]);
                maze[row, column - 1].flag = true;
            }
            if (!maze[row, column].north.locked && !maze[row - 1, column].flag)
            {
                order.Enqueue(maze[row - 1, column]);
                maze[row - 1, column].flag = true;
            }
        }

    }
}
