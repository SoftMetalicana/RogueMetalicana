namespace RogueMetalicana.Constants.Position
{
    using RogueMetalicana.Positioning;
    using System.Collections.Generic;

    /// <summary>
    /// Provides variables for the Position.cs
    /// </summary>
    public static class PositionConstants
    {
        /// <summary>
        /// If you want to move in a certain direction you need to extract the new position by using a key direction.
        /// MUST NOT BE ACCESSED FROM HERE. IF YOU WANT TO GET A NEW POSITION USE THE POSITION.CS!!!
        /// </summary>
        public static readonly Dictionary<Direction, Position> DirectionPositions = new Dictionary<Direction, Position>
        {
            [Direction.Up] = new Position(-1, 0),
            [Direction.Down] = new Position(+1, 0),
            [Direction.Left] = new Position(0, -1),
            [Direction.Right] = new Position(0, +1),
        };
    }

    /// <summary>
    /// Enumeration for the wanted direction you want to go to.
    /// </summary>
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
