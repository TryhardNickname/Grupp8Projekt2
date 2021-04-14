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
            Position = new int[0, 5];
            Color = ConsoleColor.Cyan;

            //1 1 0 0
            //0 1 1 0 
            //0 0 0 0
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    '@', '@', ' ', ' '
                },
                new List<char>
                {
                    ' ', '@', '@', ' '
                },
                new List<char>
                {
                    ' ', ' ', ' ', ' '
                }
            };

        }
    }
}



