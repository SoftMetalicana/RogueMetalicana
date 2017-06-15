namespace RogueMetalicana.Potion
{
    using System;
    using RogueMetalicana.Constants.Potions;
    public class Potion
    {
        private int id;

        /// <summary>
        /// Potion stats. If the potion is Health Potion all other stats will be set to 0
        /// </summary>
        private int xpBonus;
        private int healthBonus;
        private int damageBonus;
        private decimal marketPrice;
        private decimal sellPrice;

        public Potion(PotionType potionType)
        {
            this.id = PotionsConstants.CurrentId;
            PotionsConstants.CurrentId++;

            this.xpBonus = PotionsConstants.DefaultStat;
            this.healthBonus = PotionsConstants.DefaultStat;
            this.damageBonus = PotionsConstants.DefaultStat;
            this.marketPrice = PotionsConstants.MarketPrice;
            this.sellPrice = PotionsConstants.ResellPrice;
            switch (potionType)
            {
                case PotionType.HeathPotion: this.healthBonus = PotionsConstants.HealthBonus;
                    break;
                case PotionType.XpPotion: this.xpBonus = PotionsConstants.XpBonus;
                    break;
                case PotionType.DamageBonus: this.damageBonus = PotionsConstants.DamageBonus;
                    break;
                default:
                    break;
            }
        }

        public int UniqueId
        {
            get
            {
                return this.id;
            }
        }

        public int XpBonus
        {
            get
            {
                return this.xpBonus;
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
