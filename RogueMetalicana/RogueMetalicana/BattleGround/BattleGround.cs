
namespace RogueMetalicana.BattleGround
{
    using EnemyUnit;
    using PlayerUnit;
    using System;

    public static class BattleGround
    {
        private static readonly Random getRandom = new Random();

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

        public static void Battle(int playerDealingDamage, int playerDefense, int enemyDealingDamage, int enemyDefense, Player player, Enemy enemy)
        {
            

            player.TakeDamage(enemyDealingDamage - playerDefense);
            enemy.TakeDamage(playerDealingDamage - enemyDefense);

            Generate(player, enemy);
        }
    }
}
