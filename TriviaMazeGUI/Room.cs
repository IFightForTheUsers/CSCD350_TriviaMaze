using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TriviaMazeGUI
{
    class Room
    {
        internal Button button;
        internal Panel north;
        internal Panel south;
        internal Panel east;
        internal Panel west;

        internal void Here()
        {
            button.IsEnabled = false;
            button.Background = Regulations.hereColor;
            button.Content = "HERE";

            if (north is Door)
            {
                if (!north.locked)
                {
                    north.ghost(this).button.Click += this.Clicked_North;
                    north.ghost(this).button.IsEnabled = true;
                    north.ghost(this).button.Background = Regulations.validMoveColor;
                }
            }
            if (south is Door)
            {
                if (!south.locked)
                {
                    south.ghost(this).button.Click += this.Clicked_South;
                    south.ghost(this).button.IsEnabled = true;
                    south.ghost(this).button.Background = Regulations.validMoveColor;
                }
            }
            if (east is Door)
            {
                if (!east.locked)
                {
                    east.ghost(this).button.Click += this.Clicked_East;
                    east.ghost(this).button.IsEnabled = true;
                    east.ghost(this).button.Background = Regulations.validMoveColor;
                }
            }
            if (west is Door)
            {
                if (!west.locked)
                {
                    west.ghost(this).button.Click += this.Clicked_West;
                    west.ghost(this).button.IsEnabled = true;
                    west.ghost(this).button.Background = Regulations.validMoveColor;
                }
            }
        }

        internal void Clear()
        {
            button.IsEnabled = false;
            button.Background = Regulations.disabledColor;
            button.Content = "Visited";

            if (north is Door)
            {
                north.ghost(this).button.Click -= Clicked_North;
                north.ghost(this).button.IsEnabled = false;
                north.ghost(this).button.Background = Regulations.disabledColor;
            }
            if (south is Door)
            {
                south.ghost(this).button.Click -= Clicked_South;
                south.ghost(this).button.IsEnabled = false;
                south.ghost(this).button.Background = Regulations.disabledColor;
            }
            if (east is Door)
            {
                east.ghost(this).button.Click -= Clicked_East;
                east.ghost(this).button.IsEnabled = false;
                east.ghost(this).button.Background = Regulations.disabledColor;
            }
            if (west is Door)
            {
                west.ghost(this).button.Click -= Clicked_West;
                west.ghost(this).button.IsEnabled = false;
                west.ghost(this).button.Background = Regulations.disabledColor;
            }
        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show isntance
            return "Room@"+this.GetHashCode().ToString();
        }

        private void Clicked_North(object sender, RoutedEventArgs e)
        {
            Room temp = north.knock(this);
            this.Clear();
            if (temp != this)
                temp.Here();
            else
                this.Here();
        }

        private void Clicked_South(object sender, RoutedEventArgs e)
        {
            Room temp = south.knock(this);
            this.Clear();
            if (temp != this)
                temp.Here();
            else
                this.Here();
        }

        private void Clicked_East(object sender, RoutedEventArgs e)
        {
            Room temp = east.knock(this);
            this.Clear();
            if (temp != this)
                temp.Here();
            else
                this.Here();
        }

        private void Clicked_West(object sender, RoutedEventArgs e)
        {
            Room temp = west.knock(this);
            this.Clear();
            if (temp != this)
                temp.Here();
            else
                this.Here();
        }

        public Room()
        {
        }
    }
}
