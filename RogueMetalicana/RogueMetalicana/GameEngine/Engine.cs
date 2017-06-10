using System;

namespace RogueMetalicana.GameEngine
{
    using RogueMetalicana.BattleGround;
    using RogueMetalicana.Constants.Level;
    using RogueMetalicana.Constants.Player;
    using RogueMetalicana.EnemyUnit;
    using RogueMetalicana.LevelEngine;
    using RogueMetalicana.PlayerUnit;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.Visualization;
    using System.Collections.Generic;

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

        private List<char[]> dungeon;

        private LevelGenerator levelGenerator;

        /// <summary>
        /// Sets values to the game units.
        /// </summary>
        /// <param name="player">The player that you want to create in the engine.</param>
        /// <param name="allEnemies">The enemies that you want to create in the engine.</param>
        /// <param name="dungeon">The dungeon you want to create in the engine.</param>
        public Engine(Player player, List<Enemy> allEnemies, List<char[]> dungeon, LevelGenerator levelGenerator)
        {
            this.player = player;
            this.allEnemies = allEnemies;

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
            Visualisator.PrintEndGameMessage(PlayerConstants.PlayerDiedDueToAttack);
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
                this.dungeon = new List<char[]>();
                
                levelGenerator.GenerateFullLevelPath();
                levelGenerator.GenerateLevel(this.player, this.allEnemies, this.dungeon);

                return;
            }

            char newPositionCell = this.dungeon[newPlayerPosition.Row][newPlayerPosition.Col];

            //Executes different method depending on the cell that the player wants to step on.
            //EVERYTIME THE PLAYER MOVES SUCCESSFULLY YOU MUST INVOKE THE SWAP SYMBOLS METHOD3
            //You must do it so the dungeon knows where the player is.
            switch (newPositionCell)
            {
                //If he wants to step on the ground we let him.
                case LevelConstants.Ground:
                case PlayerConstants.Symbol:
                    PlaceThePlayerOnHisNewPosition(newPlayerPosition);
                    break;

                case LevelConstants.Lava:
                    Visualisator.PrintEndGameMessage(PlayerConstants.SteppedIntoLavaMessage);
                    break;

                case LevelConstants.Wall:
                    break;

                case LevelConstants.SpellboundForest:
                    Visualisator.PrintEndGameMessage(PlayerConstants.LostIntoSpellboundForest);
                    break;

                //all the monsters are traversed here.
                default:
                    foreach (var enemy in allEnemies)
                    {
                        //temp - to be developed once battle is possible
                        if ((enemy.Position.Col == newPlayerPosition.Col) && (enemy.Position.Row == newPlayerPosition.Row))
                        {
                            EnterInBattle(enemy);
                            Visualisator.PrintEndGameMessage($"You just encountered a fat ugly {enemy.Type}");
                        }
                    }
                    break;
            }
        }

        private void EnterInBattle(Enemy enemy)
        {
            BattleGround.Generate(player, enemy);
        }

        /// <summary>
        /// Places the player on his new position.
        /// INVOKES a method which takes care of the symbol placement on the console.
        /// The invoked method swaps the old symbol position and the new symbol position.
        /// </summary>
        /// <param name="newPlayerPosition">The new position of the player.</param>
        private void PlaceThePlayerOnHisNewPosition(Position newPlayerPosition)
        {
            Visualisator.DeleteSymbolOnPositionAndPrintNewOne(' ', this.player.Position, PlayerConstants.Color);
            Visualisator.DeleteSymbolOnPositionAndPrintNewOne(PlayerConstants.Symbol, newPlayerPosition, PlayerConstants.Color);

            this.player.Position = newPlayerPosition;

            Visualisator.PrintPlayerStats(this.player);
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
