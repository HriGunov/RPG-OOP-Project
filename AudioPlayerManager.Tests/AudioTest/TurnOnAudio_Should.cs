using AudioPlayerManager.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AudioPlayerManager.Tests.AudioTest
{
    [TestClass]
    public class TurnOnAudio_Should
    {
        [TestMethod]
        public void TurnOnTheAudio_When_MethodIsInvoked()
        {
            // Arrange
            Mock<IPlayer> soundPlayer = new Mock<IPlayer>();
            Mock<IPlayer> musicPlayer = new Mock<IPlayer>();

            IAudio sut = new Audio(soundPlayer.Object, musicPlayer.Object);

            // Act
            sut.TurnOnAudio();

            // Assert
            Assert.IsTrue(sut.IsAudioOn);
        } 
    }
}
