using Models.Unit.Combat.Interfaces;

namespace Models.Unit.Combat
{
    public class CombatStats : ICombatStats
    {
        public CombatStats()
        {
            this.BaseAtackLowBoundary = 10;
            this.BaseAtackHighBoundary = 25;
            this.BaseArmour = 5;
            this.BaseDodge = 45;
            this.BaseHitRating = 50;
        }

        public CombatStats(int baseAtackLowBoundary, int baseAtackHighBoundary, int baseArmour, int baseDodge, int baseHitRating)
        {
            this.BaseAtackLowBoundary = baseAtackLowBoundary;
            this.BaseAtackHighBoundary = baseAtackHighBoundary;
            this.BaseArmour = baseArmour;
            this.BaseDodge = baseDodge;
            this.BaseHitRating = baseHitRating;
        }

        private int BaseAtackLowBoundary { get; set; }
        private int BaseAtackHighBoundary { get; set; }

        private int BaseArmour { get; set; }
        private int BaseDodge { get; set; }
        private int BaseHitRating { get; set; }

        public int DeltaAtackLowBoundary { get; set; }
        public int DeltaAtackHightBoundary { get; set; }

        public int DeltaArmour { get; set; }
        public int DeltaDodge { get; set; }
        public int DeltaHitRating { get; set; }

        public virtual double GetAtackLowBoundary()
        {
            return BaseAtackLowBoundary + DeltaAtackLowBoundary;
        }

        public virtual double GetAtackHighBoundary()
        {
            return BaseAtackHighBoundary + DeltaAtackHightBoundary;
        }

        public virtual double GetArmour()
        {
            return BaseArmour + DeltaArmour;
        }

        public virtual double GetDodge()
        {
            return BaseDodge + DeltaDodge;
        }

        public double GetAtackRating()
        {
            return BaseHitRating + DeltaHitRating;
        }
    }
}
