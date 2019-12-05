using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections;
using System.Data.SQLite;

namespace TriviaMazeGUI
{
    [Serializable]
    internal class MazeGridBuilder
    {
        private Room[,] rooms;
        private int size;
        private Entrance ingress;
        internal Entrance Entry => ingress;
        private Exit egress;
       // public Exit Egress { get { return egress; } }
       internal Room At;
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
        public static String ButtonName(int x, int y)
        {
            return "b_x" + x + "y" + y;
        }
        public void Build(int n, Grid grid)
        {
            if (grid==null)
                throw new ArgumentNullException(nameof(grid), @"MazeGridBuilder.Build does not accept nulls.");

            this.size = n;
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

        internal void Rebuild(Grid grid)
        {
            // this will rebuild the maze in a window after being DeSerialized
            // remake the grid!

            grid.Width = size * Regulations.roomPixelSize;
            grid.Height = size * Regulations.roomPixelSize;
            grid.Margin = new Thickness(30);
            grid.HorizontalAlignment = HorizontalAlignment.Left;
            grid.VerticalAlignment = VerticalAlignment.Top;
            grid.ShowGridLines = true;

            for (int i = 0; i < size; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    Button temp = new Button();
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

            foreach (Room r in rooms)
            {
                if (r.Visited)
                    r.button.Content = "Visited";
                if (r.east is Exit)
                    r.button.Content = "Exit";
            }
        }

        public void WrapDoorsWithQuestions()
        {
            ArrayList trueFalseQNum = new ArrayList();
            ArrayList multipleChoiceQNum = new ArrayList();
            ArrayList shortAnswerQNum = new ArrayList();

            using (SQLiteConnection connection = new SQLiteConnection(MainWindow.ConnectionInfo))
            {
                connection.Open();
                var TFCount = 0;
                SQLiteCommand command;
                using (command = new SQLiteCommand("SELECT COUNT(*) FROM TrueFalse", connection))
                {
                    TFCount = int.Parse(command.ExecuteScalar().ToString());
                }

                if (TFCount == 0)
                    throw new DatabaseReadException("TFCount returned 0");

                for (int a = 0; a < TFCount; a++)
                {
                    trueFalseQNum.Add(a);
                }

                var MCCount = 0;
                using (command = new SQLiteCommand("SELECT COUNT(*) FROM MultipleChoice", connection))
                {
                    MCCount = int.Parse(command.ExecuteScalar().ToString());
                }

                if (MCCount == 0)
                    throw new DatabaseReadException("MCCount returned 0");

                for (int a = 0; a < MCCount; a++)
                {
                    multipleChoiceQNum.Add(a);
                }

                var SACount = 0;
                using (command = new SQLiteCommand("SELECT COUNT(*) FROM ShortAnswer", connection))
                {
                    SACount = int.Parse(command.ExecuteScalar().ToString());
                }

                if (SACount == 0)
                    throw new DatabaseReadException("SACount returned 0");

                for (int a = 0; a < SACount; a++)
                {
                    shortAnswerQNum.Add(a);
                }
                connection.Close();
            }

            Random rand = new Random();

            foreach (Room r in rooms)
            {
                var questionType = GetRandomQuestionType(); // just to give it a default value to shut the compiler up and since it's not a nullable type;

                int randomNumberForQuestionNumber;
                if (r.east is Door)
                {
                    if (questionType == QuestionType.TrueFalse)
                    {
                        if (trueFalseQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomTFQuestion();
                            _ = new TrueFalseQuestion(r.east, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (multipleChoiceQNum.Count != 0)
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
                        if (multipleChoiceQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomMCQuestion();
                            _ = new MultipleChoiceQuestion(r.east, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (shortAnswerQNum.Count != 0)
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
                        if (shortAnswerQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomSAQuestion();
                            _ = new ShortAnswerQuestion(r.east, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (trueFalseQNum.Count != 0)
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
                        if (trueFalseQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomTFQuestion();
                            _ = new TrueFalseQuestion(r.south, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (multipleChoiceQNum.Count != 0)
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
                        if (multipleChoiceQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomMCQuestion();
                            _ = new MultipleChoiceQuestion(r.south, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (shortAnswerQNum.Count != 0)
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
                        if (shortAnswerQNum.Count != 0)
                        {
                            randomNumberForQuestionNumber = GetRandomSAQuestion();
                            _ = new ShortAnswerQuestion(r.south, randomNumberForQuestionNumber);
                        }
                        else
                        {
                            if (trueFalseQNum.Count != 0)
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

                int index = rand.Next(0, trueFalseQNum.Count);

                int valueAtIndex = (int)trueFalseQNum[index];
                trueFalseQNum.RemoveAt(index);
                return valueAtIndex;
            }

            int GetRandomMCQuestion()
            {
                //Console.WriteLine("Number of unassigned MultipleChoice questions: " + MultipleChoiceQNum.Count);

                int index = rand.Next(0, multipleChoiceQNum.Count);

                int valueAtIndex = (int)multipleChoiceQNum[index];
                multipleChoiceQNum.RemoveAt(index);
                return valueAtIndex;

            }

            int GetRandomSAQuestion()
            {
                //Console.WriteLine("Number of unassigned ShortAnswer questions: " + ShortAnswerQNum.Count);

                int index = rand.Next(0, shortAnswerQNum.Count);

                int valueAtIndex = (int)shortAnswerQNum[index];
                shortAnswerQNum.RemoveAt(index);
                return valueAtIndex;
            }
        }
    }
}
