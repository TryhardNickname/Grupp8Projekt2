using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class TShape : Tetromino
    {
        public TShape(int middleOfGrid, int topOfGrid) : base(middleOfGrid, topOfGrid)
        {
            Color = ConsoleColor.White;

            //0 0 0 
            //1 1 1 
            //0 1 0 
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
                    0, 1, 0
                }
            };
        }

        private TShape(TShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new TShape(this);
        }
    }
}
