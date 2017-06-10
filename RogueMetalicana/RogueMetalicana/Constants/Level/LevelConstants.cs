namespace RogueMetalicana.Constants.Level
{
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
        public const char Wall = '#';
        public const char Ground = ' ';
        public const char Lava = '~';
        public const char SpellboundForest = '%';

        /// <summary>
        /// Holds the color for the wanted symbol.
        /// </summary>
        public static readonly Dictionary<char, ConsoleColor> SymbolsColors = new Dictionary<char, ConsoleColor>
        {
            [Wall] = ConsoleColor.White,
            [Ground] = ConsoleColor.White,
            [Lava] = ConsoleColor.Red,
            [SpellboundForest] = ConsoleColor.Green,
        };
    }
}
