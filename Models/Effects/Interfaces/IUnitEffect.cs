using Models.Unit.Interfaces;

namespace Models.Effects.Interfaces
{
    public interface IUnitEffect
    {
        void ActivateEffect(IUnit unit);

        void UnactivateEffect(IUnit unit);
    }
}
