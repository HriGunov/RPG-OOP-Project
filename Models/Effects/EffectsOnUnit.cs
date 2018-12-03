using System.Collections.Generic;
using Models.Effects.Interfaces;
using Models.Interfaces;
using Models.Unit.Interfaces;

namespace Models.Effects
{
    public class EffectsOnUnit : ITick
    {
        private IUnit effectedUnit;
        private List<IUnitEffect> effects = new List<IUnitEffect>();
        private List<ITimedUnitEffect> timeTrackedEffects = new List<ITimedUnitEffect>();

        public EffectsOnUnit(IUnit unit)
        {
            EffectedUnit = unit;
        }
        public void AddEffect(ITimedUnitEffect timedEffect)
        {
            if (timedEffect.Duration > 0)
            {
                timeTrackedEffects.Add(timedEffect);
                AddEffect((IUnitEffect)timedEffect);
            }
        }

        public void AddEffect(IUnitEffect effect)
        {
            effect.ActivateEffect(EffectedUnit);
            effects.Add(effect);
        }

        public void RemoveEffect(IUnitEffect effect)
        {
            effect.UnactivateEffect(EffectedUnit);
            effects.Remove(effect);
        }

        public List<IUnitEffect> GetEffects()
        {
            return new List<IUnitEffect>(effects);
        }

        public IUnit EffectedUnit
        {
            get => effectedUnit;
            set => effectedUnit = value;
        }

        public void Tick()
        {
            //Subracts 1 from the duration of timed events and if duration is 0 removes it from active effects 
            foreach (var timedEffect in timeTrackedEffects)
            {
                timedEffect.Duration -= 1;
                if (timedEffect.Duration == 0)
                {
                    effects.Remove(timedEffect);
                    timedEffect.OnDurationEnd(effectedUnit);
                }
            }
        }
    }
}
