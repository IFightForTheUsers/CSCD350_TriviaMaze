using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TriviaMazeGUI
{
    sealed class UILock
    {
        private static readonly Lazy<UILock> lazy = new Lazy<UILock> (() => new UILock());
        public static UILock Instance { get { return lazy.Value; } }
        private UILock() { }
        public Room here { get { return _here; } }

        private int depth = 0;
        private Room _here = null;
        private Room _to = null;
        private Panel _using_door = null;

        //---------------------------------------------------------------------------------------------------
        // this is all the public interface for intializing the UILock and requesting movement updates
        public void Initialize(Entrance starting_point)
        {
            _here = starting_point.ghost(null);
        }
        private void Aquire()
        {
            Clear();
            depth = _using_door.depth;
            Free();
        }

        public void Free()
        {
            depth--;
            if (depth < 0)
                throw new InvalidOperationException("UILock state changed to be negative value from free.");
            else if (depth == 0)
                _free();
        }
        private void _free()
        {
            if (_using_door.locked==false)
            {
                There();
            }
            else
            {
                Here();
            }
        }

        //---------------------------------------------------------------------------------------------------
        // this shit is all the movement traversal

        private void Here()
        {
            Move(_here);
        }

        private void There()
        {
            Move(_using_door.ghost(_here));
        }

        private void Move(Room to)
        {
            to.button.IsEnabled = false;
            to.button.Background = Regulations.hereColor;
            to.button.Content = "HERE";

            if (to.north is Door || to.north is PanelQuestion)
            {
                if (!to.north.locked)
                {
                    Button temp = to.north.ghost(to).button;
                    temp.Click += this.Clicked_North;
                    EnableBtn(temp);
                }
            }
            if (to.south is Door || to.south is PanelQuestion)
            {
                if (!to.south.locked)
                {
                    Button temp = to.south.ghost(to).button;
                    temp.Click += this.Clicked_South;
                    EnableBtn(temp);
                }
            }
            if (to.east is Door || to.east is PanelQuestion)
            {
                if (!to.east.locked)
                {
                    Button temp = to.east.ghost(to).button;
                    temp.Click += this.Clicked_East;
                    EnableBtn(temp);
                }
            }
            if (to.west is Door || to.west is PanelQuestion)
            {
                if (!to.west.locked)
                {
                    Button temp = to.west.ghost(to).button;
                    temp.Click += this.Clicked_West;
                    EnableBtn(temp);
                }
            }
        }

        internal void Clear()
        {
            _here.button.IsEnabled = false;
            _here.button.Background = Regulations.disabledColor;
            _here.button.Content = "Visited";

            if (_here.north is Door || _here.north is PanelQuestion)
            {
                _here.north.ghost(_here).button.Click -= Clicked_North;
                _here.north.ghost(_here).button.IsEnabled = false;
                _here.north.ghost(_here).button.Background = Regulations.disabledColor;
            }
            if (_here.south is Door || _here.south is PanelQuestion)
            {
                _here.south.ghost(_here).button.Click -= Clicked_South;
                _here.south.ghost(_here).button.IsEnabled = false;
                _here.south.ghost(_here).button.Background = Regulations.disabledColor;
            }
            if (_here.east is Door || _here.east is PanelQuestion)
            {
                _here.east.ghost(_here).button.Click -= Clicked_East;
                _here.east.ghost(_here).button.IsEnabled = false;
                _here.east.ghost(_here).button.Background = Regulations.disabledColor;
            }
            if (_here.west is Door || _here.west is PanelQuestion)
            {
                _here.west.ghost(_here).button.Click -= Clicked_West;
                _here.west.ghost(_here).button.IsEnabled = false;
                _here.west.ghost(_here).button.Background = Regulations.disabledColor;
            }
        }

        private void EnableBtn(Button b)
        {
            b.IsEnabled = true;
            if ((string)b.Content == "Visited")
                b.Background = Regulations.visitedMoveColor;
            else
                b.Background = Regulations.validMoveColor;
        }

        private void Clicked_North(object sender, RoutedEventArgs e)
        {
            _using_door = _here.north;
            _common_clicky();
        }

        private void Clicked_South(object sender, RoutedEventArgs e)
        {
            _using_door = _here.south;
            _common_clicky();
        }

        private void Clicked_East(object sender, RoutedEventArgs e)
        {
            _using_door = _here.east;
            _common_clicky();
        }

        private void Clicked_West(object sender, RoutedEventArgs e)
        {
            _using_door = _here.west;
            _common_clicky();
        }

        private void _common_clicky()
        {
            _to = _using_door.knock(_here);
            if (_to != _here)
            {
                Aquire();
            }
            else
            {
                Clear();
                Here();
            }
        }
    }
}
