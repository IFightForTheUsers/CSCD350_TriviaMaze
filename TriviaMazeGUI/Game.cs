using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Text.RegularExpressions;

namespace TriviaMazeGUI
{
    class Game
    {

        private Player player;
        private Maze maze;
        private SQLiteConnection connection { get; set; }

        public Game()
        {
            this.player = new Player("PlayerName");
            this.maze = new Maze();
            this.player.at = this.maze.startingRoom;

        }

        public void OpenSQLConnection()
        {
            //SQLiteCommand command = null;
            //if (!(File.Exists(@"Question_Database.sqlite")))
            //{
            //    SQLiteConnection.CreateFile(@"");
            //    connection = new SQLiteConnection(@"");
            //    connection.Open();
            //    command = new SQLiteCommand(@"", connection);

            //}
            //command.ExecuteReader();

            connection = new SQLiteConnection("Data Source=TriviaMazeQuestions.db");
            connection.Open();

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM MultipleChoice WHERE Q=2";

            SQLiteDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Console.WriteLine(dataReader["Q"] + "\n" + 
                                  dataReader["Question"] + "\n" + 
                                  dataReader["A"] + "\n" +
                                  dataReader["B"] + "\n" +
                                  dataReader["C"] + "\n" +
                                  dataReader["D"] + "\n" +
                                  dataReader["Answer"]);

                //MainWindow.loadQuestion(dataReader);
            }
            connection.Close();


        }

        //public static void Main(string[] args)
        //{
        //    Game game = new Game();
        //    Console.WriteLine("Game Initialized...");
        //    // game.OpenSQLConnection();
        //    string name;
        //    Regex nameCheck = new Regex(@"^[A-Za-z0-9]{1,15}$");
        //    do
        //    {
        //        Console.Write("\nEnter Player Name (1-15 characters, alphanumeric no special characters)--> ");
        //        name = Console.ReadLine();
        //        if (nameCheck.IsMatch(name))
        //        {
        //            game.player.Name = name;
        //        }
        //        else
        //        {
        //            name = null;
        //        }
        //    } while (name == null);
        //    Console.ReadLine();
        //}
    }
}
