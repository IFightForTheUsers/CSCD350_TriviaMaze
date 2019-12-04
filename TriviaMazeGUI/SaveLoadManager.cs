using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace TriviaMazeGUI
{
    class SaveLoadManager
    {
        public static readonly string saveFile = "save.dat";

        public void SaveClick(object sender, RoutedEventArgs e)
        {
            // this will save the maze
            if (File.Exists(saveFile))
                File.Delete(saveFile);

            using (FileStream f = File.Create(saveFile))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(f, MainWindow.Instance.maze);
            }

        }

        public void LoadClick(object sender, RoutedEventArgs e)
        {
            // if saveFile doesn't exist just exit
            if (!File.Exists(saveFile))
                return;

            // this will load the maze
            MazeGridBuilder m;
            BinaryFormatter b = new BinaryFormatter();

            using (FileStream f = File.OpenRead(saveFile))
            {
                m = (MazeGridBuilder) b.Deserialize(f);
            }

            MainWindow.Instance.ResetWindow();
            MainWindow.Instance.maze = m;
            m.Rebuild(MainWindow.Instance.Board);
        }
    }
}
