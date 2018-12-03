namespace Models.Unit.Combat.Interfaces
{
    public interface ICombatStats
    {
        int DeltaArmour { get; set; }
        int DeltaAtackHightBoundary { get; set; }
        int DeltaAtackLowBoundary { get; set; }
        int DeltaDodge { get; set; }
        int DeltaHitRating { get; set; }

        double GetArmour();
        double GetAtackHighBoundary();
        double GetAtackLowBoundary();
        double GetAtackRating();
        double GetDodge();
    }
}