
namespace RogueMetalicana.Constants.Console
{
    using System;

    /// <summary>
    /// Provides all variables for the console.
    /// </summary>
    public static class ConsoleConstants
    {
        /// <summary>
        /// Variables for the initial console size.
        /// </summary>
        public const int ConsoleHeight = 40;
        public const int ConsoleWidth = 120;

        /// <summary>
        /// Variables for the initial playfield size.
        /// </summary>
        public const int FieldHeight = 15;
        public const int FieldWidth = 70;

        // default console colors
        public const ConsoleColor DefaultConsoleBackgroundColor = ConsoleColor.Black;
        public const ConsoleColor DefaultConsoleForegroundColor = ConsoleColor.White;

        // color of the selected option in the menu
        public const ConsoleColor SelectedConsoleBackgroundColor = ConsoleColor.White;
        public const ConsoleColor SelectedConsoleForegroundColor = ConsoleColor.Black;

        public const int PlayerStatsPrintStartRow = 1;
        public const int PlayerStatsPrintStartCol = 72;
    }
}
