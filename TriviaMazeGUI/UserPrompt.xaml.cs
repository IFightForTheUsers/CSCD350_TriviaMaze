using System;
using System.Windows;

namespace TriviaMazeGUI
{
    /// <summary>
    /// Interaction logic for UserPrompt.xaml
    /// </summary>
    public partial class UserPrompt : Window
    {

        string name;

        public UserPrompt()
        {
            InitializeComponent();
        }

        private void NameButton_Click(object sender, RoutedEventArgs e)
        {
            this.name = NameEntry.Text;
            //MessageBox.Show(NameEntry.Text);

            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("Good luck, " + this.name + "!");

        }
    }
}
