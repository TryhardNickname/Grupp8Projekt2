using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class JShape : Tetromino
    {
        public JShape()
        {
            Color = ConsoleColor.Green;

            //1 1 1 0
            //0 0 1 0 
            //0 0 0 0
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    '@', '@', '@'
                },
                new List<char>
                {
                    ' ', ' ', '@'
                },
                new List<char>
                {
                    ' ', ' ', ' '
                }
            };
        }

        protected JShape(JShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new JShape(this);
        }
    }
}
