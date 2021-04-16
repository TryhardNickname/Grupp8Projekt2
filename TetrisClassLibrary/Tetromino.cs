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

            //clockwise
            int width;
            int height;

            width = Shape[0].Count;
            height = Shape.Count;

            //ändra till 3x3??
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
            //{
            //    new List<char>
            //    {
            //        ' ', ' ', ' ', ' '
            //    },
            //    new List<char>
            //    {
            //        ' ', ' ', ' ', ' '
            //    },
            //    new List<char>
            //    {
            //        ' ', ' ', ' ', ' '
            //    },
            //    new List<char>
            //    {
            //        ' ', ' ', ' ', ' '
            //    }
            //};

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

            //addoffsetdata?

        }

        public void GravityTick()
        {
            //Position = new Point(Position.X, Position.Y + 1);
            Y = Y + 1;
            
        }
    }
}
