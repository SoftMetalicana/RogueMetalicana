namespace RogueMetalicana.Constants.Player
{
    /// <summary>
    /// Provides all variables needed for the Player.cs
    /// </summary>
    public static class PlayerConstants
    {
        /// <summary>
        /// The player symbol in the game.
        /// </summary>
        public const char Symbol = '@';

        /// <summary>
        /// Starting stats for the player
        /// </summary>
        public const int StartingHealth = 100;
        public const int StartingArmor = 20;
        public const int StartingDamage = 25;

        public const int StartingLevel = 1;
        public const int StartinExperience = 0;
        public const int StartingGold = 50;

        /// <summary>
        /// Players death messages
        /// </summary>
        public const string FellOfTheDungeonMessage = "You fell of the dungeon...";
        public const string SteppedIntoLavaMessage = "You died because you stepped into hot lava...";
        public const string LostIntoSpellboundForest = "You died because you lost yourself into the spellbound forest and starved to death...";
    }
}
