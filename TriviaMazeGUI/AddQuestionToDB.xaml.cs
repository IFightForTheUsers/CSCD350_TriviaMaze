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
    public partial class AddQuestionToDB : Window
    {
        public AddQuestionToDB()
        {
            InitializeComponent();
        }

        private void SubmitQuestionTypeButton_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionTypeComboBox.Text == "True/False")
            {
                LoadCanvasTF();
            }
            if (QuestionTypeComboBox.Text == "Multiple Choice")
            {
                LoadCanvasMC();
            }
            if (QuestionTypeComboBox.Text == "Short Answer")
            {
                LoadCanvasSA();
            }
        }


        private void LoadCanvasTF()
        {

        }

        private void LoadCanvasMC()
        {
            TextBlock q = new TextBlock
            {
                Text = "Question: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };
            TextBlock a = new TextBlock
            {
                Text = "a: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };
            TextBlock b = new TextBlock
            {
                Text = "b: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };
            TextBlock c = new TextBlock
            {
                Text = "c: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };
            TextBlock d = new TextBlock
            {
                Text = "d: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };
            TextBlock ans = new TextBlock
            {
                Text = "Answer: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };

            TextBox q2 = new TextBox { Margin = new Thickness(10), Width = 400 };
            TextBox a2 = new TextBox { Margin = new Thickness(10), Width = 400 };
            TextBox b2 = new TextBox { Margin = new Thickness(10), Width = 400 };
            TextBox c2 = new TextBox { Margin = new Thickness(10), Width = 400 };
            TextBox d2 = new TextBox { Margin = new Thickness(10), Width = 400 };
            TextBox ans2 = new TextBox { Margin = new Thickness(10), Width = 100, HorizontalAlignment = HorizontalAlignment.Left };

            Button submitButton = new Button
            {
                Content = "Submit",
                Margin = new Thickness(10),
                Width = 60
            };

            SP1.Children.Add(q);
            SP1.Children.Add(a);
            SP1.Children.Add(b);
            SP1.Children.Add(c);
            SP1.Children.Add(d);
            SP1.Children.Add(ans);

            SP2.Children.Add(q2);
            SP2.Children.Add(a2);
            SP2.Children.Add(b2);
            SP2.Children.Add(c2);
            SP2.Children.Add(d2);
            SP2.Children.Add(ans2);
            SP2.Children.Add(submitButton);


        }
        private void LoadCanvasSA()
        {

        }

    }
}
