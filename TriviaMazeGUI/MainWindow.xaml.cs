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
        enum QuestionType{ TrueFalse = 0, MultipleChoice = 1, ShortAnswer = 2 }
        MazeGridBuilder maze;

        private SQLiteConnection connection { get; set; }
        private About about;
        private Instructions instruction;
        private QuestionType questionType;
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Loaded");

            //UserPrompt userPromptWindow = new UserPrompt();
            //userPromptWindow.Show();

            maze = new MazeGridBuilder();
            maze.Build(4, Board);

            game = new Game();
            game.OpenSQLConnection();

            MultipleChoiceQuestion();

            //loadQuestion();

            //string name = userPromptWindow.NameEntry.Text;
            //MessageBox.Show(name);
        }

        public void loadQuestion(SQLiteDataReader dr)
        {
            // random generate 0, 1, or 2 to decide what type of question?

            questionType = QuestionType.MultipleChoice;

            getQuestionType();
        }

        private void getQuestionType()
        {
            if (questionType == QuestionType.TrueFalse)
            {
                TrueFalseQuestion();
            }
            if (questionType == QuestionType.MultipleChoice)
            {
                MultipleChoiceQuestion();
            }
            if (questionType == QuestionType.ShortAnswer)
            {
                ShortAnswerQuestion();
            }
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

        private void TrueFalseQuestion()
        {
            TextBlock q = new TextBlock();
            q.FontSize = 20;
            q.Margin = new Thickness(20);

            RadioButton t = new RadioButton();
            RadioButton f = new RadioButton();
            t.Margin = new Thickness(10);
            f.Margin = new Thickness(10);

            StackPanel sp = new StackPanel();

            sp.Children.Add(q);
            sp.Children.Add(t);
            sp.Children.Add(f);

            Question.Children.Add(sp);

            Canvas.SetTop(sp, 50);
            Canvas.SetLeft(sp, 50);

            q.Text = "[ Question ]";
            t.Content = "True";
            f.Content = "False";
            this.questionType = QuestionType.TrueFalse;

        }

        private void MultipleChoiceQuestion()
        {
            TextBlock q = new TextBlock();
            q.FontSize = 20;
            q.Margin = new Thickness(20);

            RadioButton a = new RadioButton();
            RadioButton b = new RadioButton();
            RadioButton c = new RadioButton();
            RadioButton d = new RadioButton();
            a.Margin = new Thickness(10);
            b.Margin = new Thickness(10);
            c.Margin = new Thickness(10);
            d.Margin = new Thickness(10);

            StackPanel sp = new StackPanel();

            sp.Children.Add(q);
            sp.Children.Add(a);
            sp.Children.Add(b);
            sp.Children.Add(c);
            sp.Children.Add(d);

            Question.Children.Add(sp);

            Canvas.SetTop(sp, 50);
            Canvas.SetLeft(sp, 50);

            q.Text = "[ Question ]";
            a.Content = "A";
            b.Content = "B";
            c.Content = "C";
            d.Content = "D";
            this.questionType = QuestionType.MultipleChoice;
        }

        private void ShortAnswerQuestion()
        {
            TextBlock q = new TextBlock();
            q.FontSize = 20;
            q.Margin = new Thickness(20);

            TextBlock ans = new TextBlock();
            ans.Text = "Answer: ";
            ans.Margin = new Thickness(20, 20, 20, 0);

            TextBox input = new TextBox();
            input.Margin = new Thickness(20, 5, 20, 20);

            StackPanel sp = new StackPanel();

            sp.Children.Add(q);
            sp.Children.Add(ans);
            sp.Children.Add(input);

            Question.Children.Add(sp);

            Canvas.SetTop(sp, 50);
            Canvas.SetLeft(sp, 50);

            q.Text = "[ Question ]";
            this.questionType = QuestionType.ShortAnswer;
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            Question.Children.GetType();
            if (this.questionType.Equals(QuestionType.TrueFalse))
            {

            }
            else if(this.questionType.Equals(this.questionType.Equals(QuestionType.MultipleChoice)))
            {

            }
            else if (this.questionType.Equals(QuestionType.ShortAnswer))
            {

            }

            Question.Children.Clear();
        }


    }
}
