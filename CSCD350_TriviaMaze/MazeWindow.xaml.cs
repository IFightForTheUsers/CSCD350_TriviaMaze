using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for Maze.xaml
    /// </summary>

    public partial class MazeWindow : Window
    {
        private Grid grid;
        private Room[,] rooms;
        private Entrance ingress;
        private Exit egress;
        internal Room at;

        public MazeWindow()
        {
            InitializeComponent();
        }

        public String buttonName(int x, int y)
        {
            return "b_x" + x + "y" + y;
        }
        public void Build(int n)
        {
            grid = new Grid();
            rooms = new Room[n, n];

            grid.Width = n*Regulations.roomPixelSize;
            grid.Height = n* Regulations.roomPixelSize;
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ShowGridLines = true;
            
            // make the grid!
            for (int i=0; i<n; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // now we add buttons and rooms!
            for (int y=0; y<n; y++)
            {
                for (int x=0; x<n; x++)
                {
                    Button temp = new Button();
                    rooms[x, y] = new Room(this);
                    Room lambda_var = rooms[x, y];
                    temp.Click += (s, e) => { lambda_var.clicked(); };
                    rooms[x, y].button = temp;
                    rooms[x, y].button.Name = buttonName(x,y);
                    rooms[x, y].button.Height = Regulations.roomPixelSize;
                    rooms[x, y].button.Width = Regulations.roomPixelSize;
                    rooms[x, y].button.Background = Regulations.disabledColor;
                    rooms[x, y].button.IsEnabled = false; // no clicky button! not yet!
                    grid.Children.Add(rooms[x, y].button);
                    Grid.SetRow(rooms[x, y].button, x);
                    Grid.SetColumn(rooms[x, y].button, y);
                }
            }
            for (int i = 0; i < n; i++)
            { // add walls to maze edges
                rooms[i, 0].north = new Wall(rooms[i, 0]);
                rooms[0, i].west = new Wall(rooms[0, i]);
                rooms[n-1, i].east = new Wall(rooms[n-1, i]);
                rooms[i, n-1].south = new Wall(rooms[i, n-1]);
            }
            // corner check for walls
            //NW corner
            Debug.Assert(rooms[0, 0].north != null);
            Debug.Assert(rooms[0, 0].west != null);
            //NE corner
            Debug.Assert(rooms[n-1, 0].north != null);
            Debug.Assert(rooms[n-1, 0].east != null);
            //SW corner
            Debug.Assert(rooms[0, n-1].south != null);
            Debug.Assert(rooms[0, n-1].west != null);
            //SE corner
            Debug.Assert(rooms[n-1, n-1].south != null);
            Debug.Assert(rooms[n-1, n-1].east != null);
            for (int y = 0; y < n-1; y++)
            { // now we add doors
                for (int x = 0; x < n-2; x++)
                {
                    new Door(rooms[x, y], rooms[x + 1, y], 'e');
                    new Door(rooms[x, y], rooms[x, y + 1], 's');
                }
            }
            // hard add the entry and exit
            ingress = new Entrance(rooms[0, 0]);
            egress = new Exit(rooms[n-1, n-1]);

            rooms[0, 0].here();
            at = rooms[0, 0];
            /*foreach (Room r in rooms)
            { // wrap all the Doors with TestQuestions...
                if (r.east is Door)
                    new TestQuestion(r.east);
                if (r.south is Door)
                    new TestQuestion(r.south);
            }*/
            this.Content = grid;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
