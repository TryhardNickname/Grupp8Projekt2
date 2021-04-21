using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TetrisClassLibrary
{
    public abstract class Tetromino
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public List<List<int>> Shape { get; protected set; }
        public ConsoleColor Color { get; protected set; }       

        public Tetromino(int middleOfGrid, int topOfGrid)
        {
            X = middleOfGrid;
            Y = topOfGrid;
        }

        protected Tetromino(Tetromino copy) //: this()
        {
            this.X = copy.X;
            this.Y = copy.Y;
            this.Shape = copy.Shape;
            this.Color = copy.Color;

        }

        public abstract Tetromino Clone();

        //When the topleft coordinate is updated, the methods that use those coordinates start from a different point compared to before.
        public void Move(string direction)
        {
            if (direction == "left")
            {
                X = X - 1;
            }
            else if (direction == "right")
            {
                X = X + 1;
            }

        }

        //Rotates "2d array"
        public virtual void Rotate()
        {

            //new empty 2darray
            int width;
            int height;

            width = Shape[0].Count;
            height = Shape.Count;

            List<List<int>> newShape = new List<List<int>>();
            for (int i = 0; i < Shape.Count; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < Shape[0].Count; j++)
                {
                    temp.Add(0);
                }
                newShape.Add(temp);
            }

            //clockwise
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = col;
                    newCol = height - (row + 1);

                    newShape[newRow][newCol] = Shape[row][col];

                    //NYI counter clockwise
                    //newShape[(height -1) - col][row] = Shape[row][col];
                }
            }
            
            Shape = newShape;

        }

        //When the current tetromino falls.
        public void GravityTick()
        {
            Y = Y + 1;
        }
    }
}
