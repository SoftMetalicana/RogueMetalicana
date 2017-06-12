
namespace RogueMetalicana.BattleGround
{
    using EnemyUnit;
    using PlayerUnit;
    using System;
    using System.Text;

    public static class BattleGround
    {
        public static StringBuilder BattleResult = new StringBuilder();
        /// <summary>
        /// GenerateStats random stats.
        /// </summary>
        private static readonly Random getRandom = new Random();

        /// <summary>
        /// This method generate random player damage, player armor, enemy damage, enemy armor.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        public static void GenerateStats(Player player, Enemy enemy)
        {
            double randomDefensePlayerPoints = getRandom.Next(0, player.Defense);
            double randomDefenseEnemyPoints = getRandom.Next(0, enemy.Defense);

            double dealingDamagePlayer = getRandom.Next(1, player.Damage);
            double dealingDamageEnemy = getRandom.Next(1, enemy.Damage);

            double playerDefense = Math.Round((randomDefensePlayerPoints / dealingDamageEnemy), 2);
            double enemyDefense = Math.Round((randomDefenseEnemyPoints / dealingDamagePlayer), 2);

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
        public static void Battle(double playerDealingDamage, double playerDefense, double enemyDealingDamage, double enemyDefense, Player player, Enemy enemy)
        {

            if (enemyDealingDamage - playerDefense >= 0)
            {
                BattleResult.AppendLine($"Enemy hits Player with {enemyDealingDamage} damage");
                player.TakeDamage(enemyDealingDamage - playerDefense);
            }

            if (playerDealingDamage - enemyDefense >= 0)
            {
                BattleResult.AppendLine($"Player hits {enemy.Type} with {playerDealingDamage} damage").AppendLine();
                enemy.TakeDamage(playerDealingDamage - enemyDefense);
            }

            if (enemy.IsAlive == true)
            {
                GenerateStats(player, enemy);
            }
        }
    }
}
