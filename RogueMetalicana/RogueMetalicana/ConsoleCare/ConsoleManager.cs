namespace RogueMetalicana.ConsoleCare
{
    using System;
    using RogueMetalicana.Constants.Console;
    
    /// <summary>
    /// This class takes care of the console but does not take care for any validadions or prints.
    /// </summary>
    public static class ConsoleManager
    {
        /// <summary>
        /// Sets the console size.
        /// </summary>
        public static void SetConsoleSize()
        {
            Console.WindowHeight = ConsoleConstants.ConsoleHeight;
            Console.WindowWidth = ConsoleConstants.ConsoleWidth;

            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.BufferWidth;
        }
    }
}
