using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TetrisClassLibrary
{
    public abstract class Tetromino
    {
        //[y , x] == [row, column]
        protected int[,] Position { get; set; }
        protected List<List<char>> Shape { get; set; }
        protected ConsoleColor Color { get; set; }


        public Tetromino()
        {
            //1 = SShape
            //2 = ZShape
            //3 = LShape
            //4 = JShape
            //5 = TShape
            //6 = IShape
            //7 = OShape
            //Enum??
        }

        public void Move(string direction)
        {
            
        }

        public void Rotate()
        {

        }
    }
}
