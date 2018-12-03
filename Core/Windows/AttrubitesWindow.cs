using Models;
using Models.Unit.Player;

namespace Core.Windows
{
    public class AttrubitesWindow : BorderedWindow
    {
        private IPlayer player;

        private int strength;
        private int perception;
        private int endurance;
        private int agility;

        public AttrubitesWindow(string title, int pinX, int pinY, int width, int height, Symbol borderSymnol, bool includeTitle, IPlayer player, int layer = 0)
            : base(title, pinX, pinY, width, height, borderSymnol, includeTitle, layer)
        {
            this.Title = "Attributes";
            this.player = player;
        }

        public IPlayer Player { get => player; set => player = value; }

        public void Update()
        {
            CalculateCurrentAttribs();

            int colPosition;

            // Calculate Strenght
            string strengthText = "  STR:" + Player.Attributes.Strength;
            for (int i = 0; i < strengthText.Length; i++)
            {
                colPosition = i;
                this.matrix[1][colPosition + 1] = new Symbol(strengthText[i]);
            }

            // Calculate Perception
            string perText = "  PER:" + Player.Attributes.Perception;
            for (int i = 0; i < perText.Length; i++)
            {
                colPosition = i + 13;
                this.matrix[1][colPosition] = new Symbol(perText[i]);
            }

            // Calculate Endurance
            string endText = "  END:" + Player.Attributes.Strength;
            for (int i = 0; i < endText.Length; i++)
            {
                colPosition = i;
                this.matrix[3][colPosition + 1].Value = endText[i];
            }

            // Calculate Agility
            string aglText = "  AGL:" + Player.Attributes.Strength;
            for (int i = 0; i < aglText.Length; i++)
            {
                colPosition = i + 13;
                this.matrix[3][colPosition].Value = aglText[i];
            }
        }

        private void CalculateCurrentAttribs()
        {
            this.strength = Player.Attributes.Strength;
            this.perception = Player.Attributes.Perception;
            this.endurance = Player.Attributes.Endurance;
            this.agility = Player.Attributes.Agility;
        }
    }
}
