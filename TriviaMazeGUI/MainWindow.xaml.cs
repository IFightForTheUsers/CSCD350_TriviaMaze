using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace TriviaMazeGUI
{
    public partial class MainWindow : Window
    {
        
        MazeGridBuilder maze;

        private static readonly Lazy<MainWindow> lazy = new Lazy<MainWindow> (()=> new MainWindow());
        public static MainWindow Instance { get { return lazy.Value; } }

        private SQLiteConnection connection;
        public SQLiteConnection getConnection { get { return this.connection; } }

        private About about;
        private Instructions instruction;
        private QuestionType questionType;
        private Game game;


        private MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            connection = new SQLiteConnection(@"Data Source=TriviaMazeQuestions.db;Version=3;");
            connection.Open();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartPrompt prompt = new StartPrompt();
            Question.Children.Add(prompt);

            game = new Game();
            game.OpenSQLConnection();

            //MultipleChoiceQuestion();

            //loadQuestion();

            //string name = userPromptWindow.NameEntry.Text;
            //MessageBox.Show(name);
        }

        public void BuildMaze(int size)
        {
            maze = new MazeGridBuilder();
            maze.Build(size, Board);
            UILock.Instance.Initialize(maze.Entry);

            maze.WrapDoorsWithQuestions();
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuStop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void mnuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            this.about = new About();
            this.about.Show();

        }

        private void mnuInstructions_Click(object sender, RoutedEventArgs e)
        {
            this.instruction = new Instructions();
            this.instruction.Show();
        }

        //Canvas will only ever have one child
        private void TrueFalseAnswer()
        {
            StackPanel temp = (StackPanel)Question.Children[0];
            RadioButton btnA = (RadioButton)temp.Children[1];
            RadioButton btnB = (RadioButton)temp.Children[2];
            if (btnA.IsChecked == true)
            {
                MessageBox.Show("i");
            }
            else if (btnB.IsChecked == false)
            {

            }
        }
        private void MultipleChoiceAnswer()
        {
            StackPanel temp = (StackPanel)Question.Children[0];
            RadioButton btnA = (RadioButton)temp.Children[1];
            RadioButton btnB = (RadioButton)temp.Children[2];
            RadioButton btnC = (RadioButton)temp.Children[3];
            RadioButton btnD = (RadioButton)temp.Children[4];
            int paramOne = 2;
            SQLiteCommand ins = new SQLiteCommand(@"SELECT * FROM MultipleChoice WHERE Q = @1", connection);
            ins.Parameters.Add(new SQLiteParameter("@1", paramOne));
            using (SQLiteDataReader read = ins.ExecuteReader())
            {
                if (read.Read())
                {
                    if (btnA.IsChecked == true)
                    {
                        if ((string)read["answer"] == "A")
                        {
                            MessageBox.Show("Correct Answer");
                        }
                        else
                        {
                            MessageBox.Show("Wrong Answer");
                        }
                    }
                    else if (btnB.IsChecked == true)
                    {
                        if ((string)read["answer"] == "B")
                        {
                            MessageBox.Show("Correct Answer");
                        }
                        else
                        {
                            MessageBox.Show("Wrong answer");
                        }
                    }
                    else if (btnC.IsChecked == true)
                    {
                        if ((string)read["answer"] == "C")
                        {
                            MessageBox.Show("Correct Answer");
                        }
                        else
                        {
                            MessageBox.Show("Wrong Answer");
                        }
                    }
                    else if (btnD.IsChecked == true)
                    {
                        if ((string)read["answer"] == "D")
                        {
                            MessageBox.Show("Correct Answer");
                        }
                        else
                        {
                            MessageBox.Show("Wrong Answer");
                        }
                    }
                }
                read.Close();
            }

        }
        private void ShortAnswerAnswer()
        {
            StackPanel temp = (StackPanel)Question.Children[0];
        }

        private void AddQuestionToDB_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionToDB addQuestion = new AddQuestionToDB();
            addQuestion.Show();
        }
    }
}
