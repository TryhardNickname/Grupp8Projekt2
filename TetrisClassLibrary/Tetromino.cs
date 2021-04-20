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
        public int X {protected get; set; }
        public int Y {protected get; set; }
        public List<List<char>> Shape { get; set; }
        public ConsoleColor Color { get; set; }       

        public Tetromino()
        {
            X = 5;
            Y = 0;
        }
        protected Tetromino(Tetromino copy) : this()
        {
            this.X = copy.X;
            this.Y = copy.Y;
            this.Shape = copy.Shape;
            this.Color = copy.Color;

        }

        public abstract Tetromino Clone();

        public int GetX()
        {
            return X;
        }
        public int GetY()
        {
            return Y;
        }
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

        public void Rotate()
        {

            //clockwise
            int width;
            int height;

            width = Shape[0].Count;
            height = Shape.Count;

            List<List<char>> newShape = new List<List<char>>();
            for (int i = 0; i < Shape.Count; i++)
            {
                List<char> temp = new List<char>();
                for (int j = 0; j < Shape[0].Count; j++)
                {
                    temp.Add(' ');
                }
                newShape.Add(temp);
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int newRow;
                    int newCol;

                    newRow = col;
                    newCol = height - (row + 1);

                    newShape[newRow][newCol] = Shape[row][col];
                }
            }
            Shape = newShape;

        }

        public void GravityTick()
        {
            Y = Y + 1;
            
        }
    }
}
