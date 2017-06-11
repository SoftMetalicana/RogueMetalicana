namespace RogueMetalicana.Constants.Level
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public static class LegendConstants
    {
        private const char PaddingChar = ' ';
        private const int PaddingCount = 15;

        public const string EnemiesLabel = "ENEMIES:";
        public static readonly string SubEnemiesLabels = CreatePaddedRow("Symbol", "Meaning", "EnemyLevel");

        public const string ObstaclesLabel = "OBSTACLES:";
        public static readonly string SubObstaclesLabels = CreatePaddedRow("Symbol", "Meaning", "Threat");

        public static readonly Dictionary<char, Tuple<string, string>> ObstaclesInfo = new Dictionary<char, Tuple<string, string>>
        {
            [LevelConstants.Wall] = Tuple.Create(LevelConstants.WallMeaning, LevelConstants.WallThreat),
            [LevelConstants.Lava] = Tuple.Create(LevelConstants.LavaMeaning, LevelConstants.LavaThreat),
            [LevelConstants.SpellboundForest] = Tuple.Create(LevelConstants.ForestMeaning, LevelConstants.ForestThreat),
        };

        public static string CreatePaddedRow(string firstString, string secondString, string thirdString)
        {
            return $"|{PadRight(firstString)}|{PadRight(secondString)}|{thirdString}";
        }

        private static string PadRight(string toPad)
        {
            return toPad.PadRight(PaddingCount, PaddingChar);
        }
    }
}
