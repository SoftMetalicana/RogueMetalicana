namespace RogueMetalicana.UnitsInterfaces
{
    using RogueMetalicana.Positioning;

    /// <summary>
    /// All units on the console must inherit that interface.
    /// It provides a position property for them.
    /// </summary>
    public interface IPositionable
    {
        /// <summary>
        /// Represents a position on the console for the unit.
        /// </summary>
        Position Position { get; set; }
    }
}
