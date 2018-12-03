using Models.Unit.Interfaces;

namespace Models.Unit
{
    public class Attributes : IAttributes
    {
        public Attributes(int strength = 5, int perception = 5, int endurance = 5, int agility = 5)
        {
            Strength = strength;
            Perception = perception;
            Endurance = endurance;
            Agility = agility;
        }

        public int Strength { get; set; }

        public int Perception { get; set; }

        public int Endurance { get; set; }

        public int Agility { get; set; }
    }
}
