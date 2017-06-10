namespace RogueMetalicana.Visualization
{
    using RogueMetalicana.Constants.Console;
    using RogueMetalicana.PlayerUnit;
    using RogueMetalicana.Positioning;
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
        public static void PrintDungeon(IEnumerable<char[]> dungeon, Player player)
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
        /// Prints the player current stats.
        /// </summary>
        /// <param name="player">The players start that you want to print</param>
        public static void PrintPlayerStats(Player player)
        {
            string[] messages = new string[3]
            {
                $"Current health: {player.Health--}",
                $"Current armor: {player.Defense}",
                $"Current damage: {player.Damage}"
            };

            StringBuilder result = new StringBuilder();
            for (int currentRow = ConsoleConstants.PlayerStatsPrintStartRow, messageIndex = 0; currentRow <= 4; currentRow++, messageIndex++)
            {
                Console.SetCursorPosition(ConsoleConstants.PlayerStatsPrintStartCol, currentRow);

                if (currentRow != 4)
                {
                    Console.WriteLine(messages[messageIndex]);
                }
            }
        }

        /// <summary>
        /// This method prints a symbol on a given position.
        /// </summary>
        /// <param name="newSymbol">The symbol that you want to print.</param>
        /// <param name="toPrintOn">The cell that you want to print it on.</param>
        public static void DeleteSymbolOnPositionAndPrintNewOne(char newSymbol, Position toPrintOn)
        {
            Console.SetCursorPosition(toPrintOn.Col, toPrintOn.Row);
            Console.Write(newSymbol);
        }

        /// <summary>
        /// Prints the wanted message and then terminates/ends the program.
        /// </summary>
        /// <param name="messageToEndTheGameWith">The message that you want to print.</param>
        public static void PrintEndGameMessage(string messageToEndTheGameWith)
        {
            Console.Clear();
            Console.WriteLine(messageToEndTheGameWith);

            Environment.Exit(0);
        }
    }
}
