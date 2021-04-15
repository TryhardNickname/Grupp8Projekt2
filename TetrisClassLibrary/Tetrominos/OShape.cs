using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class OShape : Tetromino
    {
        public OShape()
        {
            Color = ConsoleColor.Blue;

            //1 1 0 0
            //1 1 0 0 
            //0 0 0 0
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    '@', '@', ' ', ' '
                },
                new List<char>
                {
                    '@', '@', ' ', ' '
                },
                new List<char>
                {
                    ' ', ' ', ' ', ' '
                },
                new List<char>
                {
                    ' ', ' ', ' ', ' '
                }
            };
        }
        protected OShape(OShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new OShape(this);
        }
    }
}
