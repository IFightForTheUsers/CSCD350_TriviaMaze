using System;
using System.Windows;
using System.Windows.Controls;

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
            LoadMaze.Click += MainWindow.Instance.SaveLoad.LoadClick;
        }

        private void BuildClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.BuildMaze(Int32.Parse(MazeSize.Text));
            MainWindow.Instance.Question.Children.Clear();
        }
    }
}
