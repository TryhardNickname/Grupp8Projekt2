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

            //0 1 0
            //1 1 0 
            //1 0 0
            Shape = new List<List<int>>
            {
                new List<int>
                {
                    0, 0, 0
                },
                new List<int>
                {
                    1, 1, 0
                },
                new List<int>
                {
                    0, 1, 1
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


            //OFFSET TO RIGHT
            //0 1 0
            //1 1 0 
            //1 0 0
            if (newShape[2][0] == 1)
            {
                //if bot,left is filled, offset to right
                for (int row = height-1; row >= 0; row--)
                {
                    for (int col = width-1; col >= 0; col--)
                    {
                        if ( col == 0 )
                        {
                            newShape[row][col] = 0;
                        }
                        else
                        {
                            newShape[row][col] = newShape[row][col - 1];
                        }
                    }
                }
            }
            //0 0 1
            //0 1 1 
            //0 1 0 

            Shape = newShape;

        }
    
        private ZShape(ZShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new ZShape(this);
        }
    }
}



