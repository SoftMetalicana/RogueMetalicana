namespace RogueMetalicana.EnemyUnit
{
    using System;
    using RogueMetalicana.Positioning;
    
    public class EnemyEventArgs : EventArgs
    {
        public int ExperienceGained { get; set; }

        public Position Position { get; set; }

        public bool IsAlive { get; set; }
    }
}
