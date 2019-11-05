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
        private static readonly int roomPixelSize = 50;
        private Grid grid;
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
            grid.Width = n*roomPixelSize;
            grid.Height = n*roomPixelSize;
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ShowGridLines = true;
            for (int i=0; i<n; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int y=0; y<n; y++)
            {
                for (int x=0; x<n; x++)
                {
                    Button temp = new Button();
                    temp.Name = buttonName(x,y);
                    temp.Height = roomPixelSize;
                    temp.Width = roomPixelSize;
                    temp.Background = Brushes.Pink;
                    grid.Children.Add(temp);
                    Grid.SetRow(temp, x);
                    Grid.SetColumn(temp, y);
                }
            }
            this.Content = grid;
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }
    }
}
