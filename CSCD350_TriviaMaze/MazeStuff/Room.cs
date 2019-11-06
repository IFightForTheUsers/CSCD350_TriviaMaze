using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TriviaMaze
{
    class Room
    {
        private MazeWindow owner;
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
            north.setUIState(this);
            south.setUIState(this);
            east.setUIState(this);
            west.setUIState(this);
        }

        internal void Clear()
        {
            button.IsEnabled = false;
            button.Background = Regulations.disabledColor;
            button.Content = null;
            north.setUIState(null);
            south.setUIState(null);
            east.setUIState(null);
            west.setUIState(null);
        }

        public override string ToString()
        {
            // just a stupid hash for debug testing to show isntance
            return "Room@"+this.GetHashCode().ToString();
        }
        internal void Clicked()
        {
            //MessageBox.Show("eep! clicked Room is" + this.ToString() + " and we are at " + owner.at.ToString());
            Room temp = null;

            //janky testing to find which door is being used
            if (this.north.ghost(this) == owner.at)
            {
                owner.at.Clear();
                owner.at = this;
                this.Here();
            }
            else if (this.south.ghost(this) == owner.at)
            {
                owner.at.Clear();
                owner.at = this;
                this.Here();
            }
            else if (this.east.ghost(this) == owner.at)
            {
                owner.at.Clear();
                owner.at = this;
                this.Here();
            }
            else if (this.west.ghost(this) == owner.at)
            {
                owner.at.Clear();
                owner.at = this;
                this.Here();
            }
            else
            {
                throw new WallHackException("fuck up in just normal room traversal.");
            }
            owner.at.Here();
        }

        public Room(MazeWindow w)
        {
            owner = w;
        }
    }
}
