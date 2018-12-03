using System;
using System.Collections.Generic;
using System.Linq;
using Models.Tile.Interfaces;
using Models.Unit.Enemies.Monster;

namespace Models.Factories
{
    public static class Register
    {
        public static Dictionary<char, Type> RegisteredTiles = new Dictionary<char, Type>();
        public static Dictionary<char, Type> RegisteredEnemies = new Dictionary<char, Type>();

        public static void Populate()
        {
            GetTilesInModels();
            GetEnemiesInModels();
        }

        public static void GetEnemiesInModels()
        {
            var enemies = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(EnemyMonster).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (Type enemy in enemies)
            {
                var field = enemy.GetField("registrationKey");
                char key = (char)field.GetValue(null);
                RegisteredEnemies.Add(key, enemy);
            }
        }

        public static void GetTilesInModels()
        {
            var tiles = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(ITile).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (Type tile in tiles)
            {
                var field = tile.GetField("registrationKey");
                char key = (char)field.GetValue(null);
                RegisteredTiles.Add(key, tile);
            }
        }
    }
}
