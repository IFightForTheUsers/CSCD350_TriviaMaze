﻿using System;
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

namespace TriviaMazeGUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class VictoryPrompt : UserControl
    {
        public VictoryPrompt()
        {
            InitializeComponent();
            this.NewMaze.Click += MainWindow.Instance.NewOnClick;
            this.LoadMaze.Click += MainWindow.Instance.SaveLoad.LoadClick;
        }
    }
}
