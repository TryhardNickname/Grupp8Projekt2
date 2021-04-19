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
        public static List<int> totalScore = new List<int>();
        public static int currentLevel = 0;
        int rowsCleardThisLevel = 0;
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
        public static void SetLevel(int input)
        {
            currentLevel = input;
        }
        public static void SaveHighScore()
        {
            File.WriteAllText("Highscore.txt", Convert.ToString(totalScore.Sum()));
        }
        public static int LoadHighScore()
        {
            return Convert.ToInt32(File.ReadAllText("Highscore.txt"));
        }
    }
}
