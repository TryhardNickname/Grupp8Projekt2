using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TetrisClassLibrary
{
    public class Score
    {
        public int TotalScore { get; private set;}
        public int CurrentLevel { get; private set; }
        public int RowsCleardThisLevel { get; private set; }

        //NYI TotalRowsCleared
        public Score()
        {
            TotalScore = 0;
            CurrentLevel = 0;
            RowsCleardThisLevel = 0;
            if (!File.Exists("Highscore.txt"))
            {
                File.Create("Highscore.txt").Close();
            }
        }

        //Takes rowscleared input and returns score based on rowscleared

        public void UpdateScore(int rowsCleared)
        {
            RowsCleardThisLevel += rowsCleared;
            switch (rowsCleared)
            {
                case 1:
                    TotalScore += (40 * (CurrentLevel + 1));
                    break;
                case 2:
                    TotalScore += (100 * (CurrentLevel + 1));
                    break;
                case 3:
                    TotalScore += (300 * (CurrentLevel + 1));
                    break;
                case 4:
                    TotalScore += (1200 * (CurrentLevel + 1));
                    break;
                default:
                    break;
            }
        }

        //Checks how many rows have been cleared on the current level and levels you up
        public bool LevelUp()
        {
            if (RowsCleardThisLevel >= (10 * CurrentLevel) + 10)
            {
                CurrentLevel++;
                RowsCleardThisLevel = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Lets you start at whichever level you want, (move to constructor
        public void SetLevel(int input)
        {
            CurrentLevel = input;
        }

        public void SaveHighScore()
        {
            File.WriteAllText("Highscore.txt", Convert.ToString(TotalScore));
        }
        public static int LoadHighScore()
        {
            if (!File.Exists("Highscore.txt"))
            {
                File.WriteAllText("Highscore.txt", Convert.ToString(0));
            }
            return Convert.ToInt32(File.ReadAllText("Highscore.txt"));
        }

        public int SetGravity()
        {
            if (CurrentLevel > 9)
            {
                return 2;
            }
            else
            {
                return 20 - CurrentLevel * 2;
            }
        }
    }
}
