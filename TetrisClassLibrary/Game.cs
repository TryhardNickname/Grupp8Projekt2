using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Media;

namespace TetrisClassLibrary
{
    public class Game
    {
        public Grid Grid { get; set; }
        public Score MyScore { get; set; }

        ConsoleKeyInfo key;

        int gravity = 20; //20 game tics
        int tickCounter = 0;
        int gameXOffset = 5;
        int gameYOffset = 4;


        public Game()
        {
            Grid = new Grid(gameXOffset, gameYOffset);
            MyScore = new Score();
            Console.CursorVisible = false;
            Thread inputThread = new Thread(Input);
            inputThread.Start();
        }


        /// <summary>
        /// Main Game Loop
        /// takes input, updates fields AND prints to console
        /// </summary>
        public int Loop()
        {
            bool playing = true;
            List<int> rowsToClear = new();

            gravity = Score.LevelChoice();
            Grid.AddNewRandomTetrominoUpcoming();
            Grid.CurrentTetromino = Grid.UpcomingTetromino;
            Grid.AddNewRandomTetrominoUpcoming();

            while (playing)
            {
                //GAME TIMING================
                Thread.Sleep(50); //game tick 
                tickCounter++;


                //HANDLE USER INPUT (MOVEMENT) ========== 
                if (!HandleUserInput())
                {
                    //Console.Beep();
                }
                key = new ConsoleKeyInfo();

                //GAME LOGIC ===============  
                if (tickCounter == gravity)
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

                        if (rowsToClear.Count > 0)
                        {
                            CoolClearLinesEffect(rowsToClear);
                            DrawScore(MyScore.UpdateScore(rowsToClear.Count));

                            Grid.RemoveFullRows(rowsToClear);
                            DrawScore(MyScore.UpdateScore(rowsToClear.Count));
                            rowsToClear.Clear();
                        }

                            //add next tetro
                            ClearUpcomingTetromino();
                            Grid.CurrentTetromino = Grid.UpcomingTetromino;
                            Grid.AddNewRandomTetrominoUpcoming();

                            // check if game lose 
                            if (!(Grid.CanTetroFit(-2, -2)))
                            {
                                playing = false;
                                Console.WriteLine("GAME OVER");
                                Console.SetCursorPosition(20, 5);
                                Console.WriteLine("PRESS ANY KEY TO EXIT");
                                Console.ReadKey();
                            }
                        tickCounter = 0;
                    }



                    //DRAW GAME==================
                    DrawUpcomingTetromino();
                    DrawGameField();
                    DrawTetromino();
                    DrawLevel();
                }
            }
            return Score.totalScore.Sum();
        }

        //Checks currentLevel in the Score class and calls the LevelUp function
        //if LevelUp is true gravity goes down and the game gets faster
        private void DrawLevel()
        {
            Console.SetCursorPosition(20, 9);
            Console.WriteLine("Level: {0}", Score.currentLevel);
            Console.WriteLine(gravity);
            if (MyScore.LevelUp())
            {
                gravity = gravity - 2;
                if(gravity < 2)
                {
                    gravity = 2;
                }
            }
        }

        //Checks the totalScore List in the Score class and prints it out
        private void DrawScore(int score)
        {
            Score.totalScore.Add(score);
            Console.SetCursorPosition(20, 7);
            Console.WriteLine("Score: {0}", Score.totalScore.Sum());
        }

        private void DrawGameField()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(gameXOffset, gameYOffset-1);
            Console.WriteLine("░----------░");

            for (int i = 0; i < gameYOffset-1; i++) 
            {
                Console.CursorLeft = gameXOffset;
                Console.CursorTop = i;
                Console.WriteLine("           "); //hiding hidden rows, but not ----
            }


            for (int i = gameYOffset; i <= Grid.GridHeight; i++) //20 rows
            {
                Console.CursorLeft = gameXOffset;
                Console.CursorTop = i;

                for (int j = 0; j < Grid.GridWidth + 2; j++)
                {
                    Console.Write(Grid.GridArea[i][j]);
                }
                Console.WriteLine();

            }

            Console.SetCursorPosition(18, 0);
            Console.Write("Next Tetromino");
        }

        internal void CoolClearLinesEffect(List<int> rowsToClear)
        {
            int forwards = (Grid.GridWidth/2) + 1;
            int backwards = Grid.GridWidth / 2;
            int gap = 0;

            while (forwards <= Grid.GridWidth)
            {

                for (int i = 0; i < rowsToClear.Count; i++)
                {
                    //if (((i + 1) < rowsToClear.Count) && ((rowsToClear[i] - rowsToClear[i+1]) > 1)) // if space between rows is > 1 ( GAP)
                    //{

                    //    gap = rowsToClear[i] - rowsToClear[i + 1] - 1;
                    //}
                    Console.SetCursorPosition(gameXOffset + forwards, rowsToClear[i] + gap);
                    Console.Write(' ');
                    Console.SetCursorPosition(gameXOffset + backwards, rowsToClear[i] + gap);
                    Console.Write(' ');
                    //gap = 0;
                }

                //for (int i = firstRowClearedIndex; i > (firstRowClearedIndex - rowsToRemove); i--)
                //{
                //    Console.SetCursorPosition(gameXOffset + forwards, i);
                //    Console.Write(' ');
                //    Console.SetCursorPosition(gameXOffset + backwards, i);
                //    Console.Write(' ');
                //}
                forwards++;
                backwards--;

                Thread.Sleep(100);
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
                        if (Y > 0) //try without
                        {
                            Console.ForegroundColor = Grid.CurrentTetromino.Color;
                            Console.SetCursorPosition(X + col + gameXOffset, Y + row);
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
            int X = Grid.UpcomingTetromino.GetX();
            int Y = Grid.UpcomingTetromino.GetY();

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
                        Console.SetCursorPosition(X + col + gameXOffset + 13, Y + row);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        private void ClearUpcomingTetromino()
        {
            int X = Grid.UpcomingTetromino.GetX();
            int Y = Grid.UpcomingTetromino.GetY();
            for (int row = 0; row < Grid.UpcomingTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < Grid.UpcomingTetromino.Shape[0].Count; col++)
                {
                    if (Grid.UpcomingTetromino.Shape[row][col] != ' ')
                    {
                        Console.SetCursorPosition(X + col + gameXOffset + 13, Y + row);
                        Console.Write(' ');
                    }
                }
            }
        }

        //Removes the full rows with an animation
        internal int RemoveFullRows(List<int> rowsToRemove)
        {
            int forwards = 5;
            int backwards = 5;
            int rowsRemoved = 0;

            while (forwards < 11)
            {
                for (int i = 0; i < rowsToRemove.Count; i++)
                {
                    if (Grid.GridArea[rowsToRemove[i]][forwards] == '@')
                    {
                        Grid.GridArea[rowsToRemove[i]][forwards] = ' ';
                    }

                    if (Grid.GridArea[rowsToRemove[i]][backwards] == '@')
                    {
                        Grid.GridArea[rowsToRemove[i]][backwards] = ' ';
                    }
                }
                forwards++;
                backwards--;

                DrawGameField();
                Thread.Sleep(80);
            }

            for (int i = 0; i < rowsToRemove.Count; i++)
            {
                rowsRemoved++;

                while(!Grid.GridArea[rowsToRemove[i]].Contains('@'))
                {
                    for (int j = rowsToRemove[i]; j > 0; j--)
                    {
                        Grid.GridArea[j] = new List<char>(Grid.GridArea[j - 1]);
                        if (j == 1)
                        {
                            Grid.GridArea[j] = new List<char> { '░', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '░' };
                        }
                    }
                }
            }
            return rowsRemoved;
        }


        private void Input()
        {
            do
            {
                key = Console.ReadKey(true);
            } while (true);
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

                default:
                    return true;
            }

        }

    }
}
