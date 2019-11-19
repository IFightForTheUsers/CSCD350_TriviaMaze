using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    public sealed class UILock
    {
        private static readonly Lazy<UILock> lazy = new Lazy<UILock> (() => new UILock());
        public static UILock Instance { get { return lazy.Value; } }
        private UILock() { }
    }
}
