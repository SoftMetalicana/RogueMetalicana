namespace RogueMetalicana.PlayerUnit
{
    using System;
    using RogueMetalicana.Positioning;

    public class PlayerEventArgs : EventArgs
    {
        public Position NewPlayerPosition { get; set; }
    }
}
