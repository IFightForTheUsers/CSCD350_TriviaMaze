using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMazeGUI
{
    class MultipleChoiceQuestion : PanelQuestion
    {
        public MultipleChoiceQuestion(Panel wrapping) : base(wrapping)
        {
        }

        protected override void ask()
        {
            // stub to fill in with a question prompt

            MultipleChoice MC = new MultipleChoice();
            MainWindow.Instance.Question.Children.Add(MC);

            int paramOne = 2;
            SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM MultipleChoice WHERE Q = @1", MainWindow.Instance.getConnection);
            ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
            using (SQLiteDataReader read = ins.ExecuteReader())
            {
                if (read.Read())
                {
                    MC.Q.Text = (string)read["Question"];
                    MC.a.Content = read["A"];
                    MC.b.Content = read["B"];
                    MC.c.Content = read["C"];
                    MC.d.Content = read["D"];
                }
                read.Close();
            }



        }



    }
}