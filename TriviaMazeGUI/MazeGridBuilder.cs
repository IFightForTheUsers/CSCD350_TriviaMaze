using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections;
using System.Data.SQLite;

namespace TriviaMazeGUI
{

    public class MazeGridBuilder
    {
        private Grid grid;
        private Room[,] rooms;
        private Entrance ingress;
        internal Entrance Entry { get { return ingress; } }
        private Exit egress;
       // public Exit Egress { get { return egress; } }
        enum QuestionType { TrueFalse = 0, MultipleChoice = 1, ShortAnswer = 2 };

        public bool check()
        {
            bool temp = Solveable.CheckIfSolveable(ref this.rooms);
            ResetFlags();
            return temp;
        }
        public void ResetFlags()
        {
            for(int i = 0; i < rooms.GetLength(0); i++)
            {
                for(int j = 0; j < rooms.GetLength(1); j++)
                {
                    rooms[i, j].Flag = false;
                }
            }
        }
        public String ButtonName(int x, int y)
        {
            return "b_x" + x + "y" + y;
        }
        public void Build(int n, Grid plop)
        {
            grid = plop;
            rooms = new Room[n, n];

            grid.Width = n * Regulations.roomPixelSize;
            grid.Height = n * Regulations.roomPixelSize;
            grid.Margin = new Thickness(30);
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ShowGridLines = true;

            // make the grid!
            for (int i = 0; i < n; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // now we add buttons and rooms!
            for (int y = 0; y < n; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    Button temp = new Button();
                    rooms[x, y] = new Room();
                    //Room lambda_var = rooms[x, y];
                    //temp.Click += (s, e) => { lambda_var.Clicked(); };
                    rooms[x, y].button = temp;
                    rooms[x, y].button.Name = ButtonName(x, y);
                    rooms[x, y].button.Height = Regulations.roomPixelSize;
                    rooms[x, y].button.Width = Regulations.roomPixelSize;
                    rooms[x, y].button.Background = Regulations.disabledColor;
                    rooms[x, y].button.IsEnabled = false; // no clicky button! not yet!
                    grid.Children.Add(rooms[x, y].button);
                    Grid.SetRow(rooms[x, y].button, x);
                    Grid.SetColumn(rooms[x, y].button, y);
                }
            }
            for (int i = 0; i < n; i++)
            { // add walls to maze edges
                rooms[i, 0].north = new Wall(rooms[i, 0]);
                rooms[0, i].west = new Wall(rooms[0, i]);
                rooms[n - 1, i].east = new Wall(rooms[n - 1, i]);
                rooms[i, n - 1].south = new Wall(rooms[i, n - 1]);
            }
            // corner check for walls
            //NW corner
            Debug.Assert(rooms[0, 0].north != null);
            Debug.Assert(rooms[0, 0].west != null);
            //NE corner
            Debug.Assert(rooms[n - 1, 0].north != null);
            Debug.Assert(rooms[n - 1, 0].east != null);
            //SW corner
            Debug.Assert(rooms[0, n - 1].south != null);
            Debug.Assert(rooms[0, n - 1].west != null);
            //SE corner
            Debug.Assert(rooms[n - 1, n - 1].south != null);
            Debug.Assert(rooms[n - 1, n - 1].east != null);
            for (int y = 0; y < n - 1; y++)
            { // now we add doors
                for (int x = 0; x < n; x++)
                {
                    _ = new Door(rooms[x, y], rooms[x, y + 1], 'e');
                }
            }
            Debug.Assert(rooms[0, 0].south == rooms[0, 1].north);
            Debug.Assert(rooms[n - 1, n - 1].north == rooms[n - 1, n - 2].south);
            for (int y = 0; y < n; y++)
            { // now we add doors
                for (int x = 0; x < n - 1; x++)
                {
                    _ = new Door(rooms[x, y], rooms[x + 1, y], 's');
                }
            }
            // hard add the entry and exit
            ingress = new Entrance(rooms[0, 0]);
            rooms[0, 0].west = ingress;
            egress = new Exit(rooms[n - 1, n - 1]);
            rooms[n - 1, n - 1].east = egress;
            rooms[3, 3].button.Content = "Exit";
        }

        public void WrapTest()
        {
            foreach (Room r in rooms)
            { // wrap all the Doors with TestQuestions...
                if (r.east is Door)
                    _ = new TestQuestion(r.east);
                if (r.south is Door)
                    _ = new TestQuestion(r.south);
            }
        }

        public void WrapDoorsWithQuestions()
        {
            
            SQLiteCommand command;

            ArrayList TrueFalseQNum = new ArrayList();
            command = new SQLiteCommand("SELECT COUNT(*) FROM TrueFalse", MainWindow.Instance.getConnection);
            int TFCount = int.Parse(command.ExecuteScalar().ToString()); 

            for (int a = 1; a <= TFCount; a++)
            {
                TrueFalseQNum.Add(a);
            }

            //MessageBox.Show("TFCount: " + TFCount);
            //TrueFalseTable.Count;

            ArrayList MultipleChoiceQNum = new ArrayList();
            command = new SQLiteCommand("SELECT COUNT(*) FROM MultipleChoice", MainWindow.Instance.getConnection);
            int MCCount = int.Parse(command.ExecuteScalar().ToString());

            for (int a = 1; a <= MCCount; a++)
            {
                MultipleChoiceQNum.Add(a);
            }

            ArrayList ShortAnswerQNum = new ArrayList();
            command = new SQLiteCommand("SELECT COUNT(*) FROM ShortAnswer", MainWindow.Instance.getConnection);
            int SACount = int.Parse(command.ExecuteScalar().ToString());

            for (int a = 1; a <= SACount; a++)
            {
                ShortAnswerQNum.Add(a);
            }

            Random rand = new Random();
            int randomNumberForQuestionNumber;
            QuestionType questionType; // just to give it a default value to shut the compiler up and since it's not a nullable type;
            
            foreach (Room r in rooms)
            {
                questionType = GetRandomQuestionType();

                if (r.east is Door)
                {
                    if (questionType == QuestionType.TrueFalse)
                    {
                        if (TrueFalseQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomTFQuestion();
                            _ = new TrueFalseQuestion(r.east, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (MultipleChoiceQNum.Count != 0)
                            {
                                randomNumberForQuestionNumber = GetRandomMCQuestion();
                                _ = new MultipleChoiceQuestion(r.east, randomNumberForQuestionNumber);
                            }
                            else
                            {
                                randomNumberForQuestionNumber = GetRandomSAQuestion();
                                _ = new ShortAnswerQuestion(r.east, randomNumberForQuestionNumber);
                            }
                        }

                    }
                    else if (questionType == QuestionType.MultipleChoice)
                    {
                        if (MultipleChoiceQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomMCQuestion();
                            _ = new MultipleChoiceQuestion(r.east, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (ShortAnswerQNum.Count != 0)
                            {
                                randomNumberForQuestionNumber = GetRandomSAQuestion();
                                _ = new ShortAnswerQuestion(r.east, randomNumberForQuestionNumber);
                            }
                            else
                            {
                                randomNumberForQuestionNumber = GetRandomTFQuestion();
                                _ = new TrueFalseQuestion(r.east, randomNumberForQuestionNumber);
                            }
                        }

                    }
                    else if (questionType == QuestionType.ShortAnswer)
                    {
                        if (ShortAnswerQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomSAQuestion();
                            _ = new ShortAnswerQuestion(r.east, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (TrueFalseQNum.Count != 0)
                            {
                                randomNumberForQuestionNumber = GetRandomTFQuestion();
                                _ = new TrueFalseQuestion(r.east, randomNumberForQuestionNumber);
                            }
                            else
                            {
                                randomNumberForQuestionNumber = GetRandomMCQuestion();
                                _ = new MultipleChoiceQuestion(r.east, randomNumberForQuestionNumber);
                            }
                        }
                    }

                }

                questionType = GetRandomQuestionType();


                if (r.south is Door)
                {
                    if (questionType == QuestionType.TrueFalse)
                    {
                        if (TrueFalseQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomTFQuestion();
                            _ = new TrueFalseQuestion(r.south, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (MultipleChoiceQNum.Count != 0)
                            {
                                randomNumberForQuestionNumber = GetRandomMCQuestion();
                                _ = new MultipleChoiceQuestion(r.south, randomNumberForQuestionNumber);
                            }
                            else
                            {
                                randomNumberForQuestionNumber = GetRandomSAQuestion();
                                _ = new ShortAnswerQuestion(r.south, randomNumberForQuestionNumber);
                            }
                        }

                    }
                    else if (questionType == QuestionType.MultipleChoice)
                    {
                        if (MultipleChoiceQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomMCQuestion();
                            _ = new MultipleChoiceQuestion(r.south, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (ShortAnswerQNum.Count != 0)
                            {
                                randomNumberForQuestionNumber = GetRandomSAQuestion();
                                _ = new ShortAnswerQuestion(r.south, randomNumberForQuestionNumber);
                            }
                            else
                            {
                                randomNumberForQuestionNumber = GetRandomTFQuestion();
                                _ = new TrueFalseQuestion(r.south, randomNumberForQuestionNumber);
                            }
                        }

                    }
                    else if (questionType == QuestionType.ShortAnswer)
                    {
                        if (ShortAnswerQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomSAQuestion();
                            _ = new ShortAnswerQuestion(r.south, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (TrueFalseQNum.Count != 0)
                            {
                                randomNumberForQuestionNumber = GetRandomTFQuestion();
                                _ = new TrueFalseQuestion(r.south, randomNumberForQuestionNumber);
                            }
                            else
                            {
                                randomNumberForQuestionNumber = GetRandomMCQuestion();
                                _ = new MultipleChoiceQuestion(r.south, randomNumberForQuestionNumber);
                            }
                        }
                    }
                }

            }

            QuestionType GetRandomQuestionType()
            {
                int randomNumber = rand.Next(0, 3); // 0 for TF, 1 for MC, 2 for SA

                if (randomNumber == 0)
                {
                    return QuestionType.TrueFalse;
                }
                else if (randomNumber == 1)
                {
                    return QuestionType.MultipleChoice;
                }
                else if (randomNumber == 2)
                {
                    return QuestionType.ShortAnswer;
                }
                else
                {
                    return QuestionType.MultipleChoice;
                }
            }

            int GetRandomTFQuestion()
            {
                //Console.WriteLine("Number of unassigned TrueFalse questions: " + TrueFalseQNum.Count);

                int index = rand.Next(0, TrueFalseQNum.Count);

                int valueAtIndex = (int)TrueFalseQNum[index];
                TrueFalseQNum.RemoveAt(index);
                return valueAtIndex;
            }

            int GetRandomMCQuestion()
            {
                //Console.WriteLine("Number of unassigned MultipleChoice questions: " + MultipleChoiceQNum.Count);

                int index = rand.Next(0, MultipleChoiceQNum.Count);

                int valueAtIndex = (int)MultipleChoiceQNum[index];
                MultipleChoiceQNum.RemoveAt(index);
                return valueAtIndex;

            }

            int GetRandomSAQuestion()
            {
                //Console.WriteLine("Number of unassigned ShortAnswer questions: " + ShortAnswerQNum.Count);

                int index = rand.Next(0, ShortAnswerQNum.Count);

                int valueAtIndex = (int)ShortAnswerQNum[index];
                ShortAnswerQNum.RemoveAt(index);
                return valueAtIndex;
            }
        }
    }
}
