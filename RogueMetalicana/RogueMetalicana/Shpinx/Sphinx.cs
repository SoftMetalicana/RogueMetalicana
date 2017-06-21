using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueMetalicana.UnitsInterfaces;
using RogueMetalicana.Positioning;

namespace RogueMetalicana.Shpinx
{
    class Sphinx:IPositionable

    {
        private Position position;
        private bool isVisited;


        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        public bool IsVisited
        {
            get { return this.isVisited; }
            set { this.isVisited = value; }
        }
       
      
    }
}
