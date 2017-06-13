using System.Runtime.InteropServices;
using RogueMetalicana.LevelEngine;
using RogueMetalicana.MapPlace;

namespace RogueMetalicana.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RogueMetalicana.Constants.Enemy;
    using RogueMetalicana.Constants.Visualisator;
    using RogueMetalicana.Constants.Console;
    using RogueMetalicana.Constants.Level;
    using RogueMetalicana.PlayerUnit;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.MapPlace;
    
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
                foreach (char symbol in line)
                {
                    PrintSymbolInColor(symbol, LevelConstants.SymbolsColors.ContainsKey(symbol) ?
                                                           LevelConstants.SymbolsColors[symbol] : 
                                                           ConsoleColor.White);
                }

                Console.WriteLine();
            }

/*            PrintTheMapLegend(dungeon);*/
        }

        public static void PrintBattleGround(List<char[]> dungeon, Player player, StringBuilder battleResult)
        {
            Console.Clear();
            Visualisator.PrintOnTheConsole(battleResult.ToString());
            Visualisator.PrintOnTheConsole("Press any key to continue:");
            Console.ReadKey(true);
            PrintAllMap(dungeon, player);
        }

        public static void PrintAllMap(List<char[]> dungeon, Player player)
        {
            Visualisator.PrintDungeon(dungeon, player);
            Visualisator.PrintOnTheConsole(LevelGenerator.CurrentMapLegend);
            Console.SetWindowPosition(0, 0);
        }
        private static void PrintTheMapLegend(IEnumerable<char[]> dungeon)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"");
            Console.SetCursorPosition(0, 16);
        }

        public static void PrintOnTheConsole(string textToPrint)
        {
            Console.WriteLine(textToPrint);
        }

        public static void PrintGainOnTheConsole(string textToPrint)
        {
            Console.SetCursorPosition(2, 16);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(2, 16);
            Console.WriteLine(textToPrint);
        }

        /// <summary>
        /// Print the legend of the map based on the enemies and obstacles dictionaries provided by LevelGenerator
        /// </summary>
        /// <param name="enemies"></param>
        public static string PrintMapLegend(Dictionary<char, KeyValuePair<string, int>> enemies, Dictionary<char, KeyValuePair<string, string>> obstacles, Dictionary<KeyValuePair<char, string>, KeyValuePair<Place.PlaceGain, int>> places)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine();
            result.AppendLine();
            result.AppendLine();
            result.Append(VisualisatorConstants.SeparatorLine);
            result.AppendLine(VisualisatorConstants.LegendEnemyHeading);

            foreach (var enemyData in enemies)
            {
                var difficulty = Enum.GetName(typeof(EnemyDifficulty), enemyData.Value.Value);

                result.AppendLine(string.Format("{0, -10}{1, -20}{2, -15}", enemyData.Key, enemyData.Value.Key, difficulty));
            }


            result.Append(VisualisatorConstants.SeparatorLine);

            result.AppendLine(VisualisatorConstants.LegendObstaclesHeading);

            foreach (var obstacleData in obstacles)
            {
                result.AppendLine(string.Format("{0, -10}{1, -20}{2, -50}", obstacleData.Key, obstacleData.Value.Key, obstacleData.Value.Value));
            }

            result.Append(VisualisatorConstants.SeparatorLine);

            result.AppendLine(VisualisatorConstants.LegendPlacesHeading);

            foreach (var placeData in places)
            {
                result.AppendLine(string.Format("{0, -10}{1, -20}", placeData.Key.Key, placeData.Key.Value));
            }


            return result.ToString();
        }

        /// <summary>
        /// Prints a symbol with a certain color on the console.
        /// </summary>
        /// <param name="symbolToPrint">The symbol that you want to print.</param>
        /// <param name="toUse">The color that you want to use.</param>
        private static void PrintSymbolInColor(char symbolToPrint, ConsoleColor toUse)
        {
            Console.ForegroundColor = toUse;
            Console.Write(symbolToPrint);
        }

        /// <summary>
        /// Prints the player current stats.
        /// </summary>
        /// <param name="player">The players start that you want to print</param>
        public static void PrintPlayerStats(Player player)
        {
            string[] messages = new string[5]
            {
                $"Current health: {player.Health:F2}/{Player.MaxHealth}     ",
                $"Current armor: {player.Defense}     ",
                $"Current damage: {player.Damage}     ",
                $"Current Level: {player.Level}     ",
                $"Current Exp {player.Experience}/{Player.NeedExperience}    "
            };

            StringBuilder result = new StringBuilder();
            for (int currentRow = ConsoleConstants.PlayerStatsPrintStartRow, messageIndex = 0; currentRow < 6; currentRow++, messageIndex++)
            {
                Console.SetCursorPosition(ConsoleConstants.PlayerStatsPrintStartCol, currentRow);

                Console.WriteLine(messages[messageIndex]);
            }
        }

        /// <summary>
        /// This method prints a symbol on a given position.
        /// </summary>
        /// <param name="newSymbol">The symbol that you want to print.</param>
        /// <param name="toPrintOn">The cell that you want to print it on.</param>
        public static void DeleteSymbolOnPositionAndPrintNewOne(char newSymbol, Position toPrintOn, ConsoleColor toUse)
        {
            Console.SetCursorPosition(toPrintOn.Col, toPrintOn.Row);
            PrintSymbolInColor(newSymbol, toUse);
        }

        /// <summary>
        /// Prints the wanted message and then terminates/ends the program.
        /// </summary>
        /// <param name="messageToEndTheGameWith">The message that you want to print.</param>
        public static void PrintEndGameMessage(string messageToEndTheGameWith)
        {
            Console.Clear();
            Console.WriteLine(messageToEndTheGameWith);

/*            Console.ReadLine();*/

            Environment.Exit(0);
        }
    }
}
