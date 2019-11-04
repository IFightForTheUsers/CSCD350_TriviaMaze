using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
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
