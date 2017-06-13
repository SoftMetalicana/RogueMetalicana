namespace RogueMetalicana.Constants.Level
{
    using RogueMetalicana.Constants.Player;
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// Provides all variables for LevelGenerator.cs
    /// </summary>
    public static class LevelConstants
    {
        /// <summary>
        /// Finds the folder two levels up from the project default folder "bin/debug".
        /// </summary>
        public const string LoadingPath = @"..\..";

        /// <summary>
        /// Full path of the file in pieces.
        /// </summary>
        public const string FolderName = @"\Levels";
        public const string LevelLoadName = @"\Level-";
        public const int StartLevelNumber = 1;
        public const string FileExtension = ".txt";

        /// <summary>
        /// Symbols used in a single level
        /// </summary>
        public const char Ground = ' ';
        public const char Wall = '#';
        public const char Door = '|';

        public const char RiverOfMercury = '^';
/*        public const string WallMeaning = "wall";
        public const string WallThreat = "No known theat";*/

        public const char Lava = '~';
/*        public const string LavaMeaning = "lava";
        public const string LavaThreat = "Swimming into hot lava will eventually lead to death";*/

        public const char SpellboundForest = '%';
/*        public const string ForestMeaning = "SpellboundForest";
        public const string ForestThreat = "Noone has ever escaped the spellbound forest";*/

/*        public const char Ghost = 'G';
        public const string GhostMeaning = "Ghost";
        public const string GhostThreat = "Low";*/

        /// <summary>
        /// These symbols are used to split the input for enemies and objects when level is generated
        /// </summary>
        public static readonly char[] LegendSplitSymbols = {'\t', '|'};


        /// <summary>
        /// Each single input for an enemy in the map legend should contain : type of input, symbol, type of enemy and enemy difficulty
        /// </summary>
        public const int EnemyInputArrayLength = 4;

        /// <summary>
        /// Each single input for an obstacle in the map legend should contain : type of input, symbol, type of obstacle and description
        /// </summary>
        public const int ObstacleInputArrayLength = 4;

        /// <summary>
        /// Each single input for a place in the map legend should contain : type of input, symbol, type of place, type of gain, value
        /// </summary>
        public const int PlaceInputArrayLength = 5;

        public const string EnemyInput = "/I/E";
        public const string ObstacleInput = "/I/O";
        public const string PlaceInput = "/I/P";


        /// <summary>
        /// Holds the color for the wanted symbol.
        /// </summary>
        public static readonly Dictionary<char, ConsoleColor> SymbolsColors = new Dictionary<char, ConsoleColor>
        {
            [PlayerConstants.Symbol] = ConsoleColor.Cyan,
            [Wall] = ConsoleColor.White,
            [Ground] = ConsoleColor.White,
            [Lava] = ConsoleColor.Red,
            [SpellboundForest] = ConsoleColor.Green,
            [RiverOfMercury] = ConsoleColor.DarkBlue
        };
    }
}
