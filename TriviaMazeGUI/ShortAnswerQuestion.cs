using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace TriviaMazeGUI
{
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
            using (SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM ShortAnswer WHERE Q = @1",
                MainWindow.Instance.getConnection))
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

            SA.SubmitButton.Click += (sender, routedArgs) =>
            {
                if (SA.userInput.Text.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Correct");
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else
                {
                    MessageBox.Show("Incorrect");
                    MainWindow.Instance.Question.Children.Clear();
                    this.locked = true;
                    UILock.Instance.Free();
                }
            };
        }

    }
}