using System;
using System.Windows;
using System.Data.SQLite;

namespace TriviaMazeGUI
{
    public partial class MainWindow : Window
    {
        
        internal MazeGridBuilder maze;

        private static readonly Lazy<MainWindow> Lazy = new Lazy<MainWindow> (()=> new MainWindow());
        public static MainWindow Instance => Lazy.Value;

        private readonly SQLiteConnection connection;
        public SQLiteConnection getConnection { get { return this.connection; } }

        private About about;
        private Instructions instruction;

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
        }

        public void BuildMaze(int size)
        {
            maze = new MazeGridBuilder();
            maze.Build(size, Board);
            UILock.Instance.Initialize(maze.Entry);

            maze.WrapDoorsWithQuestions();
            //maze.WrapTest();
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

        private void AddQuestionToDB_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionToDB addQuestion = new AddQuestionToDB();
            addQuestion.Show();
        }
    }
}
