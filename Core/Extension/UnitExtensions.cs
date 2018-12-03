using Models.Interfaces;
using Models.Unit.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Extension
{
    public static class UnitExtensions
    {
        public static int GetVisionRange(this IUnit unit)
        {
            int range = (15 + (unit.Attributes.Perception / 2)) / 2;
            return range;
        }

        public static ICollection<ICoordinates> GetLineTo(this IUnit unit, ICoordinates targetCoords, IMathFunctions math)
        {
            var rayTracingPath = math.LineAlgorithm(unit.Location.CordY, unit.Location.CordX,
                targetCoords.CordY, targetCoords.CordX);
            
            //makes sure we start from the players location
            if (rayTracingPath[0].CordX != unit.Location.CordX || rayTracingPath[0].CordY != unit.Location.CordY)
            {
                rayTracingPath.Reverse();
            }

            return rayTracingPath.Skip(1).ToArray();
        }

        public static bool CanSee(this IUnit unit, IEnumerable<ICoordinates> lineOfSight)
        {
            foreach (var step in lineOfSight)
            {
                //Makes sure when don't try to access an out of range position
                if (!(step.CordY < 0 || step.CordX < 0 || step.CordY >= unit.Map.Height ||
                      step.CordX >= unit.Map.Width))
                {
                    if (unit.Map[step.CordY, step.CordX].Tile.BlocksSight == true)
                    {
                        //shows which tiles block sight
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CanSee(this IUnit unit, ICoordinates targetCoords, IMathFunctions math)
        {
            int visionRange = unit.GetVisionRange();
            if (unit.Location.DistanceTo(targetCoords) > visionRange)
            {
                return false;
            }
            var lineOfSight = unit.GetLineTo(targetCoords, math).Take(visionRange);

            return unit.CanSee(lineOfSight);
        }

        public static void RegenarateHP(this IUnit unit)
        {
            unit.CurrentHealth += unit.HealthRegenPerTurn;
            if (unit.CurrentHealth > unit.MaximumHealth)
            {
                unit.CurrentHealth = unit.MaximumHealth;
            }
        }
    }
}


