using System.Media;

using AudioPlayerManager.Contracts;

namespace AudioPlayerManager
{
    public class Player : IPlayer
    {
        private SoundPlayer BasicPlayer { get; }

        public Player()
        {
            this.BasicPlayer = new SoundPlayer();
        }

        public void SetLocation(string path)
        {
            this.BasicPlayer.SoundLocation = path;
        }

        public void Play()
        {
            this.BasicPlayer.Play();
        }

        public void PlayLooping()
        {
            this.BasicPlayer.PlayLooping();
        }
    }
}
