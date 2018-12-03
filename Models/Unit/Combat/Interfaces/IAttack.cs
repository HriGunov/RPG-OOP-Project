using Models.Unit.Interfaces;

namespace Models.Unit.Combat.Interfaces
{
    public interface IAttack
    {
        double AttackBonusHighBoundary { get; }
        double AttackBonusLowBoundary { get; }
        double AttackDifficulty { get; }
        string AttackVerb { get; set; }
        int AttackFrequency { get; }

        void AttackUnit(IUnit target, double damage);
    }
}