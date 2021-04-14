using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TetrisClassLibrary
{
    public abstract class Tetromino
    {
        //Point(X,Y);
        protected Point Position { get; set; }
        public List<List<char>> Shape { get; set; }
        protected ConsoleColor Color { get; set; }
        

        public Tetromino()
        {
            Position = new System.Drawing.Point(5, 1);
            //1 = SShape
            //2 = ZShape
            //3 = LShape
            //4 = JShape
            //5 = TShape
            //6 = IShape
            //7 = OShape
            //Enum??
        }

        public Point GetPos()
        {
            return Position;
        }
        public void Move(string direction)
        {
            if (direction == "left")
            {
                Position = new Point(Position.X-1, Position.Y);
            }
            else
            {
                Position = new Point(Position.X + 1, Position.Y);
            }
            
        }

        public void Rotate()
        {

        }
    }
}
