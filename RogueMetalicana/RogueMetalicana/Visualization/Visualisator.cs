namespace RogueMetalicana.Visualization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    
    public static class Visualisator
    {
        public static void PrintDungeon(IEnumerable<string> dungeon)
        {
            Console.Clear();

            StringBuilder result = new StringBuilder();
            foreach (string line in dungeon)
            {
                result.AppendLine(line);
            }

            Console.Write(result);
        }
    }
}
