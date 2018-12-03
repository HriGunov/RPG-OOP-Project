using Models.Unit.Profession;
using Models.Unit.Profession.Interfaces;
using OpenTK.Graphics;

namespace Models.Unit.Player
{
    public class Player : AbstractUnit, IPlayer
    {
        private IProfession profession;

        public Player(UnitPostion location, Map map) : this(location, map, new Warrior())
        {

        }

        public Player(UnitPostion location, Map map, IProfession profession) : base(location, map)
        {
            Name = "Player";
            Profession = profession;
            MaximumHealth = 150;
            CurrentHealth = 150;
            this.Effects.AddEffect(profession);
            Representation = new Symbol('@', Color4.LimeGreen);
            //BaseAtack += 5;
        }

        public IProfession Profession
        {
            get => profession;
            set => profession = value;
        }
    }
}
