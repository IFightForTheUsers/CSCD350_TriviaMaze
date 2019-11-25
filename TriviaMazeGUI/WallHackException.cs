using System;

namespace TriviaMazeGUI
{
    class WallHackException : InvalidOperationException
    { // Exception to indicate that somehow you're attempting to traverse the maze in an illegal fashion
        public WallHackException()
        {
            // stubs
        }
        public WallHackException(string message)
            : base(message)
        {
        }

        public WallHackException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
