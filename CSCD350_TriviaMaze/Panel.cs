using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    interface Panel
    {
        bool isLocked();
        Room knock(Room from); // knock(from room): to room. if locked just returns from
        Room kick(Room from); // cheat that moves to next room always
        void rewire(Panel obj); // rewires the Panel's Rooms to point to the obj instead of it's self. mostly for decorators
    }
}
