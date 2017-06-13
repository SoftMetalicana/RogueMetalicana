namespace RogueMetalicana.MapPlace
{
    using RogueMetalicana.Positioning;
    using RogueMetalicana.UnitsInterfaces;

    public class Place : IPositionable
    {

        public enum PlaceGain
        {
            Health,
            Armor,
            Experience,
            Gold
        }

        private Position position;
        private string type;
        private PlaceGain gain;
        private int value;
        private bool isVisited;

        public Place(string type, PlaceGain gain, int value, Position position)
        {
            this.type = type;
            this.gain = gain;
            this.value = value;
            this.position = position;
            this.isVisited = false;
        }

        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public string Type
        {
            get { return this.type; }
            set { this.type = value; }
        }

        public PlaceGain Gain
        {
            get { return this.gain; }
            set { this.gain = value; }
        }

        public int Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public bool IsVisited
        {
            get { return this.isVisited; }
            set { this.isVisited = value; }
        }
    }
}