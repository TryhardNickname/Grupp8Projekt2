using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TetrisClassLibrary.Tetrominos;

namespace TetrisClassLibrary
{
    public class Grid
    {
        public List<List<int>> GridArea { get; private set; }
        public Tetromino CurrentTetromino { get; private set; }
        public Tetromino UpcomingTetromino { get; private set; }
        public int GridWidth { get; private set; }
        public int GridHeight { get; private set; }
        private int HiddenRows { get; set; }

        public Grid(int gameXOffset, int gameYOffset)
        {
            GridArea = new List<List<int>>();
            HiddenRows = gameYOffset; // dont know if they should be connected
            GridWidth = 10;
            GridHeight = 20 + HiddenRows;

            BuildMap();
            BuildBarrier();
        }
        private void BuildMap()
        {
            for (int i = 0; i < GridHeight + 1; i++) // + bottom border
            {
                GridArea.Add(new List<int>() { });
                for (int j = 0; j < GridWidth + 2; j++) // + 2 side borders
                {
                    GridArea[i].Add(0);
                }
            }
        }

        private void BuildBarrier()
        {
            for (int i = 0; i < GridArea.Count; i++)
            {
                if (i < GridArea[0].Count)
                {
                    //GridArea[0][i] = '░';  // Top part which is replaced by '-' in Game.DrawGameField.

                    GridArea[GridArea.Count - 1][i] = 2;  // Bottom
                }
                GridArea[i][0] = 2;  // Left

                GridArea[i][GridArea[0].Count - 1] = 2;  // Right
            }

        }
      
        //Checks if the current tetromino collides with anything.
        //It uses a virtual clone of the current tetromino in the position that the user wants to move to.
        //If it works out the current tetromino is used to that position, otherwise it doesnt move.
        public bool CanTetroFit(int X, int Y)
        {
            //Clone current tetromino position
            var Clone = CurrentTetromino.Clone();
            if (X == 1)
            {
                Clone.Move("right");
            }
            else if (X == -1)
            {
                Clone.Move("left");
            }
            else if (Y == 1)
            {
                Clone.GravityTick();
            }
            else if (X == 0 && Y == 0)
            {
                Clone.Rotate();
            }
            else if (X == -2 && Y == -2)
            {
                //check spawn
            }

            //Loop through shape-grid to see collission
            for (int row = 0; row < Clone.Shape.Count; row++)
            {
                for (int col = 0; col < Clone.Shape[0].Count; col++)
                {
                    if (Clone.Shape[row][col] == 1)
                    {
                        //check if shape-grid has negative value to prevent out of bounds
                        if (Clone.X + col <= 0)
                        {
                            return false;
                        }


                        //if collission return false
                        if (GridArea[Clone.Y + row][Clone.X + col] == 1)
                        {
                            return false;
                        }
                        if (GridArea[Clone.Y + row][Clone.X + col] == 2) //check bounds someway instead
                        {
                            return false;
                        }

                    }
                }
            }
            //else true
            return true;

        }

        //if collision with bottom/tetromino-stack add it to the stack. 
        public void AddCurrentTetrominoToStack()
        {

            for (int row = 0; row < CurrentTetromino.Shape.Count; row++)
            {
                for (int col = 0; col < CurrentTetromino.Shape[0].Count; col++)
                {
                    if (CurrentTetromino.Shape[row][col] == 1)
                    {
                        GridArea[CurrentTetromino.Y + row][CurrentTetromino.X + col] = 1;

                    }
                    else
                    {

                    }
                }
            }
        }
      
        //This method checks if a row is completely filled. It then fills rowsToClear with ints.
        //These ints are used in RemoveFullRows to know which rows to clear.
        public void CheckForFullRow(List<int> rowsToClear) //out List<int> rowsToClear)
        {
            for (int i = GridHeight; i >= 0; i--) //i >= 0 + HiddenRows?
            {
                int row = 0;
                for (int j = 1; j <= GridWidth; j++)
                {
                    row += GridArea[i][j];
                }
                if (row == 10)
                {
                    rowsToClear.Add(i);
                }
            }
        }

        //Uses a list of Y-cooordinates (rows) to know from where to go. 
        //It takes an int and from there it loops downward in the gamefield.
        //So row 16, becomes 17. Row 15 becomes row 16 and so on. 
        //This is repeated for every line that is empty in rowsToRemove.
        internal void RemoveFullRows(List<int> rowsToRemove)
        {
            for (int row = rowsToRemove.Count-1; row >= 0 ; row--)
            {
                int currentRow = rowsToRemove[row];
                for (int i = currentRow; i > 0; i--)
                {
                    GridArea[i] = new List<int>(GridArea[i - 1]);
                    if (i == 1)
                    {
                        GridArea[i] = new List<int> { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 };
                    }
                }
            }

        }

        //This method moves the current tetromino if the virtual tetromino in CanTetroFit returns true.
        public bool UpdateTetromino(string keyInput)
        {
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

        public void SetCurrentTetromino()
        {
            CurrentTetromino = UpcomingTetromino;
            AddNewRandomTetrominoUpcoming();
        }

        //Spawns a random tetromino which is placed to the right of the gamefield.
        public void AddNewRandomTetrominoUpcoming()
        {
            Random rng = new();
            int num = rng.Next(1, 8);
            switch (num)
            {
                case 1:
                    UpcomingTetromino = new ZShape(GridWidth / 2, 0);
                    break;
                case 2:
                    UpcomingTetromino = new SShape(GridWidth / 2, 0);
                    break;
                case 3:
                    UpcomingTetromino = new LShape(GridWidth / 2, 0);
                    break;
                case 4:
                    UpcomingTetromino = new JShape(GridWidth / 2, 0);
                    break;
                case 5:
                    UpcomingTetromino = new IShape(GridWidth / 2, 0);
                    break;
                case 6:
                    UpcomingTetromino = new TShape(GridWidth / 2, 0);
                    break;
                case 7:
                    UpcomingTetromino = new OShape(GridWidth / 2, 0);
                    break;
                default:
                    break;
            }

        }

    }
}