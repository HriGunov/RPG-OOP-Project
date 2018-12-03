using System.Collections.Generic;

namespace Models.Unit.Interfaces.Movement
{
    interface IMoves : ILocation
    {
        Queue<Position> MovementQueue { get; set; }

        void Move();
    }
}
