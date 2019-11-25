using System.Windows;

namespace TriviaMazeGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            TriviaMazeGUI.MainWindow.Instance.Show();
        }
    }
}
