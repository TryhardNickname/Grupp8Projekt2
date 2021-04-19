using System;
using TetrisClassLibrary;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Hello Menu!");
                //console menu
                Console.WriteLine("Do you wanna play tetris");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. View Highscore");
                Console.WriteLine("0. Quit");
                //if play
                Game game;
                bool play = true;
                int result = 0;

                Console.Clear();
                if (play)
                {
                    game = new Game();
                    result = game.Loop();

                    Console.WriteLine($"You Scored {result} points");
                    Console.WriteLine("Do you want to play again? [y/n]");
                    string input = Console.ReadLine();
                    if (input == "n")
                    {
                        Console.WriteLine("Thank you for playing!");
                        play = false;
                    }
                }

                Console.ReadKey();
            }
        }
    }
}
