using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TriviaMazeGUI
{
    class ShortAnswerQuestion : PanelQuestion
    {
        public ShortAnswerQuestion(Panel wrapping) : base(wrapping)
        {
        }

        protected override void ask()
        {
            // stub to fill in with a question prompt

            ShortAnswer SA = new ShortAnswer();
            MainWindow.Instance.Question.Children.Add(SA);

            int paramOne = 2;
            SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM ShortAnswer WHERE Q = @1", MainWindow.Instance.getConnection);
            ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
            using (SQLiteDataReader read = ins.ExecuteReader())
            {
                if (read.Read())
                {
                    SA.Q.Text = (string)read["Question"];
                }
                read.Close();
            }
        }

    }
}