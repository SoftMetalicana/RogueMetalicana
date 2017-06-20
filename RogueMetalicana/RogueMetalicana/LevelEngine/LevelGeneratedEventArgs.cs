using RogueMetalicana.PlayerUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RogueMetalicana.LevelEngine
{
    public class LevelGeneratedEventArgs : EventArgs
    {
        public Player Player { get; set; }
    }
}
