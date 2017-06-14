namespace RogueMetalicana.EnemyUnit
{
    using System;
    using RogueMetalicana.Constants.Enemy;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;

    /// <summary>
    /// Represents a single enemy in the dungeon
    /// </summary>
    public class Enemy : IPositionable, IFightable
    {
        /// <summary>
        /// Stats of the enemy.
        /// </summary>
        private string type;
        private int level;
        private double health;
        private int damage;
        private int defense;
        private int experienceGained;
        private int goldGained;
        private bool isAlive;

        /// <summary>
        /// Enemy Position in the dungeon.
        /// </summary>
        private Position position;

        public Enemy()
        {

        }

        public Enemy(string type, int level, double health, int damage, int defense, int experienceGained, int goldGained, Position position)
        {
            this.type = type;
            this.level = level;
            this.health = health;
            this.damage = damage;
            this.defense = defense;
            this.experienceGained = experienceGained;
            this.goldGained = goldGained;
            this.isAlive = true;
            this.position = position;
        }

        /// <summary>
        /// Get and set the enemy type outside of the class.
        /// </summary>
        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        /// <summary>
        /// Get and set the enemy level outside of the class.
        /// </summary>
        public int Level
        {
            get { return this.level; }
            set { this.level = value; }
        }
        /// <summary>
        /// Get and set the health outside of the class.
        /// </summary>
        public double Health
        {
            get { return this.health; }
            set { this.health = value; }
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
        /// Get and set the defense outside of the class.
        /// </summary>
        public int Defense
        {
            get { return this.defense; }
            set { this.defense = value; }
        }

        /// <summary>
        /// Get and set the experience gained after dead outside of the class.
        /// </summary>
        public int ExperienceGained
        {
            get { return this.experienceGained; }
            set { this.experienceGained = value; }
        }

        /// <summary>
        /// Get and set gold drop when enemy die.
        /// </summary>
        public int GoldGained
        {
            get { return this.goldGained; }
            set { this.goldGained = value; }
        }

        /// <summary>
        /// Get and set the live status outside of the class.
        /// </summary>
        public bool IsAlive
        {
            get { return this.isAlive; }
            set { this.isAlive = value; }
        }

        /// <summary>
        /// Get and set the position outside of the class.
        /// </summary>
        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public event EventHandler<EnemyEventArgs> EnemyDied;

        /// <summary>
        /// Player take damage to enemy.
        /// </summary>
        /// <param name="damageToTake"></param>
        public void TakeDamage(double damageToTake)
        {
            this.health -= damageToTake;

            if (this.health <= 0)
            {
                this.isAlive = false;
                OnEnemyDied();
            }
        }

        /// <summary>
        /// Invokes when enemy is died.
        /// </summary>
        protected virtual void OnEnemyDied()
        {
            EnemyDied?.Invoke(this, new EnemyEventArgs() { EnemyType = this.type, ExperienceGained = this.experienceGained, GoldGained = this.goldGained, Position = this.position, IsAlive = this.isAlive});
        }
    }
}
