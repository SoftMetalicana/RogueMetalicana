namespace RogueMetalicana.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Takes care to print on the console.
    /// </summary>
    public static class Visualisator
    {
        /// <summary>
        /// Refreshes the console (Console.Clear();) and than prints the latest condition of the dungeon.
        /// The dungeon is printed line by line.
        /// </summary>
        /// <param name="dungeon">The dungeon that you want to print.</param>
        public static void PrintDungeon(IEnumerable<char[]> dungeon)
        {
            Console.Clear();

            StringBuilder result = new StringBuilder();
            foreach (char[] line in dungeon)
            {
                result.AppendLine(string.Join(string.Empty, line));
            }

            Console.Write(result);
        }

        /// <summary>
        /// Prints the wanted message and then terminates/ends the program.
        /// </summary>
        /// <param name="messageToEndTheGameWith">The message that you want to print.</param>
        public static void PrintEndGameMessage(string messageToEndTheGameWith)
        {
            Console.WriteLine(messageToEndTheGameWith);

            Environment.Exit(0);
        }
    }
}
