using System;
using System.Collections.Generic;
using System.Linq;

using Models.Interfaces;
using Models.Unit.Enemies.Interfaces;

namespace Core.Extension
{
    public static class EnemyExtensions
    {
        public static void UpdatePath(this IEnemy enemyUnit, ICoordinates targetCoords, IMathFunctions math)
        {
            int visionRange = enemyUnit.GetVisionRange();

            var lineOfSight = enemyUnit.GetLineTo(targetCoords, math).Take(visionRange);

            if (enemyUnit.CanSee(lineOfSight))
            {
                enemyUnit.UpdatePath(lineOfSight.ToArray());
            }
        }

        public static void UpdatePath(this IEnemy enemyUnit, ICollection<ICoordinates> path)
        {
            if (path == null)
            {
                throw new NullReferenceException();
            }
            enemyUnit.MovementQueue = new Queue<ICoordinates>(path);
        }
    }
}
