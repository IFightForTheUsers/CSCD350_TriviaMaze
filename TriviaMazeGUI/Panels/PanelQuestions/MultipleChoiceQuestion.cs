using System;
using System.Data.SQLite;
using System.Windows;

namespace TriviaMazeGUI.Panels.PanelQuestions
{
    [Serializable]
    class MultipleChoiceQuestion : PanelQuestion
    {
        private int indexToPullFromDBTable;

        public MultipleChoiceQuestion(Panel wrapping, int indexToPullFromDBTable) : base(wrapping)
        {
            this.indexToPullFromDBTable = indexToPullFromDBTable;
        }

        protected override void Ask()
        {
            // stub to fill in with a question prompt

            MultipleChoice MC = new MultipleChoice();
            MainWindow.Instance.Question.Children.Add(MC);

            string correctAnswer = "";

            int paramOne = this.indexToPullFromDBTable + 1;
            using (SQLiteConnection connection = new SQLiteConnection(MainWindow.ConnectionInfo))
            {
                connection.Open();
                using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM MultipleChoice WHERE Q = @1",
                    connection))
                {
                    ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
                    using (SQLiteDataReader read = ins.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            MC.Q.Text = (string) read["Question"];
                            MC.a.Content = read["A"];
                            MC.b.Content = read["B"];
                            MC.c.Content = read["C"];
                            MC.d.Content = read["D"];
                            correctAnswer = read["Answer"].ToString().ToLower();
                        }

                        read.Close();
                    }
                }
                connection.Close();
            }

            MC.SubmitButton.Click += (sender, routedArgs) =>
            {

                //MessageBox.Show("Correct Answer: " + correctAnswer);


                if (MC.a.IsChecked == true && correctAnswer == "a")
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (MC.b.IsChecked == true && correctAnswer == "b")
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (MC.c.IsChecked == true && correctAnswer == "c")
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (MC.d.IsChecked == true && correctAnswer == "d")
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (MC.a.IsChecked == false && MC.b.IsChecked == false && MC.c.IsChecked == false && MC.d.IsChecked == false)
                {
                    MessageBox.Show("Please select an answer before pressing Submit, Dingus.");
                }

                else
                {
                    UILock.Instance.Wrong();
                    MainWindow.Instance.Question.Children.Clear();
                    this.locked = true;
                    UILock.Instance.Free();
                }
            };

        }

    }
}