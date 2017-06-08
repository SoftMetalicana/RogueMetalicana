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

    public class StartUp
    {
        public static void Main()
        {
            ConsoleManager.SetConsoleSize();

            Player player = new Player();
            List<Enemy> allEnemies = new List<Enemy>();
            List<string> dungeon = new List<string>();

            LevelGenerator levelGenerator = new LevelGenerator();
            levelGenerator.GenerateLevel(player, allEnemies, dungeon);

            Engine gameEngine = new Engine(player, allEnemies, dungeon);
        }
    }
}
