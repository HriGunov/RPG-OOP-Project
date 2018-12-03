using Models.Effects.Interfaces;
using Models.Unit.Interfaces;

namespace Models.Effects
{
    public class DafaultTimedEffect : ITimedUnitEffect
    {
        private int duration;

        public DafaultTimedEffect()
        {
            Duration = 0;
        }

        public void ActivateEffect(IUnit unit)
        {

        }

        public void UnactivateEffect(IUnit unit)
        {

        }

        public int Duration
        {
            get => duration;
            set => duration = value;
        }

        public void OnDurationEnd(IUnit affects)
        {

        }
    }
}
