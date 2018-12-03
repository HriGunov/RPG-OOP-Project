using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interfaces;
using Models.Tile.Interfaces;
using Moq;
using System;

namespace Models.Tests
{
    [TestClass]
    public class Position_Should
    {
        [TestMethod]
        public void AssignProperCordY_When_ObjectCreated()
        {
            // Arrange & Act
            IPosition position = new Position(5, 10);

            // Assert
            Assert.AreEqual(5, position.CordY);
        }

        [TestMethod]
        public void AssignProperCordX_When_ObjectCreated()
        {
            // Arrange & Act
            IPosition position = new Position(5, 10);

            // Assert
            Assert.AreEqual(10, position.CordX);
        }

        [TestMethod]
        public void ThrowArgumentNullException_When_TileIsNull()
        {
            // Arrange
            Mock<ITile> tile = new Mock<ITile>();

            // Assert & Act
            Assert.ThrowsException<ArgumentNullException>(() =>
                { return new Position(5, 10, null); });
        }
    }
}
