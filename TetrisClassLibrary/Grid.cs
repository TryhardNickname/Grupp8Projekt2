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
        public Grid()
        {
            GridArea = new List<List<char>>();
            BuildMap();
            BuildBarrier();
        }
        private void BuildMap()
        {
            for (int i = 0; i < 22; i++)
            {
                GridArea.Add(new List<char>() { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' });
            }
        }
        private void BuildBarrier()
        {

            for (int i = 0; i < GridArea.Count; i++)
            {
                if (i < GridArea[0].Count)
                {
                    GridArea[0][i] = '░';  // Top

                    GridArea[GridArea.Count - 1][i] = '░';  // Bottom
                }
                GridArea[i][0] = '░';  // Left

                GridArea[i][GridArea[0].Count - 1] = '░';  // Right
            }
            
        }

        public int CheckForFullRow()
        {

            //kolla full rad?
            for (int i = 21; i > 0; i--)
            {
                string row = "";
                for (int j = 1; j < 11; j++)
                {

                    row += GridArea[i][j];
                    if (row == "@@@@@@@@@@")
                    {
                        //Clear 
                        RemoveFullRows(i);

                        return 1;
                    }
                }
            }

            return 0;
        }

        public bool CanTetroFit(int X, int Y)
        {
            //Add current tetromino position
            var ClonedTetromino = CurrentTetromino.Clone();
            if (X == 1)
            {
                ClonedTetromino.Move("right");
            }
            else if (X == -1)
            {
                ClonedTetromino.Move("left");
            }
            else if ( Y == 1)
            {
                ClonedTetromino.GravityTick();
            }
            else if (X == 0 && Y == 0)
            {
                ClonedTetromino.Rotate();
            }


            //Loop through grid to see collission?
            for (int row = 0; row < ClonedTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < ClonedTetromino.Shape[0].Count; col++)
                {
                    if (ClonedTetromino.Shape[row][col] == '@')
                    {
                        //if collission return false
                        if (GridArea[ClonedTetromino.GetY()+row][ClonedTetromino.GetX()+col] == '@' || GridArea[ClonedTetromino.GetY() + row][ClonedTetromino.GetX() + col] == '░')
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
            for (int i = currentRow; i > 1; i--)
            {
                GridArea[i] = GridArea[i - 1];
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
    }
}