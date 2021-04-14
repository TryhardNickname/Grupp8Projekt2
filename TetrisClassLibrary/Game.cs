using System;
using System.Collections.Generic;

namespace TetrisClassLibrary
{
    public class Game
    {
        public List<List<string>> PlayingField { get; set; }
        public void Start()
        {
            PlayingField = new List<List<string>>();
        }

        /// <summary>
        /// Main Game Loop
        /// takes input, updates fields AND prints to console
        /// NYI - connect with EVENTS to GUI, to seperate resposibilites
        /// </summary>
        public void Loop()
        {

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
        static void BuildMap(List<List<string>> playingField)
        {
            for (int i = 0; i < 22; i++)
            {
                playingField.Add(new List<string>() { " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " });
            }
        }
        static void BuildBarrier(List<List<string>> playingField)
        {

            for (int i = 0; i < playingField[0].Count; i++)
            {
                playingField[0][i] = "░";  // Top

                playingField[playingField[0].Count - 1][i] = "░";  // Bottom

                playingField[i][0] = "░";  // Left

                playingField[i][playingField[0].Count - 1] = "░";  // Right

            }
        }

        static void DrawGameField(List<List<string>> playingField)
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    Console.Write(playingField[i][j]);
                }
                Console.WriteLine();

            }
        }

    }
}
