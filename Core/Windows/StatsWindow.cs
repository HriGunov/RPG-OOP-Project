using Models;
using Models.Unit.Player;
using OpenTK.Graphics;

namespace Core.Windows
{
    public class StatsWindow : Window
    {
        private IPlayer player;
        const int secondColStart = 35;
        private int greenHealthBars;
        private int redHealthBars;
        private int healthPercents;

        public StatsWindow(int pinY, int pinX, int width, int height, IPlayer player, int layer = 0)
            : base("Stats", pinY, pinX, width, height, layer)
        {
            this.Player = player;

            Text name = new Text($" Name: Table");
            SetTextLine(name.Symbols, 1);
        }

        public IPlayer Player { get => player; set => player = value; }

        public void Update()
        {
            CalculateCurrentHP();

            int colPosition;

            // calculates health text:
            string healthText = "Health:";
            for (int i = 0; i < healthText.Length; i++)
            {
                colPosition = secondColStart + i - healthText.Length - 1;
                matrix[1][colPosition].Value = healthText[i];
            }

            // calculates green bars
            for (int i = 0; i < greenHealthBars; i++)
            {
                colPosition = secondColStart + i;
                matrix[1][colPosition] = new Symbol(' ', Color4.Green, Color4.Green);
            }

            // calculates red bars
            for (int i = 0; i < redHealthBars; i++)
            {
                colPosition = secondColStart + greenHealthBars + i;
                matrix[1][colPosition] = new Symbol(' ', Color4.Red, Color4.Red);
            }

            // calculates percents on bar
            for (int i = 0; i < healthPercents.ToString().Length; i++)
            {
                matrix[1][secondColStart + i].Value = healthPercents.ToString()[i];
                matrix[1][secondColStart + i].Color = Color4.White;
            }
            colPosition = healthPercents.ToString().Length + secondColStart;
            matrix[1][colPosition].Value = '%';
            matrix[1][colPosition].Color = Color4.White;

            // calculate profession
            string professionText = " Profession: " + Player.Profession.Name;
            for (int i = 0; i < professionText.Length; i++)
            {
                matrix[3][i].Value = professionText[i];
            }

            // calculate level
            string levelText = "Level: " + 1; // TODO: change it later
            for (int i = 0; i < levelText.Length; i++)
            {
                colPosition = 27 + i;
                matrix[3][colPosition].Value = levelText[i];
            }

            string killsText = "Kills: " + Engine.KillCounter; // TODO: change it later
            for (int i = 0; i < killsText.Length; i++)
            {
                colPosition = 36 + i;
                matrix[3][colPosition].Value = killsText[i];
            }
        }

        private void CalculateCurrentHP()
        {
            this.healthPercents = (int)(player.CurrentHealth / (double)player.MaximumHealth * 100.0);
            healthPercents = healthPercents < 0 ? 0 : healthPercents;
            this.greenHealthBars = healthPercents / 10;
            this.redHealthBars = 10 - greenHealthBars;
        }
    }
}
