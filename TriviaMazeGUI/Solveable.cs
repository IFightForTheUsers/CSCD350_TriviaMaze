using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TriviaMazeGUI
{
    class Solveable
    {
        private static Queue<Room> order = new Queue<Room>();
        private static bool solved = false;
        private static bool inside = false;
        public static void Reset()
        {
            solved = false;
            inside = false;
            order = null;
            order = new Queue<Room>();
        }

        public static bool CheckIfSolveable(Room[,] maze)
        {
            bool solv = false;
            int tempRow = 0;
            int tempColumn = 0;

            if (order.Count == 0 && inside)
            {
                return false;
            }
            if (order.Count == 0)
            {
                order.Enqueue(maze[0, 0]);
                maze[0, 0].Flag = true;
                inside = true;
            }
            else if(!solved)
            {
                Room temp = order.Dequeue();
                if(temp.east is Exit)
                {
                    solved = true;
                    inside = false;
                    return true;
                }
                for(int i = 0; i < maze.GetLength(0); i++)
                {
                    bool check = false;
                    for (int j = 0; j < maze.GetLength(1); j++)
                    {
                        if(maze[i,j].CompareTo(temp) == 0)
                        {
                            tempRow = i;
                            tempColumn = j;
                            check = true;
                            break;
                        }
                    }
                    if (check)
                    {
                        break;
                    }
                }
            }
            if (!maze[tempRow,tempColumn].north.locked && !maze[tempRow - 1,tempColumn].Flag && !solved)
            {
                order.Enqueue(maze[tempRow - 1, tempColumn]);
                maze[tempRow - 1, tempColumn].Flag = true;
            }
            if (!maze[tempRow, tempColumn].east.locked && !maze[tempRow, tempColumn + 1].Flag && !solved)
            {
                order.Enqueue(maze[tempRow, tempColumn + 1]);
                maze[tempRow, tempColumn + 1].Flag = true;
            }
            if (!maze[tempRow, tempColumn].west.locked && !maze[tempRow, tempColumn - 1].Flag && !solved)
            {
                order.Enqueue(maze[tempRow, tempColumn - 1]);
                maze[tempRow, tempColumn - 1].Flag = true;
            }
            if (!maze[tempRow, tempColumn].south.locked && !maze[tempRow + 1, tempColumn].Flag && !solved)
            {
                order.Enqueue(maze[tempRow + 1, tempColumn]);
                maze[tempRow + 1, tempColumn].Flag = true;
            }
            solv = CheckIfSolveable(maze);
            return solv;
        }

    }
}
