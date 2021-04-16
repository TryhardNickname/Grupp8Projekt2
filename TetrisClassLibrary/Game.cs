using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Linq;

namespace TetrisClassLibrary
{
    public class Game
    {
        public Grid Grid { get; set; }
        public Score MyScore { get; set; }
        List<int> totalScore = new List<int>();

        ConsoleKeyInfo key;

        int gravity = 20; //20 game tics
        int tickCounter = 0;
        int maxGravity = 5;
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
                Thread.Sleep(10); //game tick // System.Timers better?
                tickCounter++;


                //HANDLE USER INPUT========== 
                HandleUserInput();
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
                        Grid.AddNewRandomTetromino();
                    }
                    tickCounter = 0;
                }




                //CHECK FOR FULL ROWS ==============
                int rowsCleared = Grid.CheckForFullRow();
                

                if (rowsCleared > 0)
                {
                    //Console.SetCursorPosition(15, 6);
                    //Console.WriteLine("testest");
                    //Grid.RemoveFullRows();
                    //Grid.UpdateGrid();
                    DrawScore(MyScore.UpdateScore(rowsCleared));
                    if (MyScore.LevelUp())
                    {
                        gravity--;
                    }
                }

                //DRAW GAME==================
                DrawGameField();
                DrawTetromino();
                //DrawScore(); //maybe only update when score updates for performance
                DrawLevel(); // ¨^^^¨
                
            }

        }


        private void DrawLevel()
        {
            //throw new NotImplementedException();
        }

        private void DrawScore(int score)
        {
            
            totalScore.Add(score);
            Console.SetCursorPosition(15, 5);
            Console.WriteLine("Score: {0}", totalScore.Sum());
            //Console.WriteLine(score);
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
            for (int row = 0; row < Grid.CurrentTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < Grid.CurrentTetromino.Shape[0].Count; col++)
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
        public void HandleUserInput()
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    Grid.UpdateTetromino("left");
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    Grid.UpdateTetromino("right");
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    Grid.UpdateTetromino("rotate");
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    tickCounter = gravity;
                    break;
                case ConsoleKey.Spacebar:
                    //hard drop
                    break;
                default:
                    break;
            }

        }

    }
}
