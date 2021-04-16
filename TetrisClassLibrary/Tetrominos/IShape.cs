using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class IShape : Tetromino
    {
        public IShape()
        {
            Color = ConsoleColor.Red;

            //1 1 1 1
            //0 0 0 0 
            //0 0 0 0
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    ' ', '@', ' ', ' '
                },
                new List<char>
                {
                    ' ', '@', ' ', ' '
                },
                new List<char>
                {
                    ' ', '@', ' ', ' '
                },
                new List<char>
                {
                    ' ', '@', ' ', ' '
                }
            };
        }
        protected IShape(IShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new IShape(this);
        }
    }
}
