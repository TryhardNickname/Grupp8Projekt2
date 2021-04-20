using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary
{
    public class Score
    {
        public static List<int> totalScore = new List<int>();
        public static int currentLevel = 0;
        int rowsCleardThisLevel = 0;
        //Takes rowscleared input and returns score based on rowscleared
        internal int UpdateScore(int rowsCleared)
        {
            rowsCleardThisLevel += rowsCleared;
            switch (rowsCleared){
                case 1:
                    return 40 * (currentLevel + 1);
                case 2:
                    return 100 * (currentLevel + 1);
                case 3:
                    return 300 * (currentLevel + 1);
                case 4:
                    return 1200 * (currentLevel + 1);
            }
            return 0;
        }

        //Checks how many rows have been cleared on the current level and levels you up
        internal bool LevelUp()
        {
            if (rowsCleardThisLevel >= (10 * currentLevel) + 10)
            {
                currentLevel++;
                rowsCleardThisLevel = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Lets you start at whichever level you want
        public static void SetLevel(int input)
        {
            currentLevel = input;
        }
        public static int LevelChoice()
        {
            if (currentLevel > 9)
            {
                return 2;
            }
            else
            {
                return 20 - currentLevel * 2;
            }
        }
    }
}
