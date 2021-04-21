using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class LShape : Tetromino
    {
        public LShape(int middleOfGrid, int topOfGrid) : base(middleOfGrid, topOfGrid)
        {
            Color = ConsoleColor.Yellow;

            //0 0 0
            //1 1 1 
            //1 0 0 
            Shape = new List<List<int>>
            {
                new List<int>
                {
                    0, 0, 0
                },
                new List<int>
                {
                    1, 1, 1
                },
                new List<int>
                {
                    1, 0, 0
                }

            };
        }

        private LShape(LShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new LShape(this);
        }
    }
}
