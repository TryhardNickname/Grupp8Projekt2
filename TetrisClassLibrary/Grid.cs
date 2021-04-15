﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
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
            for (int i = 21; i <= 0; i--)
            {
                for (int j = 0; j < 12; j++)
                {
                    string row = "";
                    row += GridArea[i][j];
                    if (row == "@@@@@@@@@@@@")
                    {
                        //Clear row
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
                //check if you can rotate 
            }


            //Loop through grid to see collission?
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
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

        internal void UpdateGrid()
        {
            
            //throw new NotImplementedException();
        }

        internal void RemoveFullRows()
        {
            //removes rows //and moves tiles over down
            //throw new NotImplementedException();
        }



        public void UpdateTetromino(string keyInput)
        {

            if (keyInput == "left")
            {
                CurrentTetromino.Move("left");
            }
            else if (keyInput == "right")
            {

                CurrentTetromino.Move("right");
            }
            else if (keyInput == "rotate")
            {
                CurrentTetromino.Rotate();
            }
        }

        public void AddNewRandomTetromino()
        {
            Random rng = new Random();
            int num = rng.Next(1, 7);
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