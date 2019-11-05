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

        internal void here()
        {
            button.IsEnabled = false;
            button.Background = Regulations.hereColor;
            button.Content = "HERE";
            north.setUIState(this);
            south.setUIState(this);
            east.setUIState(this);
            west.setUIState(this);
        }

        public override string ToString()
        {
            return this.GetHashCode().ToString();
        }
        internal void clicked()
        {
            MessageBox.Show("eep! clicked Room is" + this.ToString() + " and we are at " + owner.at.ToString());
        }

        public Room(MazeWindow w)
        {
            owner = w;
        }
    }
}
