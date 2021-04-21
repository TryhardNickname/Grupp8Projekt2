using System;
using TetrisClassLibrary;
using System.Media;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer tetrisMusic = new SoundPlayer("Tetris_theme.wav");
                tetrisMusic.Load();
                tetrisMusic.PlayLooping();
            }


            string input;
            bool loop = true;
            while (loop)
            {
                //console menu
                Console.WriteLine("Do you wanna play tetris");
                Console.WriteLine("1. Play");
                Console.WriteLine("2. View Highscore");
                Console.WriteLine("0. Quit");
                input = GetMenuInput(3);
                switch (input)
                {
                    case "1":
                        Game game = new Game();  


                        Console.WriteLine("What level do you want to play on? [0-9]");
                        input = GetMenuInput(10);
                        game.MyScore.SetLevel(int.Parse(input));

                        //game started
                        Console.Clear();
                        game.Loop();
                        game.MyScore.SaveHighScore();
                        Console.Clear();
                        break;

                    case "2":
                        Console.WriteLine("Your highscore is " + Score.LoadHighScore());
                        break;

                    case "0":
                        Console.WriteLine("Bye bye!");
                        //Console.ReadKey();
                        loop = false;
                        break;

                    default:
                        Console.WriteLine("Please write a valid option.");
                        break;
                }
            }
        }

        private static string GetMenuInput(int amountOfChoices)
        {
            string userInput = Console.ReadLine();

            while (true)
            {
                if (int.TryParse(userInput, out int num))
                {
                    if (num < amountOfChoices && num >= 0) 
                    {
                        return userInput;
                    }
                }
                Console.WriteLine("Fel input, försök igen: ");
                userInput = Console.ReadLine();
            }
        }
    }
}
