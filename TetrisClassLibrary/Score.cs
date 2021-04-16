using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary
{
    public class Score
    {
        int currentLevel = 0;
        internal int UpdateScore(int rowsCleared)
        {

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
            //throw new NotImplementedException();
            if (currentLevel == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
