using Models.Unit.Enemies.Animals.Interfaces;
using Models.Unit.Enemies.Interfaces;
using OpenTK.Graphics;

namespace Models.Unit.Enemies.Animals
{
    public class EnemyDog : EnemyAnimal, IEnemyAnimal, IEnemy
    {
        public static char registrationKey = 'd';

        public EnemyDog(UnitPostion location, Map map) : base(location, map)
        {
            this.Name = "Dog";
            // this.AttackDescription = "Bites";
            this.Armour = 1;
            this.Representation = new Symbol('D', Color4.Red);
            // this.AttackBonus += 10;
        }
    }
}
