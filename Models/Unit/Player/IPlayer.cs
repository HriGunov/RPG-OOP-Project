using Models.Unit.Interfaces;
using Models.Unit.Profession.Interfaces;

namespace Models.Unit.Player
{
    public interface IPlayer : IUnit
    {
        IProfession Profession { get; set; }
    }
}
