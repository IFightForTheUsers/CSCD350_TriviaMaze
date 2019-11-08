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
//using System.Data.SQLite;

namespace TriviaMazeGUI
{
    public partial class MainWindow : Window
    {
        //private SQLiteConnection connection { get; set; }
        private RadioButton t;
        private RadioButton f;
        private About about;
        public MainWindow()
        {
            InitializeComponent();
            TrueFalseQuestion();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Loaded");

            UserPrompt userPromptWindow = new UserPrompt();
            userPromptWindow.Show();

            //string name = userPromptWindow.NameEntry.Text;
            //MessageBox.Show(name);
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            this.about = new About();
            this.about.Show();

        }

        private void mnuInstructions_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TrueFalseQuestion()
        {
            t = new RadioButton();
            f = new RadioButton();
            StackPanel sp = new StackPanel();

            Question.Children.Add(sp);
            sp.Children.Add(t);
            sp.Children.Add(f);

            Canvas.SetTop(sp, 50);
            Canvas.SetLeft(sp, 50);

            t.Content = "True";
            f.Content = "False";

        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Question.Children.Clear();
        }


    }
}
