namespace RogueMetalicana.EnemyUnit
{
    using System;
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;

    /// <summary>
    /// Represents a single enemy in the dungeon
    /// </summary>
    public class Enemy : IPositionable, IFightable
    {
        private char name;
        private int experienceToGive;

        public Position Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Armor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Damage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsAlive { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void TakeDamage(int damageToTake)
        {
            throw new NotImplementedException();
        }
    }
}
