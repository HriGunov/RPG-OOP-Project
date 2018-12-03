using AudioPlayerManager.Common;

namespace AudioPlayerManager.Contracts
{
    public interface IAudio
    {
        bool IsAudioOn { get; }

        IPlayer SoundPlayer { get; }

        IPlayer MusicPlayer { get; }

        void Play(Sound sound);

        void Play(Music music);

        void TurnOnAudio();

        void TurnOffAudio();
    }
}
