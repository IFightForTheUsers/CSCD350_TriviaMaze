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

            if (north is Door || north is PanelQuestion)
            {
                if (!north.locked)
                {
                    Button temp = north.ghost(this).button;
                    temp.Click += this.Clicked_North;
                    EnableBtn(temp);
                }
            }
            if (south is Door || south is PanelQuestion)
            {
                if (!south.locked)
                {
                    Button temp = south.ghost(this).button;
                    temp.Click += this.Clicked_South;
                    EnableBtn(temp);
                }
            }
            if (east is Door || east is PanelQuestion)
            {
                if (!east.locked)
                {
                    Button temp = east.ghost(this).button;
                    temp.Click += this.Clicked_East;
                    EnableBtn(temp);
                }
            }
            if (west is Door || west is PanelQuestion)
            {
                if (!west.locked)
                {
                    Button temp = west.ghost(this).button;
                    temp.Click += this.Clicked_West;
                    EnableBtn(temp);
                }
            }
        }

        internal void Clear()
        {
            button.IsEnabled = false;
            button.Background = Regulations.disabledColor;
            button.Content = "Visited";

            if (north is Door || north is PanelQuestion)
            {
                north.ghost(this).button.Click -= Clicked_North;
                north.ghost(this).button.IsEnabled = false;
                north.ghost(this).button.Background = Regulations.disabledColor;
            }
            if (south is Door || south is PanelQuestion)
            {
                south.ghost(this).button.Click -= Clicked_South;
                south.ghost(this).button.IsEnabled = false;
                south.ghost(this).button.Background = Regulations.disabledColor;
            }
            if (east is Door || east is PanelQuestion)
            {
                east.ghost(this).button.Click -= Clicked_East;
                east.ghost(this).button.IsEnabled = false;
                east.ghost(this).button.Background = Regulations.disabledColor;
            }
            if (west is Door || west is PanelQuestion)
            {
                west.ghost(this).button.Click -= Clicked_West;
                west.ghost(this).button.IsEnabled = false;
                west.ghost(this).button.Background = Regulations.disabledColor;
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
