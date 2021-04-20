﻿using System;
using TetrisClassLibrary;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            bool loop = true;
            int result = 0;
            while (loop)
            {
                //console menu
                Console.WriteLine("Do you wanna play tetris");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. View Highscore");
                Console.WriteLine("0. Quit");
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                    case "Play":
                    case "play":
                    case "Start":
                    case "start":
                        Game game;
                        bool play = true;
                        Console.WriteLine("What level do you want to play on?");
                        input = Console.ReadLine();
                        Score.SetLevel(int.Parse(input));
                        Console.Clear();
                        if (play)
                        {
                            game = new Game();
                            result = game.Loop();
                            Score.SaveHighScore();
                        }
                        Console.Clear();
                        break;
                    case "2":
                    case "View Highscore":
                    case "view highscore":
                    case "View highscore":
                    case "View":
                    case "view":
                        Console.WriteLine("Your highscore is " + Score.LoadHighScore());
                        break;
                    case "0":
                    case "Quit":
                    case "quit":
                        Console.WriteLine("Bye bye!");
                        Console.ReadKey();
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Please write a valid option.");
                        break;
                }
            }
        }
    }
}
