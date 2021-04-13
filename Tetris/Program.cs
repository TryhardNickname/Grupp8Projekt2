using System;
using TetrisClassLibrary;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Game g = new Game();
            Console.WriteLine("Hello World!");
            //console menu

            //if play
            g.Start();
            while(true)
            {
                //tick
                //print spelplan
                //print tetromino J 
                //print points
                //clear
            }

            Console.ReadKey();
        }
    }
}
