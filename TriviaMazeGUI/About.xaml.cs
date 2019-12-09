using System.Windows;

namespace TriviaMazeGUI
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void _close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
