using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class SShape : Tetromino
    {
        public SShape()
        {
            //Position = new int[0, 5];
            Color = ConsoleColor.Magenta;
            //0 0 1 1
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
                    ' ', '@', '@'
                },
                new List<char>
                {
                    '@', '@', ' '
                }

            };
        }

        protected SShape(SShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new SShape(this);
        }
    }
}
