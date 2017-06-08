namespace RogueMetalicana.Positioning
{
    using System.Collections.Generic;
    using RogueMetalicana.Constants.Position;
    
    /// <summary>
    /// Class used to locate a cell on the console.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Provides a new position for the direction that you want to go to.
        /// </summary>
        public static readonly Dictionary<Direction, Position> DirectionPositions = 
                                    PositionConstants.DirectionPositions;

        /// <summary>
        /// Row and column variables to locate the cell on the console.
        /// CAN'T AND MUSTN'T BE ACCESSED OUTSIDE THIS CLASS DIRECTLY!
        /// </summary>
        private int row;
        private int col;

        /// <summary>
        /// Sets the row and column variables to the values you want.
        /// </summary>
        /// <param name="row">The row of the cell you want on the console.</param>
        /// <param name="col">The column of the cell you want on the console.</param>
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        /// <summary>
        /// Property which allows you to get and set the row variable outside of this class.
        /// </summary>
        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        /// <summary>
        /// Property which allows you to get and set the column variable outside of this class.
        /// </summary>
        public int Col
        {
            get { return this.col; }
            set { this.col = value; }
        }
    }
}
