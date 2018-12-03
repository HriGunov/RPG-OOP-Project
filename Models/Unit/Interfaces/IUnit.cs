using Models.Unit.Interfaces.Combat;

namespace Models.Unit.Interfaces
{
    public interface IUnit : IAttributable, IHealth, IEffectable, ILocation, IFigthable, IDescribable
    {
        Map Map { get; set; }

        Symbol Representation { get; set; }
    }
}
