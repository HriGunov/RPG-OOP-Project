using System;
using System.Runtime.InteropServices;

using AudioPlayerManager.Common;
using AudioPlayerManager.Contracts;

namespace AudioPlayerManager
{
    public class Audio : IAudio
    {
        private bool isAudioOn = true;
        private IPlayer soundPlayer;
        private IPlayer musicPlayer;

        [DllImport("winmm.dll")]
        private static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        public Audio(IPlayer injectedSoundPlayer, IPlayer injectedMusicPlayer)
        {
            this.SoundPlayer = injectedSoundPlayer ?? throw new ArgumentNullException("SoundPlayer cannot be null");
            this.MusicPlayer = injectedMusicPlayer ?? throw new ArgumentNullException("MusicPlayer cannot be null");
        }

        /// <summary>
        /// Returns volume from 0 to 10
        /// </summary>
        /// <returns>Volume from 0 to 10</returns>
        private int GetVolume()
        {
            uint CurrVol = 0;
            waveOutGetVolume(IntPtr.Zero, out CurrVol);
            ushort calcVol = (ushort)(CurrVol & 0x0000ffff);
            int volume = calcVol / (ushort.MaxValue / 10);
            return volume;
        }

        /// <summary>
        /// Sets volume from 0 to 10
        /// </summary>
        /// <param name="volume">Volume from 0 to 10</param>
        private void SetVolume(int volume)
        {
            if (volume < 0 || volume > 10)
                throw new ArgumentOutOfRangeException("Volume must be between 0 and 10");

            int newVolume = ((ushort.MaxValue / 10) * volume);
            uint newVolumeAllChannels = (((uint)newVolume & 0x0000ffff) | ((uint)newVolume << 16));
            waveOutSetVolume(IntPtr.Zero, newVolumeAllChannels);
        }

        public bool IsAudioOn
        {
            get { return this.isAudioOn; }
            private set { this.isAudioOn = value; }
        }

        public IPlayer SoundPlayer
        {
            get => soundPlayer;
            private set => soundPlayer = value;
        }

        public IPlayer MusicPlayer
        {
            get => musicPlayer;
            private set => musicPlayer = value;
        }

        public void TurnOnAudio()
        {
            SetVolume(10);
            this.IsAudioOn = true;
        }

        public void TurnOffAudio()
        {
            SetVolume(0);
            this.IsAudioOn = false;
        }

        /// <summary>
        /// Play certain sound just once (opens new thread)
        /// </summary>
        public void Play(Sound sound)
        {
            string[] paths = AudioFiles.GetSoundPaths();
            this.soundPlayer.SetLocation(paths[(int)sound]);
            this.soundPlayer.Play();
        }

        /// <summary>
        /// Play certain music looping (opens new thread)
        /// </summary>
        public void Play(Music music)
        {
            string[] paths = AudioFiles.GetMusicPaths();
            this.MusicPlayer.SetLocation(paths[(int)music]);
            this.MusicPlayer.PlayLooping();
        }
    }
}
