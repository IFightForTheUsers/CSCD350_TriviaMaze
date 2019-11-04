using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCD350_TriviaMaze
{
    class Game
    {
        private Player player;
        private Maze maze;

        public Game()
        {
            this.player = new Player("PlayerName");
            this.maze = new Maze();

        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Game Initialized...");
            string name;
            do {
                Console.Write("\nEnter Player Name --> ");
                name = Console.ReadLine();
                player.Name = name;
            } while (name == null);
            Console.ReadLine();
        }
    }
}
