using Models.Unit.Enemies.Monster.Interfaces;
using OpenTK.Graphics;

namespace Models.Unit.Enemies.Monster
{
    public class EnemyOrc : EnemyMonster, IEnemyMonster
    {
        public static char registrationKey = 'o';

        public EnemyOrc(UnitPostion location, Map map) 
            : base(location, map)
        {
            this.Name = "Orc";
            // this.AttackDescription = "Bangs";
            this.Armour = 2;
            this.Representation = new Symbol('O', Color4.DarkGreen);
            // this.AttackBonus += 15;
        }
    }
}
