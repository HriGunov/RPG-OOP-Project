using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interfaces;

namespace Models.Tests.PositionTest
{
    [TestClass]
    public class Change_Should
    {
        [TestMethod]
        public void SetNewProperCoordinates_When_Invoked()
        {
            // Arrange
            IPosition position = new Position(5, 10);

            // Act
            position.Change(55, 77);

            // Assert
            Assert.AreEqual(position.CordY, 55);
            Assert.AreEqual(position.CordX, 77);
        }
    }
}
