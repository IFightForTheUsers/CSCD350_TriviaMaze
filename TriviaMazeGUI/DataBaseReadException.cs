using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    class DatabaseReadException : InvalidOperationException
    { // Exception to indicate that somehow a Database read has fucked up
        public DatabaseReadException()
        {
            // stubs
        }
        public DatabaseReadException(string message)
            : base(message)
        {
        }

        public DatabaseReadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
