using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClassLibrary
{
    class Score
    {
        private int Points { get; set; }
        private int Level { get; set; }

        private int GetGravity(int points)
        {
            switch (points)
            {
                case < 200:
                    return 19;
                case < 300:
                    return 18;
                default:
                    return 20;
            }
        }
    }
}
