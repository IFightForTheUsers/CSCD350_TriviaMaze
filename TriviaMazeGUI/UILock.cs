﻿using System;
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
        public Room here { get; private set; } = null;

        private int depth = 0;
        private Room to = null;
        private Panel _using_door = null;

        //---------------------------------------------------------------------------------------------------
        // this is all the public interface for intializing the UILock and requesting movement updates
        public void Initialize(Entrance starting_point)
        {
            here = starting_point.ghost(null);
            Here();
        }
        private void Aquire()
        {
            depth = _using_door.depth;
            Free();
        }

        public void Free()
        {
            depth--;
            if (depth < 0)
                throw new InvalidOperationException("UILock state changed to be negative value from Free() call.");
            else if (depth == 0)
                _free();
        }
        private void _free()
        {
            if (_using_door.locked==false)
            {
                There();
                MainWindow.Instance.Question.Children.Clear();
            }
            else
            {
                Here();
                MainWindow.Instance.Question.Children.Clear();
            }
        }

        //---------------------------------------------------------------------------------------------------
        // this shit is all the movement traversal

        private void Here()
        {
            Move(here);
        }

        private void There()
        {
            Move(_using_door.ghost(here));
        }

        private void Move(Room to)
        {
            this.here = to;
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

        private void Clear()
        {
            here.button.IsEnabled = false;
            here.button.Background = Regulations.disabledColor;
            here.button.Content = "Visited";

            if (here.north is Door || here.north is PanelQuestion)
            {
                here.north.ghost(here).button.Click -= Clicked_North;
                here.north.ghost(here).button.IsEnabled = false;
                here.north.ghost(here).button.Background = Regulations.disabledColor;
            }
            if (here.south is Door || here.south is PanelQuestion)
            {
                here.south.ghost(here).button.Click -= Clicked_South;
                here.south.ghost(here).button.IsEnabled = false;
                here.south.ghost(here).button.Background = Regulations.disabledColor;
            }
            if (here.east is Door || here.east is PanelQuestion)
            {
                here.east.ghost(here).button.Click -= Clicked_East;
                here.east.ghost(here).button.IsEnabled = false;
                here.east.ghost(here).button.Background = Regulations.disabledColor;
            }
            if (here.west is Door || here.west is PanelQuestion)
            {
                here.west.ghost(here).button.Click -= Clicked_West;
                here.west.ghost(here).button.IsEnabled = false;
                here.west.ghost(here).button.Background = Regulations.disabledColor;
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
            if (depth == 0)
            {
                _using_door = here.north;
                _common_clicky();
            }
        }

        private void Clicked_South(object sender, RoutedEventArgs e)
        {
            if (depth == 0)
            {
                _using_door = here.south;
                _common_clicky();
            }
        }

        private void Clicked_East(object sender, RoutedEventArgs e)
        {
            if (depth == 0)
            {
                _using_door = here.east;
                _common_clicky();
            }
        }

        private void Clicked_West(object sender, RoutedEventArgs e)
        {
            if (depth == 0)
            {
                _using_door = here.west;
                _common_clicky();
            }
        }

        private void _common_clicky()
        {
            Boolean asked = false;
            if (_using_door is PanelQuestion)
            {
                if (((PanelQuestion)_using_door).asked == true)
                    asked = true;
            }
            to = _using_door.knock(here);
            if (to != here && !asked)
            {
                Clear();
                Aquire();
            }
            else if (to != here && asked)
            {
                Clear();
                There();
            }
            else
            {
                Clear();
                Here();
            }
        }
    }
}
