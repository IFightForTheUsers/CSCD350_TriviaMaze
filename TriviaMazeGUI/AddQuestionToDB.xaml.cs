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
using System.Data.SQLite;

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

            ComboBox correctAnswer = new ComboBox { Margin = new Thickness(10) };
            ComboBoxItem cbA = new ComboBoxItem { Content = "a" };
            ComboBoxItem cbB = new ComboBoxItem { Content = "b" };
            ComboBoxItem cbC = new ComboBoxItem { Content = "c" };
            ComboBoxItem cbD = new ComboBoxItem { Content = "d" };
            correctAnswer.Items.Add(cbA);
            correctAnswer.Items.Add(cbB);
            correctAnswer.Items.Add(cbC);
            correctAnswer.Items.Add(cbD);
            //TextBox ans2 = new TextBox { Margin = new Thickness(10), Width = 100, HorizontalAlignment = HorizontalAlignment.Left };

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
            SP2.Children.Add(correctAnswer);
            SP2.Children.Add(submitButton);

            submitButton.Click += SubmitToDBButton_Click;

            void SubmitToDBButton_Click(object sender, RoutedEventArgs e)
            {


                MessageBox.Show("You entered: \n" +
                                q.Text + q2.Text + "\n" +
                                a.Text + a2.Text + "\n" +
                                b.Text + b2.Text + "\n" +
                                c.Text + c2.Text + "\n" +
                                d.Text + d2.Text + "\n" +
                                ans.Text + correctAnswer.Text //.SelectedItem.ToString()
                                + "\n\n" + "Added to DB."
                    );

                SQLiteConnection SQLconn = new SQLiteConnection("Data Source=TriviaMazeQuestions.db");
                SQLconn.Open();

                SQLiteCommand SQLcommand = SQLconn.CreateCommand();
                SQLcommand.CommandText = "INSERT INTO MultipleChoice (Question, A, B, C, D, Answer)" +
                "VALUES (@1, @2, @3, @4, @5, @6);";
                SQLcommand.Parameters.Add(new SQLiteParameter("@1", q2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@2", a2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@3", b2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@4", c2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@5", d2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@6", correctAnswer.Text));

                SQLcommand.ExecuteNonQuery();

                ClearFields();

            }

            void ClearFields()
            {
                q2.Clear();
                a2.Clear();
                b2.Clear();
                c2.Clear();
                d2.Clear();
                correctAnswer.Text = "";
            }
        }



        private void LoadCanvasSA()
        {

        }

    }
}
