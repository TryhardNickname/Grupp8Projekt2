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
            Color = ConsoleColor.Cyan;

            Shape = new List<List<char>>
            {
                new List<char>
                {
                    ' ', ' ', '@', '@'
                },
                new List<char>
                {
                    ' ', '@', '@', ' '
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

    }
}
