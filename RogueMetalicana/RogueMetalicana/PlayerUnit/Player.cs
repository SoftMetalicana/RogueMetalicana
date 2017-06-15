namespace RogueMetalicana.PlayerUnit
{
    using System;
    using RogueMetalicana.Constants.Player;
    using RogueMetalicana.Constants.Position;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;
    using RogueMetalicana.Potion;
    using System.Collections.Generic;

    /// <summary>
    /// This represents the player in the game.
    /// </summary>
    public class Player : IPositionable, IFightable
    {
        public static int NeedExperience = 60;
        public static double MaxHealth;

        /// <summary>
        /// Stats of the player that describes his current health condition.
        /// MUSTN'T BE ACCESSED DIRECTLY OUTSIDE OF THIS CLASS.
        /// </summary>
        private double health;
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
            MaxHealth = this.health;

            this.armor = PlayerConstants.StartingArmor;
            this.damage = PlayerConstants.StartingDamage;
            this.isAlive = false;

            this.level = PlayerConstants.StartingLevel;
            this.experience = PlayerConstants.StartinExperience;
            this.gold = PlayerConstants.StartingGold;

            this.potionInventory = new List<Potion>();
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
        /// Get and set the armor outside of the class.
        /// </summary>
        public int Defense
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

        public event EventHandler<PlayerEventArgs> PlayerMoved;
        public event EventHandler<PlayerEventArgs> PlayerDied;

        public void MakeAMove()
        {
            Direction newDirection = default(Direction);

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    newDirection = Direction.Up;
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    newDirection = Direction.Down;
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    newDirection = Direction.Left;
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    newDirection = Direction.Right;
                    break;

                default:
                    break;
            }

            Position directionPosition = Position.DirectionPositions[newDirection];
            Position newPlayerPosition = new Position(directionPosition.Row, directionPosition.Col);
            newPlayerPosition.Row += this.position.Row;
            newPlayerPosition.Col += this.position.Col;

            OnPlayerMoved(newPlayerPosition);
        }

        protected virtual void OnPlayerMoved(Position newPlayerPosition)
        {
            PlayerMoved?.Invoke(this, new PlayerEventArgs() { NewPlayerPosition = newPlayerPosition });
        }

        /// <summary>
        /// Deal damage to player from enemy.
        /// </summary>
        /// <param name="damageToTake"></param>
        public void TakeDamage(double damageToTake)
        {
            this.health -= damageToTake;

            if (this.health <= 0)
            {
                OnPlayerDied();
            }
        }

        protected virtual void OnPlayerDied()
        {
            PlayerDied?.Invoke(this, new PlayerEventArgs());
        }

        /// <summary>
        /// Add experience to the player when enemy die.
        /// </summary>
        /// <param name="experienceGained"></param>
        public void GainGoldAndExperience(int experienceGained, int goldGained)
        {
            this.experience += experienceGained;
            this.gold += goldGained;

            while (experience >= NeedExperience)
            {
                LevelUp();
            } 
        }

        /// <summary>
        /// Invoke when player experience >= needed experience.
        /// </summary>
        private void LevelUp()
        {
            this.experience -= NeedExperience;
            this.level++;
            this.armor += 2;
            this.damage += 10;
            MaxHealth += 50;
            this.health += 25;
            NeedExperience *= 2;
            gold += 10;
        }

        /// <summary>
        /// Adds potion bonuses, takes care if they are above the maximum and removes the potion from the inventory
        /// </summary>
        /// <param name="currentPotion"></param>
        public void ConsumePotion(Potion currentPotion)
        {
            if (currentPotion == null)
            {
                return;
            }
            this.health += currentPotion.HealthBonus;
            this.damage += currentPotion.DamageBonus;
            this.experience += currentPotion.XpBonus;
            if (this.experience>= NeedExperience)
            {
                LevelUp();
            }
            if (this.health>MaxHealth)
            {
                this.health = MaxHealth;
            }

            var indexOfCurrentPotion = potionInventory.FindIndex(p=> p.UniqueId == currentPotion.UniqueId);
            if (indexOfCurrentPotion!=-1)
            {
                potionInventory.RemoveAt(indexOfCurrentPotion);
            }
        }

        private List<Potion> potionInventory;

        public List<Potion> PotionInventory
        {
            get
            {
                return this.potionInventory;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
