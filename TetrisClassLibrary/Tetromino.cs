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
        //Point(X,Y);
        //protected Point Position { get; set; }
        public int X {protected get; set; }
        public int Y {protected get; set; }
        public List<List<char>> Shape { get; set; }
        public ConsoleColor Color { get; set; }       

        public Tetromino()
        {
            //Position = new System.Drawing.Point(5, 1);
            X = 5;
            Y = 1;
            //1 = SShape
            //2 = ZShape
            //3 = LShape
            //4 = JShape
            //5 = TShape
            //6 = IShape
            //7 = OShape
            //Enum??
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
                //Position = new Point(Position.X-1, Position.Y);
                X = X - 1;
            }
            else if (direction == "right")
            {
                //Position = new Point(Position.X + 1, Position.Y);
                X = X + 1;
            }

        }

        public void Rotate()
        {

            char[,] matrix = new char[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Shape[i][j] == '@')
                    {
                        matrix[i, j] = '@';
                    }
                    else
                    {
                        matrix[i, j] = ' ';
                    }
                }
            }

            char[,] newmatrix = new char[4, 4];
            //rotate 2d array
            for (int i = 3; i >= 0; --i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    newmatrix[j, 3 - i] = matrix[i, j];
                }
            }

            List<List<char>> newShape = new();
            for (int i = 0; i < 4; i++)
            {
                newShape.Add(new List<char>());
            }
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    newShape[row].Add(newmatrix[row, col]);
                }
            }

            Shape = newShape;

            //addoffsetdata?

        }

        public void GravityTick()
        {
            //Position = new Point(Position.X, Position.Y + 1);
            Y = Y + 1;
            
        }
    }
}
