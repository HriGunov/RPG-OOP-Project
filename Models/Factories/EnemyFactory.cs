using System;

using Models.Unit;
using Models.Unit.Enemies.Interfaces;

namespace Models.Factories
{
    public static class EnemyFactory
    {
        public static IEnemy Create(char key, UnitPostion position)
        {
            Type[] constructorParams = new Type[] { typeof(UnitPostion), typeof(Map) };

            return (IEnemy)Register.RegisteredEnemies[key]
                .GetConstructor(constructorParams)
                .Invoke(new object[] { position, null });
        }
    }
}
