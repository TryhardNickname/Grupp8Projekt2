﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary.Tetrominos
{
    class TShape : Tetromino
    {
        public TShape()
        {
            Color = ConsoleColor.White;

            //1 1 1 0
            //0 1 0 0 
            //0 0 0 0
            Shape = new List<List<char>>
            {
                new List<char>
                {
                    '@', '@', '@', ' '
                },
                new List<char>
                {
                    ' ', '@', ' ', ' '
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

        protected TShape(TShape copy) : base(copy)
        {
            //?
        }

        public override Tetromino Clone()
        {
            return new TShape(this);
        }
    }
}
