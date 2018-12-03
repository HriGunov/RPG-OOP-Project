using Models.Factories;
using Models.Tile.Interfaces;
using Models.Unit;
using Models.Unit.Enemies.Interfaces;

namespace Core.GetMap
{
    public class ModelFactory
    {
        public static ITile ParseTile(char ch)
        {
            return TileFactory.Create(ch);
        }

        public static IEnemy ParseEnemy(char ch, UnitPostion loc)
        {
            return EnemyFactory.Create(ch, loc);
        }
    }
}
