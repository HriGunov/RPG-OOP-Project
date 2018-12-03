using System.Collections.Generic;

using Models.Unit.Enemies.Monster;
using Models.Effects;
using Models.Interfaces;
using Models.Unit.Enemies.Animals.Interfaces;
using Models.Unit.Enemies.Interfaces;

namespace Models.Unit.Enemies.Animals
{
    public abstract class EnemyAnimal : EnemyMonster, IEnemyAnimal, IEnemy
    {
        private int trackingDistance;

        public EnemyAnimal(UnitPostion location, Map map) : base(location, map)
        {
            var changeInAtribues = new Attributes(0,2,0,4);
            this.Effects.AddEffect(new AttributesEffect(changeInAtribues));
            Armour = 0;         
        }

        public int TrackingDistance
        {
            get => trackingDistance;
            set => trackingDistance = value;
        }

        public Queue<ICoordinates> MovementQueue { get; set; }
    }
}
