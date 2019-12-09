using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace TriviaMazeGUI
{
    class SaveLoadManager
    {
        internal const string SaveFile = "save.dat";

        public void SaveClick(object sender, RoutedEventArgs e)
        {
            // this will save the maze
            if (File.Exists(SaveFile))
                File.Delete(SaveFile);

            using (FileStream f = File.Create(SaveFile))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(f, MainWindow.Instance.maze);
            }
        }

        public void LoadClick(object sender, RoutedEventArgs e)
        {
            // if saveFile doesn't exist just exit
            if (!File.Exists(SaveFile))
                return;

            // this will load the maze
            MazeGridBuilder m;
            BinaryFormatter b = new BinaryFormatter();

            using (FileStream f = File.OpenRead(SaveFile))
            {
                m = (MazeGridBuilder)b.Deserialize(f);
            }

            // maze should be reloaded... now to rewrite it to the windows
            MainWindow.Instance.ResetWindow();
            MainWindow.Instance.maze = m;
            m.Rebuild(MainWindow.Instance.Board);
            MainWindow.Instance.AllowSave = true;

            // starts you back at the entrance for now... will look into saving position data too
            UILock.Instance.LoadAt(m.At);
        }
    }
}
