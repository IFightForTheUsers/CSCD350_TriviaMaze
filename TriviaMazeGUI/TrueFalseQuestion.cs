using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TriviaMazeGUI
{
    class TrueFalseQuestion : PanelQuestion
    {
        public TrueFalseQuestion(Panel wrapping, int indexToPullFromTFTable) : base(wrapping)
        {

        }

        protected override void ask()
        {
            // stub to fill in with a question prompt

            TrueFalse TF = new TrueFalse();
            MainWindow.Instance.Question.Children.Add(TF);

            int paramOne = 2;
            SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM TrueFalse WHERE Q = @1", MainWindow.Instance.getConnection);
            ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
            using (SQLiteDataReader read = ins.ExecuteReader())
            {
                if (read.Read())
                {
                    TF.Q.Text = (string)read["Question"];
                }
                read.Close();
            }
        }

    }
}
