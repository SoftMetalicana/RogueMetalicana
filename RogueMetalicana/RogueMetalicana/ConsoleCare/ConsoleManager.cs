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
        public static void SetTheConsoleForTheGame()
        {
            Console.CursorVisible = false;

            Console.WindowHeight = ConsoleConstants.ConsoleHeight;
            Console.WindowWidth = ConsoleConstants.ConsoleWidth;

        }

        // changes the text color of the select option at the menu
        public static void SelectedOptionColor()
        {
            Console.ForegroundColor = ConsoleConstants.SelectedConsoleForegroundColor;
            Console.BackgroundColor = ConsoleConstants.SelectedConsoleBackgroundColor;
        }

        //returns the default colors of the console 
        public static void DefaultColors()
        {
            Console.ForegroundColor = ConsoleConstants.DefaultConsoleForegroundColor;
            Console.BackgroundColor = ConsoleConstants.DefaultConsoleBackgroundColor;
        }

    }
}
