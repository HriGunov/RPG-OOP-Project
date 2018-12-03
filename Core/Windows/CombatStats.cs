using Models;
using Models.Unit.Player;

namespace Core.Windows
{
    public class CombatStats : BorderedWindow
    {
        private IPlayer player;
        private int baseAtack;
        private int baseAtackBounus;
        private int baseHitChance;
        private int baseDodgeChance;
        private int armour;

        public CombatStats(string title, int pinX, int pinY, int width, int height, Symbol borderSymnol, bool includeTitle, IPlayer player, int layer = 0)
            : base(title, pinX, pinY, width, height, borderSymnol, includeTitle, layer)
        {
            this.Title = "Attributes";
            this.player = player;

            Update();
        }

        public IPlayer Player { get => player; set => player = value; }
        public void Update()
        {
            int offset = 0;
            CalculateCurrentStats();

            string baseText = "Base Attack:" + baseAtack;
            for (int i = 0; i < baseText.Length; i++)
            {
                this.matrix[1][i + offset] = new Symbol(baseText[i]);
            }
            baseText = "Base Attack Bonus:" + baseAtackBounus;
            for (int i = 0; i < baseText.Length; i++)
            {
                this.matrix[3][i + offset] = new Symbol(baseText[i]);
            }
            baseText = "Base Hit Chance:" + baseHitChance;
            for (int i = 0; i < baseText.Length; i++)
            {
                this.matrix[5][i + offset] = new Symbol(baseText[i]);
            }
            baseText = "Armour:" + armour;
            for (int i = 0; i < baseText.Length; i++)
            {
                this.matrix[7][i + offset] = new Symbol(baseText[i]);
            }
            baseText = "Base Dodge Chance:" + baseDodgeChance;
            for (int i = 0; i < baseText.Length; i++)
            {
                this.matrix[9][i + offset] = new Symbol(baseText[i]);
            }

        }
        private void CalculateCurrentStats()
        {
            /*
            this.baseAtack = Player.BaseAtack;
            this.baseAtackBounus = Player.AttackBonus;
            this.baseHitChance = Player.Attributes.Endurance;
            this.baseDodgeChance = Player.BaseDodge;
            this.armour = Player.Armour;
            */
        }
    }
}
