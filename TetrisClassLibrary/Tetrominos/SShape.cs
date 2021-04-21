using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class SShape : Tetromino
    {
        public SShape(int middleOfGrid, int topOfGrid) : base(middleOfGrid, topOfGrid)
        {
            Color = ConsoleColor.Magenta;
            //0 0 0
            //0 1 1 
            //1 1 0 
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    ' ', ' ', ' '
                },
                new List<char>
                {
                    ' ', '@', '@'
                },
                new List<char>
                {
                    '@', '@', ' '
                }

            };
        }

        //Overrides to implement Offset
        public override void Rotate()
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


            //OFFSET TO RIGHT
            //1 0 0
            //1 1 0 
            //0 1 0
            if (newShape[0][0] == '@')
            {
                //if top,left is filled, offset to right
                for (int row = height - 1; row >= 0; row--)
                {
                    for (int col = width - 1; col >= 0; col--)
                    {
                        if (col == 0)
                        {
                            newShape[row][col] = ' ';
                        }
                        else
                        {
                            newShape[row][col] = newShape[row][col - 1];
                        }
                    }
                }
            }
            //0 1 0
            //0 1 1 
            //0 0 1 

            Shape = newShape;

        }

        private SShape(SShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new SShape(this);
        }
    }
}
