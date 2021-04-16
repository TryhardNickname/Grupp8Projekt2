using System;
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

        ConsoleKeyInfo key;

        int gravity = 5; //20 game tics
        int tickCounter = 0;
        int maxGravity = 5;
        int gameXOffset = 5;

        public Game()
        {
            Start();
        }
        public void Start()
        {
            Grid = new Grid();
            MyScore = new Score();
            Console.CursorVisible = false;

            Thread inputThread = new Thread(Input);
            inputThread.Start();

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

            while (playing)
            {
                //GAME TIMING================
                Thread.Sleep(50); //game tick // System.Timers better?
                tickCounter++;


                //HANDLE USER INPUT========== 
                if (!HandleUserInput())
                {
                    //Console.SetCursorPosition(15, 0);
                    Console.Beep();
                }
                key = new ConsoleKeyInfo();

                //GAME LOGIC =?==============  
                if (tickCounter == gravity)
                {
                    if (Grid.CanTetroFit(0, 1))
                    {
                        // it worked 
                        Grid.UpdateTetromino("gravity");
                    }
                    else
                    {
                        // check if game lose

                        Grid.AddCurrentTetrominoToStack();

                        Grid.CheckForFullRow();

                        Grid.AddNewRandomTetromino();
                    }
                    tickCounter = 0;
                }



                //CHECK FOR FULL ROWS ==============
                int rowsCleared = Grid.CheckForFullRow();
                if ( rowsCleared > 0)
                {
                    //Grid.RemoveFullRows();
                    //Grid.UpdateGrid();
                    MyScore.UpdateScore(rowsCleared);
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
            Console.ForegroundColor = ConsoleColor.White;
          
            for (int i = 0; i < 22; i++)
            {
                Console.CursorLeft = gameXOffset;
                Console.CursorTop = i;
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
            for (int row = 0; row < Grid.CurrentTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < Grid.CurrentTetromino.Shape[0].Count; col++)
                {
                    if (Grid.CurrentTetromino.Shape[row][col] == ' ')
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = Grid.CurrentTetromino.Color;
                        Console.SetCursorPosition(X+col + gameXOffset, Y+row);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }


        private void Input()
        {
            do
            {
                key = Console.ReadKey(true);
            } while (true);//Console.KeyAvailable);
        }

        /// <summary>
        /// Moves or rotates based on keyboard input
        /// </summary>
        public bool HandleUserInput()
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    return Grid.UpdateTetromino("left");
                    //break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    return Grid.UpdateTetromino("right");
                    //break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    return Grid.UpdateTetromino("rotate");
                    //break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    tickCounter = gravity;
                    return true;
                    //break;
                case ConsoleKey.Spacebar:
                    //hard drop
                    return true;
                    //break;
                default:
                    return true;
            }

        }

    }
}
