namespace RogueMetalicana.EnemyUnit
{
    using System;
    using RogueMetalicana.Positioning;
    
    public class EnemyEventArgs : EventArgs
    {
        public string EnemyType { get; set; }

        public int ExperienceGained { get; set; }

        public int GoldGained { get; set; }

        public Position Position { get; set; }

        public bool IsAlive { get; set; }
    }
}
