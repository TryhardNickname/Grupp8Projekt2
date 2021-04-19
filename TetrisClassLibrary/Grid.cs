using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TetrisClassLibrary.Tetrominos;

namespace TetrisClassLibrary
{
    public class Grid
    {
        public List<List<char>> GridArea { get; set; }
        public Tetromino CurrentTetromino { get; set; }
        public Tetromino UpcomingTetromino { get; set; }
        public int GridWidth { get; set; }
        public int GridHeight { get; set; }
        public Grid(int gameXOffset, int gameYOffset)
        {
            GridArea = new List<List<char>>();
            GridWidth = 10;
            GridHeight = 20;
            BuildMap();
            BuildBarrier();
        }
        private void BuildMap()
        {
            for (int i = 0; i < GridHeight + 1; i++)
            {
                GridArea.Add(new List<char>() {});
                for (int j = 0; j < GridWidth + 2; j++)
                {
                    GridArea[i].Add(' ');
                }
            }
        }
        private void BuildBarrier()
        {

            for (int i = 0; i < GridArea.Count; i++)
            {
                if (i < GridArea[0].Count)
                {
                    //GridArea[0][i] = '░';  // Top

                    GridArea[GridArea.Count - 1][i] = '░';  // Bottom
                }
                GridArea[i][0] = '░';  // Left

                GridArea[i][GridArea[0].Count - 1] = '░';  // Right
            }
            
        }

        public int CheckForFullRow()
        {
            
            int removeCounter = 0;
            //kolla full rad?
            for (int i = GridHeight; i >= 0; i--)
            {
                string row = "";
                for (int j = 1; j < GridWidth; j++)
                {

                    row += GridArea[i][j];
                }
                if (row == "@@@@@@@@@@")
                {
                    //Clear row
                    removeCounter++;
                    RemoveFullRows(i);
                    ++i;
                }
            }

            return removeCounter;
        }

        public bool CanTetroFit(int X, int Y)
        {
            //Add current tetromino position
            var Clone = CurrentTetromino.Clone();
            if (X == 1)
            {
                Clone.Move("right");
            }
            else if (X == -1)
            {
                Clone.Move("left");
            }
            else if ( Y == 1)
            {
                Clone.GravityTick();
            }
            else if (X == 0 && Y == 0)
            {
                Clone.Rotate();
            }
            else if (X == -1 && Y == -1)
            {
                //check spawn

            }


            //Loop through grid to see collission?
            for (int row = 0; row < Clone.Shape.Count; row++)
            {
                for (int col = 0; col < Clone.Shape[0].Count; col++)
                {
                    if (Clone.Shape[row][col] == '@')
                    {
                        //if collission return false
                        if (GridArea[Clone.GetY()+row][Clone.GetX()+col] == '@')
                        {
                            return false;
                        }
                        if (GridArea[Clone.GetY() + row][Clone.GetX() + col] == '░')
                        {
                            return false;
                        }

                    }
                }
            }
            //else true
            return true;
            
        }

        internal void AddCurrentTetrominoToStack()
        {

            for (int row = 0; row < CurrentTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < CurrentTetromino.Shape[0].Count; col++)
                {
                    if (CurrentTetromino.Shape[row][col] == '@')
                    {
                        GridArea[CurrentTetromino.GetY() + row][CurrentTetromino.GetX() + col] = '@';

                    }
                    else
                    {

                    }
                }
            }
        }


        internal void RemoveFullRows(int currentRow)
        {
            for (int i = currentRow; i > 0; i--)
            {
                GridArea[i] = new List<char>(GridArea[i - 1]);
                if ( i == 1)
                {
                    GridArea[i] = new List<char> { '░', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '░' };
                }
            }
        }


        public bool UpdateTetromino(string keyInput)
        {
            //ha collioncheck här?
            if (keyInput == "left" && CanTetroFit(-1, 0))
            {
                CurrentTetromino.Move("left");
            }
            else if (keyInput == "right" && CanTetroFit(1, 0))
            {

                CurrentTetromino.Move("right");
            }
            else if (keyInput == "rotate" && CanTetroFit(0, 0))
            {
                CurrentTetromino.Rotate();
            }
            else if (keyInput == "gravity" && CanTetroFit(0, 1))
            {
                CurrentTetromino.GravityTick();
            }
            else
            {
                return false;
            }
            return true;
            
        }

        public void AddNewRandomTetromino()
        {
            Random rng = new();
            int num = rng.Next(1, 8);
            switch (num)
            {
                case 1:
                    CurrentTetromino = new ZShape();
                    break;
                case 2:
                    CurrentTetromino = new SShape();
                    break;
                case 3:
                    CurrentTetromino = new LShape();
                    break;
                case 4:
                    CurrentTetromino = new JShape();
                    break;
                case 5:
                    CurrentTetromino = new TShape();
                    break;
                case 6:
                    CurrentTetromino = new IShape();
                    break;
                case 7:
                    CurrentTetromino = new OShape();
                    break;
                default:
                    break;
            }
            
        }
        public void AddNewRandomTetrominoUpcoming()
        {
            Random rng = new();
            int num = rng.Next(1, 8);
            switch (num)
            {
                case 1:
                    UpcomingTetromino = new ZShape();
                    break;
                case 2:
                    UpcomingTetromino = new SShape();
                    break;
                case 3:
                    UpcomingTetromino = new LShape();
                    break;
                case 4:
                    UpcomingTetromino = new JShape();
                    break;
                case 5:
                    UpcomingTetromino = new TShape();
                    break;
                case 6:
                    UpcomingTetromino = new IShape();
                    break;
                case 7:
                    UpcomingTetromino = new OShape();
                    break;
                default:
                    break;
            }

        }

    }
}