using System;
using System.Data.SQLite;
using System.Windows;

namespace TriviaMazeGUI
{
    [Serializable]
    class TrueFalseQuestion : PanelQuestion
    {
        private int indexToPullFromDBTable;

        public TrueFalseQuestion(Panel wrapping, int indexToPullFromDBTable) : base(wrapping)
        {
            this.indexToPullFromDBTable = indexToPullFromDBTable;
        }

        protected override void Ask()
        {
            // stub to fill in with a question prompt

            TrueFalse TF = new TrueFalse();
            MainWindow.Instance.Question.Children.Add(TF);

            string correctAnswer = "";

            int paramOne = this.indexToPullFromDBTable + 1;
            using (SQLiteConnection connection = new SQLiteConnection(MainWindow.ConnectionInfo))
            {
                connection.Open();
                using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM TrueFalse WHERE Q = @1",
                    connection))
                {
                    ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
                    using (SQLiteDataReader read = ins.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            TF.Q.Text = read["Question"].ToString();
                            correctAnswer = read["Answer"].ToString();

                        }

                        read.Close();
                    }
                }
                connection.Close();
            }

            TF.SubmitButton.Click += (sender, routedArgs) =>
            {
                if (TF.T.IsChecked == true && correctAnswer == "True")
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (TF.F.IsChecked == true && correctAnswer == "False")
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (TF.T.IsChecked == false && TF.F.IsChecked == false)
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
