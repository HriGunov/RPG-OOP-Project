using System.Collections.Generic;

using Models.Effects;
using Models.Interfaces;
using Models.Unit.Enemies.Interfaces;
using Models.Unit.Enemies.Monster.Interfaces;

namespace Models.Unit.Enemies.Monster
{
    public abstract class EnemyMonster : AbstractUnit, IEnemyMonster, IEnemy
    {
        public static char registrationKey = '%';
        public EnemyMonster(UnitPostion location, Map map)
            : base(location, map)
        {
            var attributes = new Attributes(6, 6, 6, 6);
            this.Effects.AddEffect(new AttributesEffect(attributes));
            Armour = 2;
        }

        public Queue<ICoordinates> MovementQueue { get; set; }
    }
}
