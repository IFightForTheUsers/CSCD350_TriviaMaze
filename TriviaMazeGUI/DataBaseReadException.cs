using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    class DataBaseReadException : InvalidOperationException
    { // Exception to indicate that somehow a Database read has fucked up
        public DataBaseReadException()
        {
            // stubs
        }
        public DataBaseReadException(string message)
            : base(message)
        {
        }

        public DataBaseReadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
