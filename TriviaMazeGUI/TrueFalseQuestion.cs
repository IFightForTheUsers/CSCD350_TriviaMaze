﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace TriviaMazeGUI
{
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
            SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM TrueFalse WHERE Q = @1", MainWindow.Instance.getConnection);
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

            TF.SubmitButton.Click += (sender, routedArgs) =>
            {
                if (TF.T.IsChecked == true && correctAnswer == "True")
                {
                    MessageBox.Show("Correct");
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (TF.F.IsChecked == true && correctAnswer == "False")
                {
                    MessageBox.Show("Correct");
                    MainWindow.Instance.Question.Children.Clear();
                    UILock.Instance.Free();
                }

                else if (TF.T.IsChecked == false && TF.F.IsChecked == false)
                {
                    MessageBox.Show("Please select an answer before pressing Submit, Dingus.");
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
