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
        public Grid Grid { get; set; }
        public Score MyScore { get; set; }
        public ConsoleKeyInfo inputKey { get; set; }
        public int Gravity { get; set; }

        public int TickCounter { get; set; }
        public int GameXOffset { get; set; }
        public int GameYOffset { get; set; }


        public Game()
        {
            TickCounter = 0;
            GameXOffset = 10;
            GameYOffset = 4;

            Grid = new Grid(GameXOffset, GameYOffset);
            MyScore = new Score();
            Gravity = 20; //20 game tics (20*50ms == 1sec)

            Console.CursorVisible = false;
            Console.SetWindowSize(45, 35);
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

            //Sets up the game by using the input by player for gravity, adds a tetromino that will become the current one
            //and adds another tetromino that will be shown as the next one.
            Gravity = MyScore.SetGravity();
            Grid.AddNewRandomTetrominoUpcoming();
            Grid.CurrentTetromino = Grid.UpcomingTetromino;
            Grid.AddNewRandomTetrominoUpcoming();

            while (playing)
            {
                //GAME TIMING================
                Thread.Sleep(50); //game tick 
                TickCounter++; //for each loop, add 1 to TickCounter.


                //HANDLE USER INPUT (MOVEMENT) ========== 
                if (!HandleUserInput())
                {
                    //Console.Beep();
                }
                inputKey = new ConsoleKeyInfo();

                //GAME LOGIC ===============  
                //Gravity dictates how often the current Tetromino will fall down.
                if (TickCounter == Gravity) 
                {
                    if (Grid.CanTetroFit(0, 1))
                    {
                        //CanTetroFit is a collision check, if it returns true the current tetromino falls down one step.
                        Grid.UpdateTetromino("gravity");
                    }
                    else //If CanTetroFit returns false it means it cannot fall down anymore.
                    {
                        //This method turns the current tetromino into at signs in the gamefield at the position where it couldnt fall anymore.
                        Grid.AddCurrentTetrominoToStack();

                        //CHECK FOR FULL ROWS ==============
                        Grid.CheckForFullRow(rowsToClear); //out List<int> rowsToClear);

                        //Remove if full rows
                        if (rowsToClear.Count > 0)
                        {
                            CoolClearLinesEffect(rowsToClear);
                            Grid.RemoveFullRows(rowsToClear);
                            MyScore.UpdateScore(rowsToClear.Count);
                            rowsToClear.Clear();
                        }

                        //Add next tetro
                        ClearUpcomingTetromino();
                        Grid.CurrentTetromino = Grid.UpcomingTetromino;
                        Grid.AddNewRandomTetrominoUpcoming();

                        // Check if game lose 
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

            Console.SetCursorPosition(GameXOffset + infoOffset, 9);
            Console.WriteLine("Level: {0}", MyScore.CurrentLevel);

            Console.WriteLine(Gravity); // test output

            Console.SetCursorPosition(GameXOffset + infoOffset, 7);
            Console.WriteLine("Score: {0}", MyScore.TotalScore);
        }

        //Goes through "GridArea" which is a list of list of chars. List<List<char>>. 
        //This function writes out the content of each list after eachother vertically so that it looks like its a gamefield.
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

                for (int j = 0; j < Grid.GridWidth + 2; j++)
                {
                    Console.Write(Grid.GridArea[i][j]);
                }
                Console.WriteLine();

            }

            Console.SetCursorPosition(18, 0);
            Console.Write("Next Tetromino");
        }

        //This method imitates how the classical tetris removes full lines. 
        //It starts in the middle of each line that is full and goes out to the edge of that line, both ways, char by char
        //And replaces those chars with empty spaces so that it looks like the row is removed from the inside out.
        internal void CoolClearLinesEffect(List<int> rowsToClear)
        {
            int forwards = (Grid.GridWidth / 2) + 1;
            int backwards = Grid.GridWidth / 2;
            int gap = 0;

            while (forwards <= Grid.GridWidth)
            {

                for (int i = 0; i < rowsToClear.Count; i++)
                {
                    Console.SetCursorPosition(GameXOffset + forwards, rowsToClear[i] + gap);
                    Console.Write(' ');
                    Console.SetCursorPosition(GameXOffset + backwards, rowsToClear[i] + gap);
                    Console.Write(' ');

                }

                forwards++;
                backwards--;

                Thread.Sleep(100);
            }
        }

        //This is where the current tetromino is drawn. The X and Y values are the topleft corner of the invisible square that 
        //contains the current tetromino. It then uses those values to write At symbols in the correct coordinates in GridArea.
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
                            Console.SetCursorPosition(X + col + GameXOffset, Y + row);
                            Console.Write('@');
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
        }

        //Draws the tetromino thats coming next. Same technique as DrawTetromino(), but to the right of GridArea. 
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
                        Console.SetCursorPosition(X + col + GameXOffset + 13, Y + row);
                        Console.Write('@');
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }

        //This method is used to erase the old upcoming tetromino when it is moved to GridArea. 
        //Without this, the new upcoming tetromino will just be placed over the old one and it looks ugly.
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
                        Console.SetCursorPosition(X + col + GameXOffset + 13, Y + row);
                        Console.Write(' ');
                    }
                }
            }
        }

        //Handles inputs.
        private void Input()
        {
            do
            {
                inputKey = Console.ReadKey(true);
            } while (true);
        }

        /// <summary>
        /// Moves or rotates based on keyboard input
        /// </summary>
        public bool HandleUserInput()
        {
            switch (inputKey.Key)
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
