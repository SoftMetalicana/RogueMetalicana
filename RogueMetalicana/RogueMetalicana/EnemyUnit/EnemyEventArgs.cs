using System;
using RogueMetalicana.Positioning;

namespace RogueMetalicana.EnemyUnit
{
    public class EnemyEventArgs : EventArgs
    {
        public int ExperienceGained { get; set; }

        public Position Position { get; set; }

        public bool IsAlive { get; set; }
    }
}
