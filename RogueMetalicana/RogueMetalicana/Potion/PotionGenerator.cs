namespace RogueMetalicana.Potion
{
    using RogueMetalicana.Constants.Potions;
    using System;

    class PotionGenerator
    {
        public static Potion GeneratePotion()
        {
            var values = Enum.GetValues(typeof(PotionType));
            var random = new Random();
            PotionType randomType = (PotionType)values.GetValue(random.Next(values.Length));
            return new Potion(randomType);
        }
    }
}
