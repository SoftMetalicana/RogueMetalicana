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
        private bool isVisited =false;
        private int bonusGold;
        private int minusHealth;
        public Sphinx(Position position,int bonusGold,int minusHealth)
        {
            this.position = position;
            this.bonusGold = bonusGold;
            this.minusHealth = minusHealth;
        }
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
        public int BonusGold
        {
            get { return this.bonusGold; }
            set { this.bonusGold = value; }
        }
       public int MinusHealth
        {
            get { return this.minusHealth; }
            set { this.minusHealth = value; }
        }
      
    }
}
