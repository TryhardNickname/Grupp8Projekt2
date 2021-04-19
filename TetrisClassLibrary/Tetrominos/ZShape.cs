using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class ZShape : Tetromino
    {
        public ZShape()
        {
            //Position = new System.Drawing.Point(5, 0);
            Color = ConsoleColor.Cyan;

            //1 1 0 0
            //0 1 1 0 
            //0 0 0 0
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



