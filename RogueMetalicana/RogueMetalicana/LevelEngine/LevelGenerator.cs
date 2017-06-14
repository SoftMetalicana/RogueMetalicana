
using System;
using System.Linq;

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
    using RogueMetalicana.MapPlace;

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
        /// Holds basic data for all enemies which will be encountered in the current level and described in the map's metadata
        /// </summary>
        private static Dictionary<char, KeyValuePair<string, int>> levelEnemies;

        /// <summary>
        /// Holds data for all obstacles in the current level described in the map's metadata
        /// For the time being the obstacles are hardcoded and this info is only aimed to provide source for printing the legend
        /// </summary>
        private static Dictionary<char, KeyValuePair<string, string>> levelObstacles;

        /// <summary>
        /// Holds data for all places in the current level described in the map's metadata
        /// </summary>
        private static Dictionary<KeyValuePair<char, string>, KeyValuePair<Place.PlaceGain, int>> levelPlaces;

        public static string CurrentMapLegend;

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
        public void GenerateLevel(Player player, List<Enemy> allEnemies, List<Place> allPlaces, List<char[]> dungeon)
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
                                    GenerateObstaclesList(objectTokens);
                                    break;
                                case LevelConstants.PlaceInput:
                                    GeneratePlacesList(objectTokens);
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
                            case LevelConstants.Door:
                            case LevelConstants.SpellboundForest:
                            case LevelConstants.Lava:
                            case LevelConstants.RiverOfMercury:
                                break;

                            default:

                                if (levelEnemies.ContainsKey(symbol))
                                {
                                    GenerateCurrentEnemy(symbol, allEnemies, new Position(currentRow, currentCol));
                                    break;
                                } else
                                {
                                    GenerateCurrentPlace(symbol, allPlaces, new Position(currentRow, currentCol));
                                    break;
                                }
                        }
                    }

                    currentRow++;
                }

                Visualisator.PrintDungeon(dungeon, player);
                CurrentMapLegend = Visualisator.PrintMapLegend(levelEnemies, levelObstacles, levelPlaces);
                Visualisator.PrintOnTheConsole(CurrentMapLegend);

                Console.SetWindowPosition(0, 0);

            }
        }

        /// <summary>
        ///Generates a place based on its symbol whenever the symbol is encountered in the level template and adds this current place to the allPlaces list.
        /// Checks whether the symbol is already populated in the levelPlaces dictionary and based on the that data creates the place.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="allPlaces"></param>
        /// <param name="position"></param>
        private void GenerateCurrentPlace(char symbol, List<Place> allPlaces, Position position)
        {
            // all places that are described with a symbol, type, gain and value in the map legend
            var describedPlaces = levelPlaces.Keys.Select(x => x.Key).ToList();

            if (!describedPlaces.Contains(symbol))
            {
                // Printout message
            }
            else
            {
                var type = levelPlaces.Keys.Where(x => x.Key == symbol).First().Value;
                var gain = levelPlaces[new KeyValuePair<char, string>(symbol, type)].Key;
                var value = levelPlaces[new KeyValuePair<char, string>(symbol, type)].Value;

                var currentPlace = new Place(type, gain, value, position);

                allPlaces.Add(currentPlace);
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
                    case (int)EnemyDifficulty.Easy:
                        allEnemies.Add(new Enemy(type, currentLevelNumber, Easy.Health, Easy.Damage, Easy.Defense, Easy.ExperienceGained, Easy.GoldGained, position));
                        break;
                    case (int)EnemyDifficulty.Medium:
                        allEnemies.Add(new Enemy(type, currentLevelNumber, Medium.Health, Medium.Damage, Medium.Defense, Medium.ExperienceGained, Medium.GoldGained, position));
                        break;
                    case (int)EnemyDifficulty.Difficult:
                        allEnemies.Add(new Enemy(type, currentLevelNumber, Difficult.Health, Difficult.Damage, Difficult.Defense, Difficult.ExperienceGained, Difficult.GoldGained, position));
                        break;
                }
            }
        }

        /// <summary>
        /// Adds to levelEnemies dictionary all custom input enemies described in the level txt file with their symbol, type and difficulty level.
        /// </summary>
        /// <param name="dungeonInfoLine"></param>
        private void GenerateEnemiesList(string[] enemyInfoLine)
        {
            // Checks if the input line holds all necessary information i.e. - symbol, type and difficulty level (+ type of object at index 0)
            if (enemyInfoLine.Length < LevelConstants.EnemyInputArrayLength)
            {
                return;                
            }

            var enemyCharacter = enemyInfoLine[1].ToCharArray();
            var enemyType = enemyInfoLine[2];
            var enemyDifficulty = int.Parse(enemyInfoLine[3]);

            if (levelEnemies == null)
            {
                levelEnemies = new Dictionary<char, KeyValuePair<string, int>>();
            }

            if (!levelEnemies.ContainsKey(enemyCharacter[0]))
            {
                levelEnemies[enemyCharacter[0]] = new KeyValuePair<string, int>(enemyType, enemyDifficulty);
            }
        }

        /// <summary>
        /// Adds to levelObstacles dictionary all obstacles described in the level txt file with their symbol, type and description
        /// </summary>
        /// <param name="obstacleInfoLine"></param>
        private void GenerateObstaclesList(string[] obstacleInfoLine)
        {
            // Checks if the input line holds all necessary information i.e. - symbol, type and description (+ type of object at index 0)
            if (obstacleInfoLine.Length < LevelConstants.ObstacleInputArrayLength)
            {
                return;
            }

            var obstacleCharacter = obstacleInfoLine[1].ToCharArray();
            var obstacleType = obstacleInfoLine[2];
            var obstacleDescription = obstacleInfoLine[3];

            if (levelObstacles == null)
            {
                levelObstacles = new Dictionary<char, KeyValuePair<string, string>>();
            }

            if (!levelObstacles.ContainsKey(obstacleCharacter[0]))
            {
                levelObstacles[obstacleCharacter[0]] = new KeyValuePair<string, string>(obstacleType, obstacleDescription);
            }
        }

        private void GeneratePlacesList(string[] placeInfoLine)
        {

            // Checks if the input line holds all necessary information i.e. - symbol, type, type of gain and value (+ type of object at index 0)
            if (placeInfoLine.Length < LevelConstants.PlaceInputArrayLength)
            {
                return;
            }

            var placeChar = placeInfoLine[1].ToCharArray();
            var placeType = placeInfoLine[2];
            Place.PlaceGain placeGain = (Place.PlaceGain)int.Parse(placeInfoLine[3]);
            var placeValue = int.Parse(placeInfoLine[4]);

            if (levelPlaces == null)
            {
                levelPlaces = new Dictionary<KeyValuePair<char, string>, KeyValuePair<Place.PlaceGain, int>>();
            }

            var key = new KeyValuePair<char, string>(placeChar[0], placeType);

            if (!levelPlaces.ContainsKey(key))
            {
                levelPlaces[key] = new KeyValuePair<Place.PlaceGain, int>(placeGain, placeValue);
            }
        }
    }
}
