namespace RogueMetalicana.GameEngine
{
    using RogueMetalicana.EnemyUnit;
    using RogueMetalicana.PlayerUnit;
    using System.Collections.Generic;
    
    /// <summary>
    /// Packs everything needed in the game into itself.
    /// Takes care of objects interactions.
    /// </summary>
    public class Engine
    {
        private Player player;
        private List<Enemy> allEnemies;

        private List<string> dungeon;

        public Engine(Player player, List<Enemy> allEnemies, List<string> dungeon)
        {
            this.player = player;
            this.allEnemies = allEnemies;

            this.dungeon = dungeon;
        }
    }
}
