﻿using System;
using System.Windows;
using System.Windows.Input;

namespace TriviaMazeGUI
{
    public partial class MainWindow : Window
    {
        
        internal MazeGridBuilder maze;
        internal readonly SaveLoadManager SaveLoad = new SaveLoadManager();
        internal const string ConnectionInfo = @"Data Source=TriviaMazeQuestions.db;Version=3;";

        private static readonly Lazy<MainWindow> Lazy = new Lazy<MainWindow> (()=> new MainWindow());
        public static MainWindow Instance => Lazy.Value;

        private About about;
        private Instructions instruction;
        private int MazeSize = 4;

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
    }
}
