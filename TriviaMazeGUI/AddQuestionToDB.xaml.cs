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
            SP1.Children.Clear();
            SP2.Children.Clear();

            TextBlock s = new TextBlock
            {
                Text = "Statement: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };

            TextBlock ans = new TextBlock
            {
                Text = "Correct \nAnswer: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };

            GroupBox group = new GroupBox { Margin = new Thickness(5), BorderThickness = new Thickness(0) };
            Grid g = new Grid();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            g.ColumnDefinitions.Add(col1);
            g.ColumnDefinitions.Add(col2);
            RadioButton t = new RadioButton { Margin = new Thickness(8), Content = "True", Tag = "T" };
            RadioButton f = new RadioButton { Margin = new Thickness(8), Content = "False", Tag = "F" };

            Grid.SetColumn(t, 0);
            Grid.SetColumn(f, 1);

            TextBox s2 = new TextBox { Margin = new Thickness(10), Width = 400, Padding = new Thickness(3) };

            g.Children.Add(t);
            g.Children.Add(f);

            group.Content = g;

            string ans2 = "";

            Button submitButton = new Button
            {
                Content = "Submit",
                Margin = new Thickness(10),
                Width = 60
            };

            SP1.Children.Add(s);
            SP1.Children.Add(ans);

            SP2.Children.Add(s2);
            SP2.Children.Add(group);
            SP2.Children.Add(submitButton);

            submitButton.Click += SubmitToDBButton_Click;

            void SubmitToDBButton_Click(object sender, RoutedEventArgs e)
            {
                if (t.IsChecked == true)
                {
                    ans2 = t.Content.ToString();
                }
                else if (f.IsChecked == true)
                {
                    ans2 = f.Content.ToString();
                }

                MessageBox.Show("You entered: \n" +
                                s.Text + s2.Text + "\n" +
                                ans.Text + ans2 + "\n"
                                + "\n\n" + "Added to DB."
                    );

                SQLiteConnection SQLconn = new SQLiteConnection("Data Source=TriviaMazeQuestions.db");
                SQLconn.Open();

                SQLiteCommand SQLcommand = SQLconn.CreateCommand();
                SQLcommand.CommandText = "INSERT INTO TrueFalse (Question, Answer)" + "VALUES (@1, @2);";
                SQLcommand.Parameters.Add(new SQLiteParameter("@1", s2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@2", ans2));

                SQLcommand.ExecuteNonQuery();

                s2.Clear();
            }

        }

        private void LoadCanvasMC()
        {
            SP1.Children.Clear();
            SP2.Children.Clear();

            TextBlock q = new TextBlock
            {
                Text = "Question: ",
                FontSize = 20,
                Margin = new Thickness(8,8,8,9)
            };
            TextBlock a = new TextBlock
            {
                Text = "a: ",
                FontSize = 20,
                Margin = new Thickness(8, 8, 8, 9)
            };
            TextBlock b = new TextBlock
            {
                Text = "b: ",
                FontSize = 20,
                Margin = new Thickness(8,8,8,9)
            };
            TextBlock c = new TextBlock
            {
                Text = "c: ",
                FontSize = 20,
                Margin = new Thickness(8,8,8,9)
            };
            TextBlock d = new TextBlock
            {
                Text = "d: ",
                FontSize = 20,
                Margin = new Thickness(8,8,8,9)
            };
            TextBlock ans = new TextBlock
            {
                Text = "Answer: ",
                FontSize = 20,
                Margin = new Thickness(8,8,8,9)
            };

            TextBox q2 = new TextBox { Margin = new Thickness(10), Width = 400, Padding = new Thickness(3) };
            TextBox b2 = new TextBox { Margin = new Thickness(10), Width = 400, Padding = new Thickness(3) };
            TextBox c2 = new TextBox { Margin = new Thickness(10), Width = 400, Padding = new Thickness(3) };
            TextBox d2 = new TextBox { Margin = new Thickness(10), Width = 400, Padding = new Thickness(3) };
            TextBox a2 = new TextBox { Margin = new Thickness(10), Width = 400, Padding = new Thickness(3) };

            ComboBox correctAnswer = new ComboBox { Margin = new Thickness(10) };
            ComboBoxItem cbA = new ComboBoxItem { Content = "a" };
            ComboBoxItem cbB = new ComboBoxItem { Content = "b" };
            ComboBoxItem cbC = new ComboBoxItem { Content = "c" };
            ComboBoxItem cbD = new ComboBoxItem { Content = "d" };
            correctAnswer.Items.Add(cbA);
            correctAnswer.Items.Add(cbB);
            correctAnswer.Items.Add(cbC);
            correctAnswer.Items.Add(cbD);

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
            SP1.Children.Clear();
            SP2.Children.Clear();

            TextBlock qs = new TextBlock
            {
                Text = "Question/ \nStatement: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };

            TextBlock ans = new TextBlock
            {
                Text = "Correct \nAnswer: ",
                FontSize = 20,
                Margin = new Thickness(5)
            };

            TextBox qs2 = new TextBox { Margin = new Thickness(20), Width = 400, Padding = new Thickness(3) };
            TextBox ans2 = new TextBox { Margin = new Thickness(20), 
                Width = 200, 
                HorizontalAlignment = HorizontalAlignment.Left, 
                Padding = new Thickness(3) 
            };

            Button submitButton = new Button
            {
                Content = "Submit",
                Margin = new Thickness(10),
                Width = 60
            };

            SP1.Children.Add(qs);
            SP1.Children.Add(ans);

            SP2.Children.Add(qs2);
            SP2.Children.Add(ans2);
            SP2.Children.Add(submitButton);

            submitButton.Click += SubmitToDBButton_Click;

            void SubmitToDBButton_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("You entered: \n" +
                                qs.Text + qs2.Text + "\n" +
                                ans.Text + ans2.Text + "\n"
                                + "\n\n" + "Added to DB."
                    );

                SQLiteConnection SQLconn = new SQLiteConnection("Data Source=TriviaMazeQuestions.db");
                SQLconn.Open();

                SQLiteCommand SQLcommand = SQLconn.CreateCommand();
                SQLcommand.CommandText = "INSERT INTO ShortAnswer (Question, Answer)" + "VALUES (@1, @2);";
                SQLcommand.Parameters.Add(new SQLiteParameter("@1", qs2.Text));
                SQLcommand.Parameters.Add(new SQLiteParameter("@2", ans2.Text));

                SQLcommand.ExecuteNonQuery();

                ClearFields();
            }

            void ClearFields()
            {
                qs2.Clear();
                ans2.Clear();
            }

        }

    }
}
