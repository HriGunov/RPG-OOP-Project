using AudioPlayerManager.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AudioPlayerManager.Tests.AudioTest
{
    [TestClass]
    public class TurnOffAudio_Should
    {
        [TestMethod]
        public void TurnffTheAudio_When_MethodIsInvoked()
        {
            // Arrange
            Mock<IPlayer> soundPlayer = new Mock<IPlayer>();
            Mock<IPlayer> musicPlayer = new Mock<IPlayer>();

            IAudio result = new Audio(soundPlayer.Object, musicPlayer.Object);

            // Act
            result.TurnOffAudio();

            // Assert
            Assert.IsFalse(result.IsAudioOn);
        }
    }
}
