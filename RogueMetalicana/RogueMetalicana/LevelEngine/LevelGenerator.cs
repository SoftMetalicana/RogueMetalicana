
using System;

namespace RogueMetalicana.LevelEngine
{
    using System.IO;
    using RogueMetalicana.Constants.Level;
    using RogueMetalicana.PlayerUnit;
    using System.Collections.Generic;
    using RogueMetalicana.EnemyUnit;
    using RogueMetalicana.Constants.Player;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.Constants.Enemy;
    using RogueMetalicana.Visualization;

    /// <summary>
    /// Generates a level.
    /// Finds all enemies, the player and saves the dungeon into a given variable.
    /// </summary>
    public class LevelGenerator
    {
        /// <summary>
        /// The stream reads the dungeon from the fiven file line by line.
        /// </summary>
        private StreamReader leverReader;
        /// <summary>
        /// Full level path holds the path to the current level.
        /// </summary>
        private string fullLevelPath;

        /// <summary>
        /// Variable that holds the index of the current level.
        /// </summary>
        private int currentLevelNumber;

        /// <summary>
        /// Holds basic data for all enemies which will be encountered in the current level 
        /// </summary>
        private static Dictionary<char, KeyValuePair<string, int>> levelEnemies;

        /// <summary>
        /// Initializes the field variables.
        /// </summary>
        public LevelGenerator()
        {
            this.currentLevelNumber = LevelConstants.StartLevelNumber;

            GenerateFullLevelPath();
        }

        public void GenerateFullLevelPath()
        {
            this.fullLevelPath = LevelConstants.LoadingPath +
                                             LevelConstants.FolderName +
                                             LevelConstants.LevelLoadName +
                                             this.currentLevelNumber++ +
                                             LevelConstants.FileExtension;

            this.leverReader = new StreamReader(fullLevelPath);
        }

        /// <summary>
        /// Processes the level and inializes the arguments.
        /// Finds the player position and the enemies positions.
        /// Fills the dungeon.
        /// </summary>
        /// <param name="player">The player in the game that you want to initialize.</param>
        /// <param name="allEnemies">The enemies in the game which you want to initialize.</param>
        /// <param name="dungeon">The dungeon that you want to create.</param>
        public void GenerateLevel(Player player, List<Enemy> allEnemies, List<char[]> dungeon)
        {
            using (leverReader)
            {
                // Processes each line of the level txt file and either adds this line as char array to the dungeon list 
                // or passes it to the GenerateEnemiesList method whenever the line hold technical info i.e. starts with /I/E for enemies or /I/O for obstacles
                string currentLine;
                int currentRow = 0;
                while ((currentLine = leverReader.ReadLine()) != default(string))
                {
                    if (currentRow < Constants.Console.ConsoleConstants.FieldHeight)
                    {
                        dungeon.Add(currentLine.ToCharArray());
                    }
                    else
                    {
                        if (currentLine != string.Empty)
                        {
                            var objectTokens = currentLine.Split(LevelConstants.LegendSplitSymbols, StringSplitOptions.RemoveEmptyEntries);

                            switch (objectTokens[0])
                            {
                                case LevelConstants.EnemyInput:
                                    GenerateEnemiesList(objectTokens);
                                    break;
                                case LevelConstants.ObstacleInput:
                                    //to be implemented process for obstacles creation
                                    break;
                            }
                        }                       
                    }
                    
                   currentRow++;
                }



                //Processing every row element by element and finding the positions of all units.
                currentRow = 0;
                foreach (var line in dungeon)
                {

                    for (int currentCol = 0;
                        currentCol < Constants.Console.ConsoleConstants.FieldWidth;
                        currentCol++)
                    {
                        char symbol = line[currentCol];

                        switch (symbol)
                        {
                            case PlayerConstants.Symbol:
                                player.Position = new Position(currentRow, currentCol);
                                break;

                            case LevelConstants.Wall:
                            case LevelConstants.Ground:
                                break;

                            default:
                                GenerateCurrentEnemy(symbol, allEnemies, new Position(currentRow, currentCol));
                                break;
                        }
                    }

                    currentRow++;
                }

                Visualisator.PrintDungeon(dungeon, player);
            }
        }

        /// <summary>
        /// Generates an enemy based on its symbol whenever the symbol is encountered in the level template and adds this current enemy to the allEnemies list.
        /// Checks whether the symbol is already populated in the levelEnemies dictionary and based on the that data creates the enemy.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="allEnemies"></param>
        /// <param name="position"></param>
        private void GenerateCurrentEnemy(char symbol, List<Enemy>allEnemies, Position position)
        {
            if (!levelEnemies.ContainsKey(symbol))
            {
                // Printout message
            }
            else
            {
                var type = levelEnemies[symbol].Key;
                var difficulty = levelEnemies[symbol].Value;

                switch (difficulty)
                {
                    case (int)EnemyDifficulty.Easy: allEnemies.Add(new Enemy(type, currentLevelNumber, Easy.Health, Easy.Damage, Easy.Defense, Easy.ExperienceGained, position));
                        break;
                    case (int)EnemyDifficulty.Medium:
                        allEnemies.Add(new Enemy(type, currentLevelNumber, Medium.Health, Medium.Damage, Medium.Defense, Medium.ExperienceGained, position));
                        break;
                    case (int)EnemyDifficulty.Difficult:
                        allEnemies.Add(new Enemy(type, currentLevelNumber, Difficult.Health, Difficult.Damage, Difficult.Defense, Difficult.ExperienceGained, position));
                        break;
                }
            }
        }

        /// <summary>
        /// Adds to levelEnemies dictionary all custom input enemies described in the level txt file with their symbol, type and difficulty level.
        /// </summary>
        /// <param name="dungeonInfoLine"></param>
        private void GenerateEnemiesList(string[] dungeonInfoLine)
        {
            // Checks if the input line holds all necessary information i.e. - symbol, type and difficulty level (+ type of object at index 0)
            if (dungeonInfoLine.Length < LevelConstants.EnemyInputArrayLength)
            {
                return;                
            }

            var enemyCharacter = dungeonInfoLine[1].ToCharArray();
            var enemyType = dungeonInfoLine[2];
            var enemyDifficulty = int.Parse(dungeonInfoLine[3]);

            if (levelEnemies == null)
            {
                levelEnemies = new Dictionary<char, KeyValuePair<string, int>>();
            }

            if (!levelEnemies.ContainsKey(enemyCharacter[0]))
            {
                levelEnemies[enemyCharacter[0]] = new KeyValuePair<string, int>(enemyType, enemyDifficulty);
            }

        }
    }
}
