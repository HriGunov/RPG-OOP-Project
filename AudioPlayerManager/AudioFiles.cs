using System.IO;

namespace AudioPlayerManager
{
    public static class AudioFiles
    {
        public static string[] GetSoundPaths()
        {
            return Directory.GetFiles(@"..\..\..\AudioPlayerManager\AudioFiles\Sound");
        }

        public static string[] GetMusicPaths()
        {
            return Directory.GetFiles(@"..\..\..\AudioPlayerManager\AudioFiles\Music");
        }
    }
}
