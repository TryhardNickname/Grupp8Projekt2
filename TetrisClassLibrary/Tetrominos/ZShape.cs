using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class ZShape : Tetromino
    {
        public ZShape(int middleOfGrid, int topOfGrid) : base(middleOfGrid, topOfGrid)
        {
            //Position = new System.Drawing.Point(5, 0);
            Color = ConsoleColor.Cyan;

            //0 0 0
            //1 1 0 
            //0 1 1
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    ' ', ' ', ' '
                },
                new List<char>
                {
                    '@', '@', ' '
                },
                new List<char>
                {
                    ' ', '@', '@'
                }

            };

        }

        public new void Rotate()
        {

            //new empty 2darray
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

            //offset to right
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    newShape[row][col] = ' ';
                }
            }

            Shape = newShape;

        }
    

        protected ZShape(ZShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new ZShape(this);
        }
    }
}



