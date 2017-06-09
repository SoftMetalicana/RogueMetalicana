namespace RogueMetalicana.UnitsInterfaces
{
    /// <summary>
    /// For the units that are going to battle.
    /// </summary>
    public interface IFightable
    {
        double Health { get; set; }
        int Defense { get; set; }
        int Damage { get; set; }

        bool IsAlive { get; set; }

        void TakeDamage(double damageToTake);
    }
}
