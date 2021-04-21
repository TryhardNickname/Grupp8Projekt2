using System;
using Xunit;
using TetrisClassLibrary;
using System.Collections.Generic;

namespace TetrisClassLibrary.Test
{
    public class ScoreTest
    {
        [Theory]
        [InlineData(4, 1200)]
        [InlineData(1, 40)]
        [InlineData(3, 300)]
        public void UpdateScore_RowsClearedShouldUpdateScore(int rowsCleared, int expected)
        {
            Game game = new();
            
            game.MyScore.UpdateScore(rowsCleared);
            int actual = game.MyScore.TotalScore;

            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(9, 4, 12000)]
        [InlineData(4, 1, 200)]
        [InlineData(7, 3, 2400)]
        public void UpdateScore_RowsClearedShouldUpdateScoreAtDifferentLevel(int level, int rowsCleared, int expected)
        {
            Game game = new();
            game.MyScore.SetLevel(level);
            game.MyScore.UpdateScore(rowsCleared);
            int actual = game.MyScore.TotalScore;

            Assert.Equal(expected, actual);
        }
    }

}
