using System;
using System.Collections.Generic;
using TetrisClassLibrary.Tetrominos;

namespace TetrisClassLibrary
{
    public class Grid
    {
        public List<List<bool>> test;
        public Tetromino CurrentTetromino { get; set; }

        void AddNewRandomTetromino()
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