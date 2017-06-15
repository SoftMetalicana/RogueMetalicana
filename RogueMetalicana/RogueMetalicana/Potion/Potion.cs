namespace RogueMetalicana.Potion
{
    using System;
    using RogueMetalicana.Constants.Potions;
    class Potion
    {
        /// <summary>
        /// Potion stats. If the potion is Health Potion all other stats will be set to 0
        /// </summary>
        private int manaBonus;
        private int healthBonus;
        private int damageBonus;
        private decimal marketPrice;
        private decimal sellPrice;

        public Potion(PotionType potionType)
        {
            this.manaBonus = PotionsConstants.DefaultStat;
            this.healthBonus = PotionsConstants.DefaultStat;
            this.damageBonus = PotionsConstants.DefaultStat;
            this.marketPrice = PotionsConstants.MarketPrice;
            this.sellPrice = PotionsConstants.ResellPrice;
            switch (potionType)
            {
                case PotionType.HeathPotion: this.healthBonus = PotionsConstants.HealthBonus;
                    break;
                case PotionType.ManaPotion: this.manaBonus = PotionsConstants.ManaBonus;
                    break;
                case PotionType.DamageBonus: this.damageBonus = PotionsConstants.DamageBonus;
                    break;
                default:
                    break;
            }
        }

        public int ManaBonus
        {
            get
            {
                return this.manaBonus;
            }
        }

        public int HealthBonus
        {
            get
            {
                return this.healthBonus;
            }
        }

        public int DamageBonus
        {
            get
            {
                return this.damageBonus;
            }
        }
    }
}
