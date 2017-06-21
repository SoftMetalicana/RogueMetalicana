

namespace RogueMetalicana.Constants.Shop
{
    public class ShopConstants
    {
        public const char Symbol = '$';

        public static string lastBought { get; set; }

        public static bool lastSuccess { get; set; }

        public static void PrintLastShopAction()
        {
            if (lastBought==null)
            {
                return;
            }
            if (lastSuccess)
            {
                Visualization.Visualisator.PrintUnderTheBattleField($"You have bought {lastBought} for {Constants.Potions.PotionsConstants.MarketPrice} gold");
            }
            else
            {
                Visualization.Visualisator.PrintUnderTheBattleField($"You don't have enought money!");

            }
        }

    }
}
