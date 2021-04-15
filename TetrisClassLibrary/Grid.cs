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

            for (int i = 0; i < GridArea.Count; i++)
            {
                if (i < GridArea[0].Count)
                {
                    GridArea[0][i] = '░';  // Top
                    testdraw();

                    GridArea[GridArea.Count - 1][i] = '░';  // Bottom
                    testdraw();
                }
                GridArea[i][0] = '░';  // Left
                testdraw();

                GridArea[i][GridArea[0].Count - 1] = '░';  // Right
                testdraw();
            }
            
        }
        public void testdraw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i2 = 0; i2 < 22; i2++)
            {
                for (int j2 = 0; j2 < 12; j2++)
                {
                    Console.Write(GridArea[i2][j2]);
                }
                Console.WriteLine();

            }
        }
        public void UpdateGrid(string keyInput)
        {

            //kolla om tetromino landar
            //sätt till '@'

            //kolla kollision

            //kolla full rad?
            for (int i = 22; i > 0; i--)
            {
                for (int j = 0; j < 12; j++)
                {
                    string row = "";
                    row += GridArea[i][j];
                    if (row == "@@@@@@@@@@@@")
                    {
                        //Clear row
                    }
                }
            }

            //kolla om game over?

            //kolla vart nuvarande tetromino är
            //UpdateTetromino();

        }

        public bool GravityTick()
        {
            if (CurrentTetromino.GetPos().Y > 19)
            {
                Point pos = CurrentTetromino.GetPos();

                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (CurrentTetromino.Shape[row][col] == ' ')
                        {
                            
                        }
                        else
                        {
                            GridArea[pos.Y + row][pos.X + col] = '@';
                        }
                    }
                }
                return false;
            }
            else
            {
                CurrentTetromino.GravityTick();
                return true;

            }

        }
        public void UpdateTetromino(string keyInput)
        {
            if (keyInput == null)
            {
                return;
            }
            else if (keyInput == "left")
            {

                if (CurrentTetromino.GetPos().X < 2)
                {
                    //wall
                }
                else
                {
                    CurrentTetromino.Move("left");
                }
            }
            else if (keyInput == "right")
            {
                if (CurrentTetromino.GetPos().X > 9)
                {
                    //wall
                }
                else
                {
                    CurrentTetromino.Move("right");
                }
            }
            else if (keyInput == "rotate")
            {
                CurrentTetromino.Move("rotate");
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