using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class LShape : Tetromino
    {
        public LShape()
        {
            Color = ConsoleColor.Yellow;

            //0 0 0
            //1 1 1 
            //1 0 0 
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    ' ', ' ', ' '
                },
                new List<char>
                {
                    '@', '@', '@'
                },
                new List<char>
                {
                    '@', ' ', ' '
                }

            };
        }

        protected LShape(LShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new LShape(this);
        }
    }
}
