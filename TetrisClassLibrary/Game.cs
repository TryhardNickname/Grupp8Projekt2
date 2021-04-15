using System;
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

            string input = "";


            while (playing)
            {
                //GAME TIMING================
                var timeElapsed = Stopwatch.GetTimestamp();
                Thread.Sleep(50); //game tick
                tickCounter++;



                //HANDLE USER INPUT========== check collision etc game logic
                input = HandleUserInput();

                //GAME LOGIC =?=============
                switch (input)
                {
                    case "left":
                        if(grid.CanTetroFit(-1, 0))
                        {
                            grid.UpdateTetromino("left");
                        }
                        break;
                    case "right":
                        if (grid.CanTetroFit(1, 0))
                        {
                            grid.UpdateTetromino("right");
                        }
                        break;
                    case "rotate":
                        if (grid.CanTetroFit(0, 0))
                        {
                            grid.UpdateTetromino("rotate");
                        }
                        break;
                    default:
                        break;
                }

                if (tickCounter == gravity)
                {
                    if (grid.CanTetroFit(0, 1))
                    {
                        // it worked
                        grid.CurrentTetromino.GravityTick();
                    }
                    else
                    {
                        // check if game lose
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
            int X = grid.CurrentTetromino.GetX();
            int Y = grid.CurrentTetromino.GetY();
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
                    //grid.UpdateTetromino("left");
                    //break;
                    ConsoleKey.D => "right",
                    //grid.UpdateTetromino("right");
                    //break;
                    ConsoleKey.Q => "rotate",
                    //grid.UpdateTetromino("rotate");
                    //break;
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
