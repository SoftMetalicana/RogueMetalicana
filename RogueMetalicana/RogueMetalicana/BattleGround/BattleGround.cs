namespace RogueMetalicana.BattleGround
{
    using EnemyUnit;
    using PlayerUnit;
    using System;

    public static class BattleGround
    {
        /// <summary>
        /// Generate random stats.
        /// </summary>
        private static readonly Random getRandom = new Random();

        /// <summary>
        /// This method generate random player damage, player armor, enemy damage, enemy armor.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        public static void Generate(Player player, Enemy enemy)
        {
            int randomDefensePlayerPoints = getRandom.Next(player.Defense);
            int randomDefenseEnemyPoints = getRandom.Next(enemy.Defense);

            int dealingDamagePlayer = getRandom.Next(1, player.Damage);
            int dealingDamageEnemy = getRandom.Next(1, enemy.Damage);

            int playerDefense = randomDefensePlayerPoints / dealingDamageEnemy;
            int enemyDefense = randomDefenseEnemyPoints / dealingDamagePlayer;

            Battle(dealingDamagePlayer, playerDefense, dealingDamageEnemy, enemyDefense, player, enemy);
        }

        /// <summary>
        /// Represent fighting.
        /// </summary>
        /// <param name="playerDealingDamage"></param>
        /// <param name="playerDefense"></param>
        /// <param name="enemyDealingDamage"></param>
        /// <param name="enemyDefense"></param>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        public static void Battle(int playerDealingDamage, int playerDefense, int enemyDealingDamage, int enemyDefense, Player player, Enemy enemy)
        {
            player.TakeDamage(enemyDealingDamage - playerDefense);
            enemy.TakeDamage(playerDealingDamage - enemyDefense);

            Generate(player, enemy);
        }
    }
}
