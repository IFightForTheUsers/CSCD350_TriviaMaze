namespace TriviaMazeGUI
{
    class Player
    {
        private string name;
        private Room roomIn;
        public Room at
        {
            get => roomIn;
            set => roomIn = value;
        }

        public Player(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get => name;
            set
            {
                if (value != null)
                {
                    name = value;
                }
            }
        }
    }
}
