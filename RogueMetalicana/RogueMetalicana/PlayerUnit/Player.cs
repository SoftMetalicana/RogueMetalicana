namespace RogueMetalicana.PlayerUnit
{
    using System;
    using System.Linq;
    using RogueMetalicana.Constants.Player;
    using RogueMetalicana.Constants.Position;
    using RogueMetalicana.Constants.Potions;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;
    using RogueMetalicana.Potion;
    using System.Collections.Generic;
    using RogueMetalicana.BattleGround;

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
                case ConsoleKey.D1:
                    ConsumePotion(PotionType.HealthPotion);
                    return;
                case ConsoleKey.D2:
                    ConsumePotion(PotionType.XpPotion);
                    return;
                case ConsoleKey.D3:
                    ConsumePotion(PotionType.BonusDamagePotion);
                    return;
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
        /// <param name="potionType"></param>
        public void ConsumePotion(PotionType potionType)
        {
            
            var foundPotion = PotionInventory.FirstOrDefault(p => p.PotionType == potionType);

            if (foundPotion== null)
            {
                Visualization.Visualisator.PrintUnderTheBattleField(PotionsConstants.UnavaiblePotion + potionType + "s!");
                return;

            }
            switch (potionType)
            {
                case PotionType.HealthPotion:
                    Visualization.Visualisator.PrintUnderTheBattleField(PotionsConstants.ConsumedPotion + foundPotion.HealthBonus + " health!");
                    break;
                case PotionType.XpPotion:
                    Visualization.Visualisator.PrintUnderTheBattleField(PotionsConstants.ConsumedPotion + foundPotion.XpBonus + " experience!");
                    break;
                case PotionType.BonusDamagePotion:
                    Visualization.Visualisator.PrintUnderTheBattleField(PotionsConstants.ConsumedPotion + foundPotion.DamageBonus + " bonus damage!");
                    break;
                default:
                    break;
            }

            this.health += foundPotion.HealthBonus;
            this.damage += foundPotion.DamageBonus;
            this.experience += foundPotion.XpBonus;
            if (this.experience>= NeedExperience)
            {
                LevelUp();
            }
            if (this.health>MaxHealth)
            {
                this.health = MaxHealth;
            }

            var indexOfCurrentPotion = potionInventory.IndexOf(foundPotion);
            if (indexOfCurrentPotion!=-1)
            {
                potionInventory.RemoveAt(indexOfCurrentPotion);
            }
        }


        /// <summary>
        /// Holds all the looted potions
        /// </summary>
        private List<Potion> potionInventory;

        public List<Potion> PotionInventory
        {
            get
            {
                return this.potionInventory;
            }
        }

        public void RecievePotion(Potion potion)
        {
            if (potion==null)
            {
                return;
            }
            this.potionInventory.Add(potion);
        }

        public  void BuyPotion(string potionType)
        {
            if (potionType == null)
            {
                return;
            }

            switch (potionType)
            {
                case "HealthPotion":
                    if (DoesHeHasEnoughKinti())
                    {
                        PotionInventory.Add(new Potion(PotionType.HealthPotion));
                        Gold -= Constants.Potions.PotionsConstants.MarketPrice;
                    }
                    break;
                case "XpPotion":
                    if (DoesHeHasEnoughKinti())
                    {
                        PotionInventory.Add(new Potion(PotionType.XpPotion));
                        Gold -= Constants.Potions.PotionsConstants.MarketPrice;
                    }
                    break;
                case "BonusDamagePotion":
                    if (DoesHeHasEnoughKinti())
                    {
                        PotionInventory.Add(new Potion(PotionType.BonusDamagePotion));
                        Gold -= Constants.Potions.PotionsConstants.MarketPrice;
                    }
                    break;
                default:
                    break;
            }
          
        }

        private bool DoesHeHasEnoughKinti()
        {
            if (this.gold >= Constants.Potions.PotionsConstants.MarketPrice)
            {
                Constants.Shop.ShopConstants.lastSuccess = true;

            }
            else
            {
                Constants.Shop.ShopConstants.lastSuccess = false;
            }
            return this.gold >= Constants.Potions.PotionsConstants.MarketPrice;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
