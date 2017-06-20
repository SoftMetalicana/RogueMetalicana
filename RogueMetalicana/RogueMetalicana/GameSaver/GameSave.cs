namespace RogueMetalicana.GameSaver
{
    using RogueMetalicana.Constants.Potions;
    using RogueMetalicana.LevelEngine;
    using RogueMetalicana.PlayerUnit;
    using System;
    using System.IO;
    using System.Linq;

    class GameSave
    {
        /// <summary>
        /// Splitted by ','
        /// First line 1 - Damage, 2 - Defence, 3- XP, 4- Gold, 5 - HP, 6-Level, healthPotionsStored, xpPotionsTored, damagePotionsStored
        /// </summary>
        public void OnLevelGenerated(object sender, LevelGeneratedEventArgs eventArgs)
        {
            Player player = eventArgs.Player;

            var healthPotionsStored = player.PotionInventory.Where(p => p.PotionType == PotionType.HealthPotion).ToArray().Length;
            var xpPotionsStored = player.PotionInventory.Where(p => p.PotionType == PotionType.XpPotion).ToArray().Length;
            var damagePotionsStored = player.PotionInventory.Where(p => p.PotionType == PotionType.BonusDamagePotion).ToArray().Length;
            var playerStatsToSave = string.Join("," ,player.Damage, player.Defense, player.Experience, player.Gold, player.Health, player.Level, healthPotionsStored, xpPotionsStored, damagePotionsStored);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameSave.txt");
            File.WriteAllText(path, playerStatsToSave);
        }
    }
}
