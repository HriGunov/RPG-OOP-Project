using System.Collections.Generic;

using Models.Interfaces;
using Models.Unit.Interfaces;

namespace Models.Unit.Enemies.Interfaces
{
    public interface IEnemy : IUnit
    {
        Queue<ICoordinates> MovementQueue { get; set; }
    }
}
