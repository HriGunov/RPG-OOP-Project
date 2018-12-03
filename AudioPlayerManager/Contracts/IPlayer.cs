using System.Media;

namespace AudioPlayerManager.Contracts
{
    public interface IPlayer
    {
        void SetLocation(string path);

        void Play();

        void PlayLooping();
    }
}
