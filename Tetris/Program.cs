using System;
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

                        Console.Clear();
                        if (play)
                        {
                            game = new Game();
                            result = game.Loop();

                        }
                        break;
                    case "2":
                    case "View Highscore":
                    case "view highscore":
                    case "View highscore":
                    case "View":
                    case "view":
                        Console.WriteLine("Please add a way to save highscore before checking highscore.");
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
