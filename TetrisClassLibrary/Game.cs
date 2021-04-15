﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace TetrisClassLibrary
{
    public class Game
    {
        public Grid grid { get; set; }

        public Game()
        {
            Start();
        }
        public void Start()
        {
            grid = new Grid();
        }

        string direction;

        /// <summary>
        /// Main Game Loop
        /// takes input, updates fields AND prints to console
        /// NYI - connect with EVENTS to GUI, to seperate resposibilites
        /// </summary>
        public void Loop()
        {
            bool playing = true;
            grid.AddNewRandomTetromino();
            //System.Timers.Timer timer = new System.Timers.Timer();
            //Stopwatch.StartNew();

            int gravity = 20; //20 game tics
            int tickCounter = 0;

            while (playing)
            {
                //GAME TIMING================
                var timeElapsed = Stopwatch.GetTimestamp();
                Thread.Sleep(50); //game tick
                tickCounter++;



                //HANDLE USER INPUT========== check collision etc game logic
                CheckInput();

                //GAME LOGIC =?=============

                if (tickCounter == gravity)
                {
                    if (grid.TryToMoveDown())
                    {
                        // it worked
                    }
                    else
                    {
                        //Check if new tetro can spawn?
                        grid.CanTetroFit(0, 1);
                        grid.AddNewRandomTetromino();
                    }
                    tickCounter = 0;
                }



                //UPDATE GRID ==============



                //DRAW GAME==================
                DrawGameField();
                DrawTetromino();
            }

        }


        private void DrawGameField()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(grid.GridArea[i][j]);
                }
                Console.WriteLine();

            }
        }

        private void DrawTetromino()
        {
            Point pos = grid.CurrentTetromino.GetPos();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (grid.CurrentTetromino.Shape[row][col] == ' ')
                    {
                        //GridArea[pos.Y + row][pos.X + col] = ' ';
                    }
                    else
                    {
                        //GridArea[row][col] = '@';
                        Console.ForegroundColor = grid.CurrentTetromino.Color;
                        Console.SetCursorPosition(pos.X+col, pos.Y+row);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        public void CheckInput()
        {
            ConsoleKey key;
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.A:
                        grid.UpdateTetromino("left");
                        break;
                    case ConsoleKey.D:
                        grid.UpdateTetromino("right");
                        break;
                    case ConsoleKey.Q:
                        grid.UpdateTetromino("rotate");
                        break;
                    default:
                        direction = "null";
                        break;
                }
            }
        }

    }
}
