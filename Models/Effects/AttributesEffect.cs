using Models.Effects.Interfaces;
using Models.Unit;
using Models.Unit.Interfaces;

namespace Models.Effects
{
    public class AttributesEffect : IUnitEffect
    {
        private readonly Attributes deltaAttributes;

        public AttributesEffect(Attributes deltaAttributes)
        {
            this.deltaAttributes = deltaAttributes;
        }

        public void ActivateEffect(IUnit unit)
        {
            unit.Attributes = new Attributes(
                unit.Attributes.Strength + deltaAttributes.Strength,
                unit.Attributes.Perception + deltaAttributes.Perception,
                unit.Attributes.Endurance + deltaAttributes.Endurance,
                unit.Attributes.Agility + deltaAttributes.Agility
            );
        }

        public void UnactivateEffect(IUnit unit)
        {
            unit.Attributes = new Attributes(
                unit.Attributes.Strength - deltaAttributes.Strength,
                unit.Attributes.Perception - deltaAttributes.Perception,
                unit.Attributes.Endurance - deltaAttributes.Endurance,
                unit.Attributes.Agility - deltaAttributes.Agility
            );
        }
    }
}
