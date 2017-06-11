
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
                int currentRow = 0;
                int enemyId = 1;
                string currentLine;
                while ((currentLine = leverReader.ReadLine()) != default(string))
                {
                    dungeon.Add(currentLine.ToCharArray());

                    //Processing every row element by element and finding the positions of all units.

                    if (currentRow < Constants.Console.ConsoleConstants.FieldHeight)
                    {
                        for (int currentCol = 0; currentCol < Constants.Console.ConsoleConstants.FieldWidth; currentCol++)
                        {
                            char symbol = currentLine[currentCol];

                            switch (symbol)
                            {
                                case PlayerConstants.Symbol:
                                    player.Position = new Position(currentRow, currentCol);
                                    break;

                                case SnakeConstants.Symbol:
                                    allEnemies.Add(new Enemy(enemyId, SnakeConstants.Type, SnakeConstants.Level, SnakeConstants.Health, SnakeConstants.Damage, SnakeConstants.Defense, SnakeConstants.ExperienceGained, new Position(currentRow, currentCol)));
                                    enemyId++;
                                    break;

                                case LevelConstants.Wall:
                                case LevelConstants.Ground:
                                    break;

                                default:
                                    break;
                            }
                        }
                    } 
                    
                   currentRow++;
                }
            }
        }
    }
}
