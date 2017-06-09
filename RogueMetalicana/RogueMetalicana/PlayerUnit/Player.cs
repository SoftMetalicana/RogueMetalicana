namespace RogueMetalicana.PlayerUnit
{
    using System;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;
    using RogueMetalicana.Constants.Player;
    using RogueMetalicana.Constants.Position;

    /// <summary>
    /// This represents the player in the game.
    /// </summary>
    public class Player : IPositionable, IFightable
    {
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

            ConsoleKeyInfo pressedKey = Console.ReadKey(false);
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

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
