namespace RogueMetalicana.LevelEngine
{
    using System.IO;
    using RogueMetalicana.Constants.Level;
    using RogueMetalicana.PlayerUnit;
    using System.Collections.Generic;
    using RogueMetalicana.EnemyUnit;
    using RogueMetalicana.Constants.Player;

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
        /// Initializes the field variables.
        /// </summary>
        public LevelGenerator()
        {
            this.currentLevelNumber = LevelConstants.StartLevelNumber;

            this.fullLevelPath = LevelConstants.LoadingPath + 
                                 LevelConstants.FolderName + 
                                 LevelConstants.LevelLoadName + 
                                 LevelConstants.StartLevelNumber + 
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
        public void GenerateLevel(Player player, List<Enemy> allEnemies, List<string> dungeon)
        {
            using (leverReader)
            {
                int row = 0;
                string currentLine;
                while ((currentLine = leverReader.ReadLine()) != default(string))
                {
                    dungeon.Add(currentLine);

                    for (int currentSymbol = 0; currentSymbol < currentLine.Length; currentSymbol++)
                    {
                        char symbol = currentLine[currentSymbol];

                        switch (symbol)
                        {
                            case PlayerConstants.Symbol:
                                break;

                            case LevelConstants.Wall:
                                break;

                            case LevelConstants.Ground:
                                break;

                            default:
                                break;
                        }
                    }

                    row++;
                }
            }
        }
    }
}
