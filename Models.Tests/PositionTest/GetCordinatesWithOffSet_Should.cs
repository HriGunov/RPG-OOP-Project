using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Interfaces;

namespace Models.Tests.PositionTest
{
    [TestClass]
    public class GetCordinatesWithOffSet_Should
    {
        [TestMethod]
        public void ReturnCorrectCoordinates_When_PassParameters()
        {
            // Arrange
            IPosition position = new Position(5, 10);

            // Act
            ICoordinates coordinates = position.GetCoordinatesWithOffSet(10, 20);

            // Assert
            Assert.IsTrue(coordinates.CordY == 15 && 
                          coordinates.CordX == 30);
        }
    }
}
