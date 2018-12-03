using System.Collections.Generic;

using Models.Unit.Combat;
using Models.Unit.Combat.Interfaces;

namespace Models.Unit.Interfaces.Combat
{
    public interface IFigthable
    {
        ICollection<IAttack> AvailableAttacks { get; }

        CombatStats CombatombatStats { get; set; }

        void ReciveAtack(double damage);

        bool Dead { get; }
    }
}
