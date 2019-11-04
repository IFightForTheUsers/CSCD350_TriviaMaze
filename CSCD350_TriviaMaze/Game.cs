﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Data.SQLite;

namespace CSCD350_TriviaMaze
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

        }

        public void OpenSQLConnection()
        {
            SQLiteCommand command = null;
            if (!(File.Exists(@"")))
            {
                SQLiteConnection.CreateFile(@"");
                connection = new SQLiteConnection(@"");
                connection.Open();
                command = new SQLiteCommand(@"", connection);

            }
            command.ExecuteReader();

        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Game Initialized...");
            OpenSQLConnection();
            string name;
            Regex nameCheck = new Regex(@"^[A-Z]{1}[a-z]{1,15}, [A-Z]{1}[a-z]{1,15}, [A-Z](\.[A-Z]){0,2}$");
            do {
                Console.Write("\nEnter Player Name --> ");
                name = Console.ReadLine();
                if (nameCheck.IsMatch(name))
                {
                    player.Name = name;
                }
                else
                {
                    name = null;
                }
            } while (name == null);
            Console.ReadLine();
        }
    }
}
