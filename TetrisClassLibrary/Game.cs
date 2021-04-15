﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace TetrisClassLibrary
{
    public class Game
    {
        public Grid Grid { get; set; }
        public Score MyScore { get; set; }

        public Game()
        {
            Start();
        }
        public void Start()
        {
            Grid = new Grid();
            MyScore = new Score();
            Console.CursorVisible = false;

        }

        /// <summary>
        /// Main Game Loop
        /// takes input, updates fields AND prints to console
        /// NYI - connect with EVENTS to GUI, to seperate resposibilites
        /// </summary>
        public void Loop()
        {
            bool playing = true;
            Grid.AddNewRandomTetromino();

            int gravity = 20; //20 game tics
            int tickCounter = 0;

            string input = "";

            while (playing)
            {
                //GAME TIMING================
                Thread.Sleep(50); //game tick // System.Timers better?
                tickCounter++;


                //HANDLE USER INPUT========== 
                input = HandleUserInput();


                //GAME LOGIC =?============= checking if collision -> if not -> perform action
                switch (input)
                {
                    case "left":
                        if(Grid.CanTetroFit(-1, 0))
                        {
                            Grid.UpdateTetromino("left");
                        }
                        break;
                    case "right":
                        if (Grid.CanTetroFit(1, 0))
                        {
                            Grid.UpdateTetromino("right");
                        }
                        break;
                    case "rotate":
                        if (Grid.CanTetroFit(0, 0))
                        {
                            Grid.UpdateTetromino("rotate");
                        }
                        break;
                    default:
                        break;
                }

                if (tickCounter == gravity)
                {
                    if (Grid.CanTetroFit(0, 1))
                    {
                        // it worked
                        Grid.CurrentTetromino.GravityTick();
                    }
                    else
                    {
                        // check if game lose
                        //AddCurrentTetromino to stack()??
                        Grid.AddNewRandomTetromino();
                    }
                    tickCounter = 0;
                }



                //CHECK FOR FULL ROWS ==============
                int rowsCleared = Grid.CheckForFullRow();
                if ( rowsCleared > 0)
                {
                    Grid.RemoveFullRows();
                    //Grid.UpdateGrid();
                    MyScore.UpdateScore();
                    if (MyScore.LevelUp())
                    {
                        gravity--;
                    }
                }

                //DRAW GAME==================
                DrawGameField();
                DrawTetromino();
                DrawScore(); //maybe only update when score updates for performance
                DrawLevel(); // ¨^^^¨
            }

        }

        private void DrawLevel()
        {
            //throw new NotImplementedException();
        }

        private void DrawScore()
        {
            //throw new NotImplementedException();
        }

        private void DrawGameField()
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(Grid.GridArea[i][j]);
                }
                Console.WriteLine();

            }
        }

        private void DrawTetromino()
        {
            int X = Grid.CurrentTetromino.GetX();
            int Y = Grid.CurrentTetromino.GetY();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Grid.CurrentTetromino.Shape[row][col] == ' ')
                    {
                        //GridArea[pos.Y + row][pos.X + col] = ' ';
                    }
                    else
                    {
                        //GridArea[row][col] = '@';
                        Console.ForegroundColor = Grid.CurrentTetromino.Color;
                        Console.SetCursorPosition(X+col, Y+row);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        /// <summary>
        /// Moves or rotates based on keyboard input
        /// </summary>
        public string HandleUserInput()
        {
            
            ConsoleKey key;
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey().Key;

                return key switch
                {
                    ConsoleKey.A => "left",
                    ConsoleKey.D => "right",
                    ConsoleKey.Q => "rotate",
                    _ => "null",
                };
            }
            else
            {
                return "null";
            }
        }

    }
}
