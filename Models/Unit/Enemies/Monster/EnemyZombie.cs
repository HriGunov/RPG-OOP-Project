namespace Models.Unit.Enemies.Monster
{
    using Models.Unit.Enemies.Monster.Interfaces;
    using OpenTK.Graphics;

    public class EnemyZombie : EnemyMonster, IEnemyMonster
    {
        public static char registrationKey = 'z';

        public EnemyZombie(UnitPostion location, Map map)
            : base(location, map)
        {
            this.Name = "Zombie";
            // this.AttackDescription = "Bites";
            this.Armour = 3;
            this.Representation = new Symbol('Z', Color4.YellowGreen);
            // this.AttackBonus += 20;
        }
    }
}
