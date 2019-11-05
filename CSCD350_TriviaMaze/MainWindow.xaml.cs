using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        MazeWindow maze = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (!_regex.IsMatch(maze_dim.Text)) {
                maze = new MazeWindow();
                maze.Build(Int32.Parse(maze_dim.Text));
                maze.Show();
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            btn_1.Click += btn1_Click;
        }
    }
}
