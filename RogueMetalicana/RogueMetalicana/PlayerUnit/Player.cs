namespace RogueMetalicana.PlayerUnit
{
    using System;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;
    using RogueMetalicana.Constants.Player;

    /// <summary>
    /// This represents the player in the game.
    /// </summary>
    public class Player : IPositionable, IFightable
    {
        /// <summary>
        /// Stats of the player that describes his current health condition.
        /// MUSTN'T BE ACCESSED DIRECTLY OUTSIDE OF THIS CLASS.
        /// </summary>
        private int health;
        private int armor;
        private int damage;
        private bool isAlive;

        /// <summary>
        /// Stats of the player that describe his current career condition.
        /// MUSTN'T BE ACCESSED DIRECTLY OUTSIDE OF THIS CLASS.
        /// </summary>
        private int level;
        private int experience;
        private int gold;

        /// <summary>
        /// Current position in the dungeon.
        /// </summary>
        private Position position;
        
        /// <summary>
        /// Sets all the stats of the player to the starting ones.
        /// DOES NOT SET THE STARTING POSITION OF THE PLAYER!
        /// To set the position use .Position;
        /// </summary>
        /// <param name="startingPosition">The starting position in the dungeon.</param>
        public Player()
        {
            this.health = PlayerConstants.StartingHealth;
            this.armor = PlayerConstants.StartingArmor;
            this.damage = PlayerConstants.StartingDamage;
            this.isAlive = false;

            this.level = PlayerConstants.StartingLevel;
            this.experience = PlayerConstants.StartinExperience;
            this.gold = PlayerConstants.StartingGold;
        }

        /// <summary>
        /// Get and set the health outside of the class.
        /// </summary>
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        /// <summary>
        /// Get and set the armor outside of the class.
        /// </summary>
        public int Armor
        {
            get { return this.armor; }
            set { this.armor = value; }
        }

        /// <summary>
        /// Get and set the damage outside of the class.
        /// </summary>
        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        /// <summary>
        /// Get and set the level outside of the class.
        /// </summary>
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }

        /// <summary>
        /// Get and set the experience outside of the class.
        /// </summary>
        public int Experience
        {
            get { return this.experience; }
            set { this.experience = value; }
        }

        /// <summary>
        /// Get and set the gold outside of the class.
        /// </summary>
        public int Gold
        {
            get { return this.gold; }
            set { this.gold = value; }
        }

        /// <summary>
        /// Get and set the position outside of the class.
        /// </summary>
        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        /// <summary>
        /// Get and set outside of the class.
        /// </summary>
        public bool IsAlive
        {
            get { return this.isAlive; }
            set { this.isAlive = value; }
        }

        public void TakeDamage(int damageToTake)
        {
            throw new NotImplementedException();
        }
    }
}
