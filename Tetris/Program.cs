﻿using System;
using System.Collections.Generic;
using TetrisClassLibrary;
using System.Collections.Generic;

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

                Console.Clear();
                if (play)
                {
                    game = new Game();
                    game.Loop();

                    //print spelplan
                    //print tetromino J 
                    //print points
                    //clear
                }

                Console.ReadKey();
            }
        }
    }
}
