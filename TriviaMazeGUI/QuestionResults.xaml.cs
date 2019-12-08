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

namespace TriviaMazeGUI
{
    public partial class QuestionResults : UserControl
    {
        public QuestionResults()
        {
            InitializeComponent();
            LoadMaze.Click += MainWindow.Instance.SaveLoad.LoadClick;
            NewMaze.Click += MainWindow.Instance.NewOnClick;
        }
    }
}
