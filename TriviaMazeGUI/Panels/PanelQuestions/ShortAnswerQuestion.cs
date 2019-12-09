using System;
using System.Data.SQLite;

namespace TriviaMazeGUI.Panels.PanelQuestions
{
    [Serializable]
    class ShortAnswerQuestion : PanelQuestion
    {
        private int indexToPullFromDBTable;

        public ShortAnswerQuestion(Panel wrapping, int indexToPullFromDBTable) : base(wrapping)
        {
            this.indexToPullFromDBTable = indexToPullFromDBTable;
        }

        protected override void Ask()
        {
            // stub to fill in with a question prompt

            ShortAnswer SA = new ShortAnswer();
            MainWindow.Instance.Question.Children.Add(SA);

            string correctAnswer = "";

            int paramOne = this.indexToPullFromDBTable + 1;
            using (SQLiteConnection connection = new SQLiteConnection(MainWindow.ConnectionInfo))
            {
                connection.Open();
                using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM ShortAnswer WHERE Q = @1",
                    connection))
                {
                    ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
                    using (SQLiteDataReader read = ins.ExecuteReader())
                    {
                        if (read.Read())
                        {
                            SA.Q.Text = read["Question"].ToString();
                            correctAnswer = read["Answer"].ToString();
                        }

                        read.Close();
                    }
                }
                connection.Close();
            }

            SA.SubmitButton.Click += (sender, routedArgs) =>
            {
                if (SA.userInput.Text.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    UILock.Instance.Correct();
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
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