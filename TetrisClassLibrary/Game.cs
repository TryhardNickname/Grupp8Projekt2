using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Media;
using System.IO;

namespace TetrisClassLibrary
{
    public class Game
    {
        private Grid Grid { get; set; }
        public Score MyScore { get; private set; }
        private ConsoleKeyInfo InputKey { get; set; }
        private int Gravity { get; set; }

        private int TickCounter { get; set; }
        private int GameXOffset { get; set; }
        private int GameYOffset { get; set; }


        public Game()
        {
            TickCounter = 0;
            GameXOffset = 10;
            GameYOffset = 3;

            Grid = new Grid(GameXOffset, GameYOffset);
            MyScore = new Score();
            Gravity = 20; //20 game tics (20*50ms == 1sec)

            Console.CursorVisible = false;
            //Console.SetWindowSize(45, 35);
            Thread inputThread = new Thread(Input);
            inputThread.Start();
        }


        /// <summary>
        /// Main Game Loop
        /// takes input, updates fields AND prints to console
        /// </summary>
        public void Loop()
        {
            bool playing = true;
            List<int> rowsToClear = new();

            Gravity = MyScore.SetGravity();
            Grid.AddNewRandomTetrominoUpcoming();
            Grid.SetCurrentTetromino();

            while (playing)
            {
                //GAME TIMING================
                Thread.Sleep(50); //game tick 
                TickCounter++;


                //HANDLE USER INPUT (MOVEMENT) ========== 
                if (!HandleUserInput())
                {
                    //Console.Beep();
                }
                InputKey = new ConsoleKeyInfo();

                //GAME LOGIC ===============  
                if (TickCounter == Gravity)
                {
                    if (Grid.CanTetroFit(0, 1))
                    {
                        // it worked, move tetris down
                        Grid.UpdateTetromino("gravity");
                    }
                    else//tetromino landed
                    {
                        Grid.AddCurrentTetrominoToStack();

                        //CHECK FOR FULL ROWS ==============
                        Grid.CheckForFullRow(rowsToClear); //out List<int> rowsToClear);

                        //remove if full rows
                        if (rowsToClear.Count > 0)
                        {
                            CoolClearLinesEffect(rowsToClear);
                            Grid.RemoveFullRows(rowsToClear);
                            MyScore.UpdateScore(rowsToClear.Count);
                            rowsToClear.Clear();
                        }

                        //add next tetro
                        ClearUpcomingTetromino();
                        Grid.SetCurrentTetromino();

                        // check if game lose 
                        if (!(Grid.CanTetroFit(-2, -2)))
                        {
                            playing = false;
                            Console.WriteLine("GAME OVER");
                            Console.SetCursorPosition(20, 5);
                            Console.WriteLine("PRESS ANY KEY TO EXIT");
                            Console.ReadKey();
                        }
                    }

                    //Checks currentLevel in the Score class and calls the LevelUp function
                    //if LevelUp is true Gravity goes down and the game gets faster
                    if (MyScore.LevelUp())
                    {
                        Gravity = Gravity - 2;
                        if (Gravity < 2)
                        {
                            Gravity = 2;
                        }
                    }

                    TickCounter = 0;
                }

                //DRAW GAME==================
                DrawUpcomingTetromino();
                DrawGameField();
                DrawTetromino();
                DrawLevelAndScore();

            }
        }


        //Checks the totalScore and currentlevel in the Score class and prints it out
        private void DrawLevelAndScore()
        {
            int infoOffset = 15;


            Console.SetCursorPosition(GameXOffset + infoOffset, GameYOffset-GameYOffset);
            Console.Write("Next Tetromino");

            Console.SetCursorPosition(GameXOffset + infoOffset, GameYOffset);
            Console.WriteLine("Score: {0}", MyScore.TotalScore);

            Console.SetCursorPosition(GameXOffset + infoOffset, GameYOffset*2);
            Console.WriteLine("Level: {0}", MyScore.CurrentLevel);
        }

        private void DrawGameField()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(GameXOffset, GameYOffset - 1);
            Console.WriteLine("░----------░");

            for (int i = 0; i < GameYOffset - 1; i++) 
            {
                Console.CursorLeft = GameXOffset;
                Console.CursorTop = i;
                Console.WriteLine("           "); //hiding hidden rows, but not ----
            }


            for (int i = GameYOffset; i <= Grid.GridHeight; i++) //20 rows
            {
                Console.CursorLeft = GameXOffset;
                Console.CursorTop = i;

                for (int j = 0; j < Grid.GridWidth + 2; j++) //+2 -> borders
                {
                    Console.Write(Grid.GridArea[i][j]);
                }
                Console.WriteLine();

            }
        }

        internal void CoolClearLinesEffect(List<int> rowsToClear)
        {
            int forwards = (Grid.GridWidth / 2);
            int backwards = Grid.GridWidth / 2;

            while (forwards <= Grid.GridWidth)
            {

                for (int i = 0; i < rowsToClear.Count; i++)
                {
                    Console.SetCursorPosition(GameXOffset + forwards, rowsToClear[i]);
                    Console.Write(' ');
                    Console.SetCursorPosition(GameXOffset + backwards, rowsToClear[i]);
                    Console.Write(' ');
                }

                forwards++;
                backwards--;

                Thread.Sleep(100);
            }
        }

        private void DrawTetromino()
        {
            int X = Grid.CurrentTetromino.X;
            int Y = Grid.CurrentTetromino.Y;
            for (int row = 0; row < Grid.CurrentTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < Grid.CurrentTetromino.Shape[0].Count; col++)
                {
                    if (Grid.CurrentTetromino.Shape[row][col] == ' ')
                    {

                    }
                    else
                    {
                        if (Y > 0) //try without
                        {
                            Console.ForegroundColor = Grid.CurrentTetromino.Color;
                            Console.SetCursorPosition(X + col + GameXOffset, Y + row);
                            Console.Write('@');
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
        }

        //Draws the tetromino thats coming next
        private void DrawUpcomingTetromino()
        {
            int upcomingOffsetX = 13;

            int X = Grid.UpcomingTetromino.X;
            int Y = Grid.UpcomingTetromino.Y;

            for (int row = 0; row < Grid.UpcomingTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < Grid.UpcomingTetromino.Shape[0].Count; col++)
                {
                    if (Grid.UpcomingTetromino.Shape[row][col] == ' ')
                    {

                    }
                    else
                    {
                        Console.ForegroundColor = Grid.UpcomingTetromino.Color;
                        Console.SetCursorPosition(X + col + GameXOffset + upcomingOffsetX, Y + row);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        private void ClearUpcomingTetromino()
        {
            int upcomingOffsetX = 13;

            int X = Grid.UpcomingTetromino.X;
            int Y = Grid.UpcomingTetromino.Y;
            for (int row = 0; row < Grid.UpcomingTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < Grid.UpcomingTetromino.Shape[0].Count; col++)
                {
                    if (Grid.UpcomingTetromino.Shape[row][col] != ' ')
                    {
                        Console.SetCursorPosition(X + col + GameXOffset + upcomingOffsetX, Y + row);
                        Console.Write(' ');
                    }
                }
            }
        }

        private void Input()
        {
            do
            {
                InputKey = Console.ReadKey(true);
            } while (true);
        }

        /// <summary>
        /// Moves or rotates based on keyboard input
        /// </summary>
        public bool HandleUserInput()
        {
            switch (InputKey.Key)
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
                    TickCounter = Gravity;
                    return true;
                //break;

                default:
                    return true;
            }

        }

    }
}
