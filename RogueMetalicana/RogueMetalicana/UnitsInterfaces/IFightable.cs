namespace RogueMetalicana.UnitsInterfaces
{
    /// <summary>
    /// For the units that are going to battle.
    /// </summary>
    public interface IFightable
    {
        int Health { get; set; }
        int Armor { get; set; }
        int Damage { get; set; }

        bool IsAlive { get; set; }

        void TakeDamage(int damageToTake);
    }
}
