using System;
using System.Collections.Generic;
using System.Drawing;
using TetrisClassLibrary.Tetrominos;

namespace TetrisClassLibrary
{
    public class Grid
    {
        public List<List<char>> GridArea;
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

            for (int i = 0; i < GridArea[0].Count; i++)
            {
                GridArea[0][i] = '░';  // Top

                GridArea[GridArea[0].Count - 1][i] = '░';  // Bottom

                GridArea[i][0] = '░';  // Left

                GridArea[i][GridArea[0].Count - 1] = '░';  // Right

            }
        }

        public void UpdateTetromino()
        {
            Point pos = CurrentTetromino.GetPos();

            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (CurrentTetromino.Shape[row][col] == ' ')
                    {
                        //do nothing
                    }
                    else
                    {
                        //GridArea[row][col] = '@';
                        GridArea[pos.Y + row][pos.X + col] = '@';
                    }
                }
            }
        }
        public void AddNewRandomTetromino()
        {
            Random rng = new Random();
            int num = rng.Next(1, 7);
            int test = 1;
            num = test;
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