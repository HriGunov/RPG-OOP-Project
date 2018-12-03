using System.Collections.Generic;

using Models.Interfaces;
using Models.Unit.Enemies.Interfaces;

namespace Models.Unit.Enemies
{
    public abstract class Enemy : AbstractUnit, IEnemy
    {
        private Queue<ICoordinates> movementQueue = new Queue<ICoordinates>();

        public Enemy(UnitPostion location, Map map) : base(location, map)
        {

        }

        public Queue<ICoordinates> MovementQueue
        {
            get { return this.movementQueue; }
            set { this.movementQueue = value; }
        }
    }
}
