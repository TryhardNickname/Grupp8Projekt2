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
        int gameYOffset = 0;


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
        /// NYI - connect with EVENTS to GUI, to seperate resposibilites
        /// </summary>
        public int Loop()
        {
            bool playing = true;
            int rowsCleared = 0;


            Grid.AddNewRandomTetrominoUpcoming();
            Grid.CurrentTetromino = Grid.UpcomingTetromino;
            Grid.AddNewRandomTetrominoUpcoming();

            while (playing)
            {
                //GAME TIMING================
                Thread.Sleep(50); //game tick // System.Timers better?
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

                        //rowsCleared = 
                        if (Grid.CheckForFullRow().Count > 0)
                        {
                            RemoveFullRows(Grid.CheckForFullRow());
                        }

                        //add next tetro
                        ClearUpcomingTetromino();
                        Grid.CurrentTetromino = Grid.UpcomingTetromino;
                        Grid.AddNewRandomTetrominoUpcoming();

                        // check if game lose i
                        if (!(Grid.CanTetroFit(-2, -2)))
                        {
                            playing = false;
                        }
                    }
                    tickCounter = 0;
                }
                if (rowsCleared > 0)
                {
                    DrawScore(MyScore.UpdateScore(rowsCleared));
                    rowsCleared = 0;
                }


                //DRAW GAME==================
                DrawUpcomingTetromino();
                DrawGameField();
                DrawTetromino();
                //DrawScore(); //maybe only update when score updates for performance
                DrawLevel(); // ¨^^^¨

            }
            return Score.totalScore.Sum();
        }


        //Checks currentLevel in the Score class and calls the LevelUp function
        //if LevelUp is true gravity goes down and the game gets faster
        private void DrawLevel()
        {
            Console.SetCursorPosition(20, 9);
            Console.WriteLine("Level: {0}", Score.currentLevel);
            if (MyScore.LevelUp())
            {
                gravity = gravity - 3;
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
            //Setting the gamefield further down and to the left
            Console.SetCursorPosition(gameXOffset, gameYOffset + Grid.HiddenRows);
            Console.WriteLine("░----------░");
            Console.SetCursorPosition(gameXOffset, gameYOffset);
            for (int k = 0; k < Grid.HiddenRows; k++)
            {
                Console.CursorLeft = gameXOffset;
                Console.CursorTop = k + gameYOffset;
                Console.WriteLine("            ");
            }
            for (int i = 0 + Grid.HiddenRows + 1; i < Grid.GridHeight + 1; i++)
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

        //internal void CoolClearLinesEffect(int rowsToRemove, int firstRowClearedIndex)
        //{
        //    int forwards = (Grid.GridWidth/2) + 1;
        //    int backwards = Grid.GridWidth / 2;
        //    int rowsIndexWithOffset = firstRowClearedIndex + gameYOffset;

        //    while (forwards <= Grid.GridWidth)
        //    {
        //        for (int i = rowsIndexWithOffset; i > (rowsIndexWithOffset-rowsToRemove); i--)
        //        {
        //            Console.SetCursorPosition(gameXOffset + forwards, i);
        //            Console.Write(' ');
        //            Console.SetCursorPosition(gameXOffset + backwards, i);
        //            Console.Write(' ');
        //        }
        //        forwards++;
        //        backwards--;

        //        Thread.Sleep(100);
        //    }
        //}

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
                        Console.SetCursorPosition(X + col + gameXOffset, Y + row + gameYOffset);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }
            }
        }

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

        internal void RemoveFullRows(List<int> rowsToRemove)
        {
            int forwards = 5;
            int backwards = 5;

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

                default:
                    return true;
            }

        }

    }
}
