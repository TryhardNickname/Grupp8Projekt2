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
            //first 
            grid.AddNewRandomTetromino();


            while (playing)
            {
                //DRAW GAME==================
                Console.SetCursorPosition(0, 0);
                DrawGameField();

                //HANDLE USER INPUT========== check collision etc game logic
                //Console.KeyAvailable;


                //UPDATE GRID ==============
                //check gravity, pass into update?
                grid.UpdateTetromino();
            }

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
