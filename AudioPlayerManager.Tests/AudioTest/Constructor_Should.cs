using AudioPlayerManager.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AudioPlayerManager;
using Moq;
using System;

namespace AudioPlayerManager.Tests.AudioTest
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void AssignCorrectSoundPlayerValue_When_ObjectCreated()
        {
            // Arrange
            Mock<IPlayer> soundPlayer = new Mock<IPlayer>();
            Mock<IPlayer> musicPlayer = new Mock<IPlayer>();

            // Act
            IAudio result = new Audio(soundPlayer.Object, musicPlayer.Object);
            
            // Assert
            Assert.IsInstanceOfType(result.SoundPlayer, typeof(IPlayer));
        }

        [TestMethod]
        public void AssignCorrectMusicPlayerValue_When_ObjectCreated()
        {
            // Arrange
            Mock<IPlayer> soundPlayer = new Mock<IPlayer>();
            Mock<IPlayer> musicPlayer = new Mock<IPlayer>();

            // Act
            IAudio result = new Audio(soundPlayer.Object, musicPlayer.Object);

            // Assert
            Assert.IsInstanceOfType(result.MusicPlayer, typeof(IPlayer));
        }

        [TestMethod]
        public void ThrowArgumentNullException_When_MusicPlayerIsNull()
        {
            // Arrange
            Mock<IPlayer> musicPlayer = new Mock<IPlayer>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                return new Audio(null, musicPlayer.Object);
            });
        }

        [TestMethod]
        public void ThrowArgumentNullException_When_SoundPlayerIsNull()
        {
            // Arrange
            Mock<IPlayer> soundPlayer = new Mock<IPlayer>();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                return new Audio(soundPlayer.Object, null);
            });
        }
    }
}
