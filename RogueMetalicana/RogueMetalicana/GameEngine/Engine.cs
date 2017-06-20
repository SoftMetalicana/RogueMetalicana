namespace RogueMetalicana.GameEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RogueMetalicana.BattleGround;
    using RogueMetalicana.Constants.Level;
    using RogueMetalicana.Constants.Player;
    using RogueMetalicana.Constants.Visualisator;
    using RogueMetalicana.EnemyUnit;
    using RogueMetalicana.MapPlace;
    using RogueMetalicana.LevelEngine;
    using RogueMetalicana.PlayerUnit;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.Visualization;
    using RogueMetalicana.Constants.Potions;
    using RogueMetalicana.Potion;
   
    using RogueMetalicana.Menu;
    using RogueMetalicana.Constants.Shop; 


    /// <summary>
    /// Packs everything needed in the game into itself.
    /// Takes care of objects interactions.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Those are all the vital units in the game for now.
        /// </summary>
        private Player player;
        private List<Enemy> allEnemies;
        private List<Place> allPlaces;
        
        private List<char[]> dungeon;

        private LevelGenerator levelGenerator;

        /// <summary>
        /// Sets values to the game units.
        /// </summary>
        /// <param name="player">The player that you want to create in the engine.</param>
        /// <param name="allEnemies">The enemies that you want to create in the engine.</param>
        /// <param name="dungeon">The dungeon you want to create in the engine.</param>
        public Engine(Player player, List<Enemy> allEnemies, List<Place> allPlaces, List<char[]> dungeon, LevelGenerator levelGenerator)
        {
            this.player = player;
            this.allEnemies = allEnemies;
            this.allPlaces = allPlaces;
           
          
            this.dungeon = dungeon;

            this.levelGenerator = levelGenerator;
        }

        public List<char[]> Dungeon { get { return this.dungeon; } }

        /// <summary>
        /// This method is executed when the player dies.
        /// </summary>
        /// <param name="sender">The publisher of the event. The player.</param>
        /// <param name="playerEventArgs">Holds the needed things that changed.</param>
        public void OnPlayerDied(object sender, PlayerEventArgs playerEventArgs)
        {
            BattleGround.BattleResult.AppendLine(PlayerConstants.PlayerDiedDueToAttack);
            Visualisator.PrintEndGameMessage(BattleGround.BattleResult.ToString());
        }

        /// <summary>
        /// Every time a new level is generated and the monsters are read the engine subscribes to their event
        /// </summary>
        public void OnLevelGenerated(object sender, LevelGeneratedEventArgs eventArgs)
        {
            foreach (Enemy enemy in allEnemies)
            {
                enemy.EnemyDied += OnEnemyDied;
            }
        }

        /// <summary>
        /// This method is executed everytime the player moves.
        /// </summary>
        /// <param name="sender">The publisher of the event. The player.</param>
        /// <param name="playerEventArgs">Holds the needed things that changed.</param>
        public void OnPlayerMoved(object sender, PlayerEventArgs playerEventArgs)
        {
            Position newPlayerPosition = playerEventArgs.NewPlayerPosition;

            if (PlayerFellOfTheDungeon(newPlayerPosition))
            {
                this.allEnemies = new List<Enemy>();
                this.allPlaces = new List<Place>();
                                      
                this.dungeon = new List<char[]>();

                levelGenerator.GenerateFullLevelPath();
                levelGenerator.GenerateLevel(this.player, this.allEnemies, this.allPlaces,this.dungeon);

                return;
            }

            char newPositionCell = this.GetSymbolFromDungeon(newPlayerPosition);

            //Executes different method depending on the cell that the player wants to step on.
            //EVERYTIME THE PLAYER MOVES SUCCESSFULLY YOU MUST INVOKE THE SWAP SYMBOLS METHOD3
            //You must do it so the dungeon knows where the player is.
            switch (newPositionCell)
            {
                //If he wants to step on the ground we let him.
                case LevelConstants.Ground:
                    PlaceThePlayerOnHisNewPosition(newPlayerPosition);
                    break;

                case LevelConstants.Lava:
                    Visualisator.PrintEndGameMessage(PlayerConstants.SteppedIntoLavaMessage);
                    break;

                case LevelConstants.Wall:
                    break;

                case LevelConstants.Door:
                    TryOpenDoor(newPlayerPosition);
                    break;

                case LevelConstants.SpellboundForest:
                    Visualisator.PrintEndGameMessage(PlayerConstants.LostIntoSpellboundForest);
                    break;

                case LevelConstants.RiverOfMercury:
                    Visualisator.PrintEndGameMessage(PlayerConstants.EnterIntoRiverOfMercury);
                    break;

                case ShopConstants.Symbol:
                    Menu.OpenShop(); break;

                //all the monsters are traversed here.
                default:

                    bool isEnemy = false;
                    bool isShop = false;
                    var enemy = new Enemy();
                    

                    foreach (var thisEnemy in allEnemies)
                    {
                        if ((thisEnemy.Position.Col == newPlayerPosition.Col) &&
                            (thisEnemy.Position.Row == newPlayerPosition.Row))
                        {
                            isEnemy = true;
                            enemy = thisEnemy;
                            break;
                        }
                    }
               
                    if (isEnemy)
                    {
                        EnterInBattle(enemy);
                        break;
                    }
                    else if (isShop)
                    {
                        Menu.OpenShop();
                        break;
                    }
                    else
                    {

                        ConsumePlaceGains(newPlayerPosition);

                        break;
                    }

            }
        }

        private void ConsumePlaceGains(Position newPlayerPosition)
        {
            foreach (var place in allPlaces)
            {
                if (place.Position.Row == newPlayerPosition.Row &&
                    place.Position.Col == newPlayerPosition.Col)
                {
                    if (!place.IsVisited)
                    {
                        var gain = place.Gain;
                        var gainType = string.Empty;
                        double gainValue = place.Value;

                        switch (gain)
                        {
                            case Place.PlaceGain.Health:

                                if (this.player.Health + place.Value <= Player.MaxHealth)
                                {
                                    this.player.Health += place.Value;
                                }
                                else
                                {
                                    gainValue = Player.MaxHealth - this.player.Health;
                                    this.player.Health = Player.MaxHealth;

                                }

                                gainType = Enum.GetName(typeof(Place.PlaceGain), Place.PlaceGain.Health);
                                break;

                            case Place.PlaceGain.Armor:
                                this.player.Defense += place.Value;
                                gainType = Enum.GetName(typeof(Place.PlaceGain), Place.PlaceGain.Armor);
                                break;

                            case Place.PlaceGain.Experience:
/*                                this.player.Experience += place.Value;*/
                                this.player.GainGoldAndExperience(place.Value, 0);
                                gainType = Enum.GetName(typeof(Place.PlaceGain), Place.PlaceGain.Experience);
                                break;

                            case Place.PlaceGain.Gold:
/*                                this.player.Gold += place.Value;*/
                                this.player.GainGoldAndExperience(0, place.Value);
                                gainType = Enum.GetName(typeof(Place.PlaceGain), Place.PlaceGain.Gold);
                                break;
                        }

                        Visualisator.PrintUnderTheBattleField(VisualisatorConstants.PlaceGainConsumed + place.Type + " " + gainValue + " " + gainType);
                        place.IsVisited = true;
                    }
                    else
                    {
                        Visualisator.PrintUnderTheBattleField(VisualisatorConstants.PlaceAlreadyVisited);
                    }

                }
            }
        }

        /// <summary>
        /// Open door to the boss and goes to next level.
        /// </summary>
        /// <param name="newPlayerPosition"></param>
        private void TryOpenDoor(Position newPlayerPosition)
        {

            if (allEnemies.Count <= 1)
            {
                PlaceThePlayerOnHisNewPosition(newPlayerPosition);
            }
            else
            {
                Visualisator.PrintUnderTheBattleField(VisualisatorConstants.UnableToCompleteLevel);
            }

            //dinamichno vzemane na levela za zavurshvane na nivoto.

/*            if (player.Level >= 6)
            {
                PlaceThePlayerOnHisNewPosition(newPlayerPosition);
            }
            else
            {
                //print message why can't open the door.
            }*/
        }

        /// <summary>
        /// Invokes when enemy die to gain expereince, gold and remove enemy from enemies list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="enemyEventArgs"></param>
        public void OnEnemyDied(object sender, EnemyEventArgs enemyEventArgs)
        {
            BattleGround.BattleResult.AppendLine($"{enemyEventArgs.EnemyType} has been defeated by Player.");
            BattleGround.BattleResult.AppendLine($"Player won {enemyEventArgs.ExperienceGained} experience");
            BattleGround.BattleResult.AppendLine($"Player won {enemyEventArgs.GoldGained} gold");
            var potionGenerated = PotionGenerator.GeneratePotion();
            if (potionGenerated.PotionType!=PotionType.None)
            {
                switch (potionGenerated.PotionType)
                {
                    case PotionType.HealthPotion:
                BattleGround.BattleResult.AppendLine($"Player obtained {potionGenerated.PotionType.ToString()}({potionGenerated.HealthBonus} health)");
                        break;
                    case PotionType.XpPotion:
                BattleGround.BattleResult.AppendLine($"Player obtained {potionGenerated.PotionType.ToString()}({potionGenerated.XpBonus} xp bonus)");
                        break;
                    case PotionType.BonusDamagePotion:
                BattleGround.BattleResult.AppendLine($"Player obtained {potionGenerated.PotionType.ToString()}({potionGenerated.DamageBonus}) damage bonus)");
                        break;
                    default:
                        break;
                }
                this.player.RecievePotion(potionGenerated);
            }
            this.player.GainGoldAndExperience(enemyEventArgs.ExperienceGained, enemyEventArgs.GoldGained);
            this.allEnemies = allEnemies.Where(e => e.IsAlive).ToList();
            this.dungeon[enemyEventArgs.Position.Row][enemyEventArgs.Position.Col] = ' ';
        }

        /// <summary>
        /// GenerateStats battle screen and start battle.
        /// </summary>
        /// <param name="enemy"></param>
        private void EnterInBattle(Enemy enemy)
        {
            BattleGround.BattleResult.Clear();
            BattleGround.GenerateStats(player, enemy);
            Visualisator.PrintBattleGround(this.dungeon, this.player, BattleGround.BattleResult);
        }

        /// <summary>
        /// Places the player on his new position.
        /// INVOKES a method which takes care of the symbol placement on the console.
        /// The invoked method swaps the old symbol position and the new symbol position.
        /// </summary>
        /// <param name="newPlayerPosition">The new position of the player.</param>
        private void PlaceThePlayerOnHisNewPosition(Position newPlayerPosition)
        {
            this.SwapSymbolsInDungeon(this.player.Position, newPlayerPosition);

            Visualisator.DeleteSymbolOnPositionAndPrintNewOne(' ', this.player.Position, PlayerConstants.Color);
            Visualisator.DeleteSymbolOnPositionAndPrintNewOne(PlayerConstants.Symbol, newPlayerPosition, PlayerConstants.Color);

            this.player.Position = newPlayerPosition;

            Visualisator.PrintPlayerStats(this.player);
        }

        /// <summary>
        /// Gets the symbol from a cell from the dungeon.
        /// </summary>
        /// <param name="toGetFrom">The row and column of the symbol that you want to get.</param>
        /// <returns>The symbol at the given position.</returns>
        private char GetSymbolFromDungeon(Position toGetFrom)
        {
            return this.dungeon[toGetFrom.Row][toGetFrom.Col];
        }

        /// <summary>
        /// Swaps two symbols places in the dungeon.
        /// </summary>
        /// <param name="firstPosition">The symbol that you want to swap.</param>
        /// <param name="secondPosition">The symbol that you want to swap it with.</param>
        private void SwapSymbolsInDungeon(Position firstPosition, Position secondPosition)
        {
            char symbolHolder = this.dungeon[firstPosition.Row][firstPosition.Col];
            this.dungeon[firstPosition.Row][firstPosition.Col] = this.dungeon[secondPosition.Row][secondPosition.Col];
            this.dungeon[secondPosition.Row][secondPosition.Col] = symbolHolder;
        }

        /// <summary>
        /// Returns true if the players positions is ouside the bounds of the dungeon.
        /// </summary>
        /// <param name="newPlayerPosition">The new position you want to check.</param>
        /// <returns>True if player fell of the dungeon.</returns>
        private bool PlayerFellOfTheDungeon(Position newPlayerPosition)
        {
            int rowToCheck = newPlayerPosition.Row;
            int colToCheck = newPlayerPosition.Col;

            return rowToCheck < 0 || rowToCheck >= this.dungeon.Count ||
                   colToCheck < 0 || colToCheck >= this.dungeon[rowToCheck].Length;
        }
    }
}
