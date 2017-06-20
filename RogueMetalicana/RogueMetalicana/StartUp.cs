namespace RogueMetalicana
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RogueMetalicana.ConsoleCare;
    using RogueMetalicana.PlayerUnit;
    using RogueMetalicana.EnemyUnit;
    using RogueMetalicana.LevelEngine;
    using RogueMetalicana.GameEngine;
    using RogueMetalicana.Visualization;
    using RogueMetalicana.Menu;
    using RogueMetalicana.MapPlace;
    using RogueMetalicana.GameSaver;

    public class StartUp
    {
        public static void Main()
        {
            //Sets the console size for the game and makes the cursor invisible
            ConsoleManager.SetTheConsoleForTheGame();

            //Opens the start menu
            Menu.Menu.StartMenu();

            //Returns the default console settings
            ConsoleManager.SetTheConsoleForTheGame();
            
            //Creating instances for the player, enemies and the dungeon
            Player player = new Player();
            List<Enemy> allEnemies = new List<Enemy>();
            List<Place> allPlaces = new List<Place>();
            List<char[]> dungeon = new List<char[]>();

            //Processes and generates the level from the Level.txt file
            //Sets the player position fills the list with enemies and creates the dungeon from the file.
            LevelGenerator levelGenerator = new LevelGenerator();
            levelGenerator.GenerateLevel(player, allEnemies, allPlaces, dungeon);

            GameSave gameSave = new GameSave();
            levelGenerator.LevelGenerated += gameSave.OnLevelGenerated;

            //Knows about all objects/units and takes the care about the interaction of the units.
            Engine gameEngine = new Engine(player, allEnemies, allPlaces, dungeon, levelGenerator);

            //The movement of the player is an event
            //The engine is subscribed to this event so it can know about every move of the player.
            player.PlayerMoved += gameEngine.OnPlayerMoved;
            player.PlayerDied += gameEngine.OnPlayerDied;
            foreach (Enemy enemy in allEnemies)
            {
                enemy.EnemyDied += gameEngine.OnEnemyDied;
            }

            //Visualisator.PrintDungeon(dungeon, player);
            while (true)
            {
                player.MakeAMove();
                //Visualisator.PrintDungeon(gameEngine.Dungeon, player);

                //player.TakeDamage(12);
            }
        }
    }
}
