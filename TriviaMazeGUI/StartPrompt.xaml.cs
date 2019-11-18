using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TriviaMazeGUI
{
    /// <summary>
    /// Interaction logic for StartPrompt.xaml
    /// </summary>
    public partial class StartPrompt : UserControl
    {
        public StartPrompt()
        {
            InitializeComponent();
        }

        public void BuildClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.BuildMaze(Int32.Parse(MazeSize.Text));
            MainWindow.Instance.Question.Children.Clear();
        }
    }
}
