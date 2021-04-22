using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class JShape : Tetromino
    {
        public JShape(int middleOfGrid, int topOfGrid) : base(middleOfGrid, topOfGrid)
        {
            Color = ConsoleColor.Green;

            //0 0 0
            //1 1 1 
            //0 0 1
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
                    0, 0, 1
                }

            };
        }

        private JShape(JShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new JShape(this);
        }
    }
}
