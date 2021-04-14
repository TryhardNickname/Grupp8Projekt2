using System;
using System.Collections.Generic;
using System.Threading;

namespace TetrisClassLibrary
{
    public class Game
    {
        public Grid grid { get; set; }

        public Game()
        {
            Start();
        }
        public void Start()
        {
            grid = new Grid();
        }

        /// <summary>
        /// Main Game Loop
        /// takes input, updates fields AND prints to console
        /// NYI - connect with EVENTS to GUI, to seperate resposibilites
        /// </summary>
        public void Loop()
        {
            bool playing = true;
            grid.AddNewRandomTetromino();

            while (playing)
            {
                grid.UpdateTetromino();
                Console.SetCursorPosition(0, 0);

                DrawGameField();
                Thread.Sleep(1000);
            }
            //loop (playing){
            //  printField 
            //  printCurrenttetrino
            //  takeinput

            //    if (input left
            //       checkCollision
            //       currenttetrino.move()

            //    if (input right
            //        osv


            //  check gravityCounter
            //  update field
            //}
        }


        private void DrawGameField()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(grid.GridArea[i][j]);
                }
                Console.WriteLine();

            }
        }

        private void DrawTetromino()
        {

        }

    }
}
