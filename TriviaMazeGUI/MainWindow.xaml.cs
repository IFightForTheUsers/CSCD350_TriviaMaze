using System;
using System.Windows;
using System.Windows.Input;
using File = System.IO.File;

namespace TriviaMazeGUI
{
    public partial class MainWindow : Window
    {
        
        internal MazeGridBuilder maze;
        internal readonly SaveLoadManager SaveLoad = new SaveLoadManager();
        internal static string ConnectionInfo = "";
        private bool topicChoosen = false;
        private static readonly Lazy<MainWindow> Lazy = new Lazy<MainWindow> (()=> new MainWindow());
        public static MainWindow Instance => Lazy.Value;

        private DataBaseView dataBaseView;
        private About about;
        private Instructions instruction;
        private int MazeSize = 4;
        private string selection;

        internal bool AllowSave = false;

        private MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartPrompt prompt = new StartPrompt();
            Question.Children.Add(prompt);
        }

        public void BuildMaze(int size)
        {
            MazeSize = size;
            maze = new MazeGridBuilder();
            maze.Build(size, Board);
            UILock.Instance.Initialize(maze.Entry);

            maze.WrapDoorsWithQuestions();
            AllowSave = true;
            //maze.WrapTest();
        }


        public void ResetWindow()
        {
            Board.Children.Clear();
            Board.RowDefinitions.Clear();
            Board.ColumnDefinitions.Clear();
            Question.Children.Clear();
        }

        public void NewOnClick(object sender, RoutedEventArgs e)
        {
            ResetWindow();
            BuildMaze(MazeSize);
        }

        private void File_Click(object sender, RoutedEventArgs e)
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

        private void Save_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveLoad.SaveClick(sender, e);
        }

        private void Load_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveLoad.LoadClick(sender, e);
        }

        private void New_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ResetWindow();
            BuildMaze(MazeSize);
        }

        private void AllowSave_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = AllowSave;
        }

        private void mnuOpenDBWindow_Click(object sender, RoutedEventArgs e)
        {
            if (topicChoosen)
            {
                this.dataBaseView = new DataBaseView();
                this.dataBaseView.Show();
            }

        }

        private void AllowLoad_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (System.IO.File.Exists(SaveLoadManager.SaveFile))
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
        private void _enter_DatabaseChoice(object sender, RoutedEventArgs e)
        {
            if (extComboBox.Text != null)
            {
                selection = extComboBox.Text;
                extComboBox.Text = null;
                topicChoosen = true;
                DataBaseChoice(selection);
            }

        }
        private static void DataBaseChoice(string s)
        {
            switch (s) 
            {
                case "CSQuestions":
                    ConnectionInfo = @"Data Source=TriviaMazeQuestions.db;Version=3;";
                    break;
                case "RandomShh":
                    ConnectionInfo = @"Data Source=MetalQuestions.db;Version=3;";
                    break;
                default:
                    break;
            }
        }
    }
}
