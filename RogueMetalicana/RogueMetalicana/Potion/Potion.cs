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
        private int marketPrice;
        private int sellPrice;
        private PotionType type;

        /// <summary>
        // 
        /// </summary>
        /// <param name="potionType"></param>
        public Potion(PotionType potionType)
        {
            this.id = PotionsConstants.CurrentId;
            PotionsConstants.CurrentId++;

            this.xpBonus = PotionsConstants.DefaultStat;
            this.healthBonus = PotionsConstants.DefaultStat;
            this.damageBonus = PotionsConstants.DefaultStat;
            this.marketPrice = PotionsConstants.MarketPrice;
            this.sellPrice = PotionsConstants.ResellPrice;
            this.type = potionType;
            switch (potionType)
            {
                case PotionType.HealthPotion: this.healthBonus = GenerataRandomValue(PotionType.HealthPotion);
                    break;
                case PotionType.XpPotion: this.xpBonus = GenerataRandomValue(PotionType.XpPotion);
                    break;
                case PotionType.BonusDamagePotion: this.damageBonus = GenerataRandomValue(PotionType.BonusDamagePotion);
                    break;
                default:
                    break;
            }
        }

        private static int GenerataRandomValue(PotionType type)
        {
            var rndm = new Random();
            switch (type)
            {
                case PotionType.HealthPotion: var healthBonus = rndm.Next(PotionsConstants.MinHealthBonus, PotionsConstants.MaxHealthBonus);
                    return healthBonus;
                case PotionType.XpPotion: var xpBonus = rndm.Next(PotionsConstants.MinXpBonus, PotionsConstants.MaxXpBonus);
                    return xpBonus;
                case PotionType.BonusDamagePotion: var dmgBonus = rndm.Next(PotionsConstants.MinDmgBonus, PotionsConstants.MaxDamageBonus);
                    return dmgBonus;
                default: return 0;
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

        public PotionType PotionType
        {
            get
            {
                return this.type;
            }
        }

        public int DamageBonus
        {
            get
            {
                return this.damageBonus;
            }
        }

        public int MarketValue
        {
            get
            {
                return this.marketPrice;
            }
        }

        public int ResellPrice
        {
            get
            {
                return this.ResellPrice;
            }
        }
    }
}
