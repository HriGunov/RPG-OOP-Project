using Models.Unit.Interfaces;

namespace Models.Effects.Interfaces
{
    public interface ITimedUnitEffect : IUnitEffect
    {
        int Duration { get; set; }

        void OnDurationEnd(IUnit affects);
    }
}
