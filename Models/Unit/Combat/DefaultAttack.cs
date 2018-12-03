using Models.Effects;
using Models.Effects.Interfaces;
using Models.Unit.Combat.Interfaces;
using Models.Unit.Interfaces;

namespace Models.Unit.Combat
{
    public class DefaultAttack : IAttack
    {
        public double AttackBonusLowBoundary { get; }
        public double AttackDifficulty { get; set; }
        public double AttackBonusHighBoundary { get; }

        //Some magic Number as of now
        //Higher meeans Higher 
        public int AttackFrequency { get; set; }

        public ITimedUnitEffect onHitEffect;

        public DefaultAttack() : this(5, 5, 1, 50, new DafaultTimedEffect(), "hits")
        {
            AttackBonusLowBoundary = 5;
            AttackBonusHighBoundary = 5;
            AttackDifficulty = 1;
            AttackFrequency = 50;
        }

        public DefaultAttack(double attackBonusLowBoundary, double attackBonusHighBoundary, double attackDifficulty,
            int attackFrequency, ITimedUnitEffect onHitEffect, string attackVerb)
        {
            this.AttackBonusLowBoundary = attackBonusLowBoundary;
            this.AttackBonusHighBoundary = attackBonusHighBoundary;
            this.AttackFrequency = attackFrequency;
            this.onHitEffect = onHitEffect;
            this.AttackVerb = attackVerb;
            this.AttackDifficulty = attackDifficulty;
        }

        public string AttackVerb { get; set; }

        public void AttackUnit(IUnit target, double damage)
        {
            AffectUnit(target, onHitEffect);
            target.ReciveAtack(damage);
        }

        public void AffectUnit(IUnit unit, ITimedUnitEffect onHitEffect)
        {
            unit.Effects.AddEffect(onHitEffect);
        }
    }
}
