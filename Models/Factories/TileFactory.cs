using System;
using Models.Tile.Interfaces;

namespace Models.Factories
{
    public static class TileFactory
    {
        public static ITile Create(char key)
        {
            return (ITile)Register.RegisteredTiles[key].GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
        }
    }
}
