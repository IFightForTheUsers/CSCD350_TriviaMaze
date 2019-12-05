using System.Windows.Media;

namespace TriviaMazeGUI
{
    /// <summary>
    /// Quick edit fields for default appearance of application
    /// </summary>
    class Regulations
    {
        public const int roomPixelSize = 80;
        public static readonly SolidColorBrush hereColor = Brushes.Pink;
        public static readonly SolidColorBrush disabledColor = Brushes.Gray;
        public static readonly SolidColorBrush validMoveColor = Brushes.Blue;
        public static readonly SolidColorBrush visitedMoveColor = Brushes.Aqua;
    }
}
