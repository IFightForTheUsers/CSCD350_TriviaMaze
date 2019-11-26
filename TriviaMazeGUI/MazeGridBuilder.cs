using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Collections;
using System.Data.SQLite;

namespace TriviaMazeGUI
{

    public partial class MazeGridBuilder
    {
        private Grid grid;
        private static Room[,] rooms;
        private Entrance ingress;
        internal Entrance Entry { get { return ingress; } }
        private Exit egress;
       // public Exit Egress { get { return egress; } }
        enum QuestionType { TrueFalse = 0, MultipleChoice = 1, ShortAnswer = 2 };

        public static bool check()
        {
            bool temp = Solveable.CheckIfSolveable(rooms);
            ResetFlags();
            return temp;
        }
        public static void ResetFlags()
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

            ArrayList TrueFalseTable = new ArrayList();
            command = new SQLiteCommand("SELECT COUNT(*) FROM TrueFalse", MainWindow.Instance.getConnection);
            int TFCount = int.Parse(command.ExecuteScalar().ToString());
            //MessageBox.Show("TFCount: " + TFCount);
            //TrueFalseTable.Count;

            ArrayList MultipleChoiceTable = new ArrayList();
            command = new SQLiteCommand("SELECT COUNT(*) FROM MultipleChoice", MainWindow.Instance.getConnection);
            int MCCount = int.Parse(command.ExecuteScalar().ToString());

            ArrayList ShortAnswerTable = new ArrayList();
            command = new SQLiteCommand("SELECT COUNT(*) FROM ShortAnswer", MainWindow.Instance.getConnection);
            int SACount = int.Parse(command.ExecuteScalar().ToString());

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
                        randomNumberForQuestionNumber = rand.Next(0, TFCount);
                        _ = new TrueFalseQuestion(r.east, randomNumberForQuestionNumber);
                    }
                    else if (questionType == QuestionType.MultipleChoice)
                    {
                        randomNumberForQuestionNumber = rand.Next(0, MCCount);
                        _ = new MultipleChoiceQuestion(r.east, randomNumberForQuestionNumber);
                    }
                    else if (questionType == QuestionType.ShortAnswer)
                    {
                        randomNumberForQuestionNumber = rand.Next(0, SACount);
                        _ = new ShortAnswerQuestion(r.east, randomNumberForQuestionNumber);
                    }
                }

                questionType = GetRandomQuestionType();


                if (r.south is Door)
                {
                    if (questionType == QuestionType.TrueFalse)
                    {
                        randomNumberForQuestionNumber = rand.Next(0, TFCount);
                        _ = new TrueFalseQuestion(r.south, randomNumberForQuestionNumber);
                    }
                    else if (questionType == QuestionType.MultipleChoice)
                    {
                        randomNumberForQuestionNumber = rand.Next(0, MCCount);
                        _ = new MultipleChoiceQuestion(r.south, randomNumberForQuestionNumber);
                    }
                    else if (questionType == QuestionType.ShortAnswer)
                    {
                        randomNumberForQuestionNumber = rand.Next(0, SACount);
                        _ = new ShortAnswerQuestion(r.south, randomNumberForQuestionNumber);
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
        }
    }
}
