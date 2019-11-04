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
using System.Windows.Shapes;

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
