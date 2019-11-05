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
            for (int i = 0; i < 4; i++)
            { // add walls to maze edges
                rooms[i, 0].north = new Wall(rooms[i, 0]);
                rooms[i, 3].south = new Wall(rooms[i, 3]);
                rooms[0, i].west = new Wall(rooms[0, i]);
                rooms[3, i].east = new Wall(rooms[3, i]);
            }
            for (int x = 0; x < 3; x++)
            { // now we add doors
                for (int y = 0; y < 3; y++)
                {
                    new Door(rooms[x, y], rooms[x + 1, y], 'e');
                    new Door(rooms[x, y], rooms[x, y + 1], 's');
                }
            }
            // hard add the entry and exit
            ingress = new Entrance(rooms[0, 0]);
            egress = new Exit(rooms[3, 3]);

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
