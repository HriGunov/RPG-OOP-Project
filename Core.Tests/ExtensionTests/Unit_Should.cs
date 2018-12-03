using System.Collections.Generic;
using Core.Extension;
using Core.Tests.Abstract;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Interfaces;
using Models.Tile.Interfaces;
using Moq;

namespace Core.Tests.ExtensionTests
{
    [TestClass]
    public class Unit_Should
    {
        [TestMethod]
        [DataRow(0)]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(8.123456)]
        [DataRow(34)]
        [DataRow(1337.420666)]
        [DataRow(500.51245)]
        [DataRow(1060.53)]
        public void Regenerate_TheCorrect_AmountHP(double variance)
        {
            var mockLocation = new Mock<ICoordinates>();

            var mockMap = new Mock<Map>();

            // Arrange
            var unit = new MockUnit(mockLocation.Object, mockMap.Object);

            unit.CurrentHealth = 1;
            unit.HealthRegenPerTurn = variance;
            // mockUnit.SetupGet(u => u.CurrentHealth)

            // Act
            
            unit.RegenarateHP();

            // Assert
            Assert.IsTrue(1.0 + variance == unit.CurrentHealth || unit.CurrentHealth == unit.MaximumHealth);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-2)]
        [DataRow(-4)]
        [DataRow(-8.123456)]
        [DataRow(-34)]
        [DataRow(-10000.5)]
        public void Should_DealDamageWhen_NegativeRegen(double variance)
        {
            // Arrange
            var mockLocation = new Mock<ICoordinates>();

            var mockMap = new Mock<Map>();

            var unit = new MockUnit(mockLocation.Object, mockMap.Object);

            unit.CurrentHealth = 100;
            unit.HealthRegenPerTurn = variance;
            // mockUnit.SetupGet(u => u.CurrentHealth)

            // Act
            unit.RegenarateHP();

            // Assert
            Assert.IsTrue(100 + variance == unit.CurrentHealth);
        }

        [TestMethod]
        [DataRow(10)]
        [DataRow(20)]
        [DataRow(400)]
        [DataRow(80.123456)]
        [DataRow(340)]
        [DataRow(1337.420666)]
        [DataRow(500.51245)]
        [DataRow(1060.53)]
        public void Regenerate_ShouldCap_AtMxHP(double variance)
        {
            var mockLocation = new Mock<ICoordinates>();

            var mockMap = new Mock<Map>();

            // Arrange
            var unit = new MockUnit(mockLocation.Object, mockMap.Object);

            unit.CurrentHealth = 0;
            unit.MaximumHealth = 10;
            unit.HealthRegenPerTurn = variance;
            // mockUnit.SetupGet(u => u.CurrentHealth)

            // Act
            unit.RegenarateHP();

            // Assert
            Assert.IsTrue(unit.CurrentHealth == unit.MaximumHealth);
        }

        [TestMethod]
        public void NotSeeWhen_LineOfSight_IsBroken()
        {
            // Arrange

            // TODO: do the matrix creaion here without relying on map constuctor

            // Altho it brakes the isolation map used because  its initialise method creates
            // AND MAPS!! postion x,y  leaving us only to fill them with tiles

            // Creates a map off 3 tiles and puts a wall in the middle

            var Matrix = new Map(1, 3).Matrix;

            var mockWall = new Mock<ITile>();
            var mockAir = new Mock<ITile>();

            mockWall.SetupGet(w => w.BlocksSight).Returns(true);
            mockAir.SetupGet(w => w.BlocksSight).Returns(false);
            Matrix[0][1].Tile = mockWall.Object;
            Matrix[0][0].Tile = mockAir.Object;
            Matrix[0][2].Tile = mockAir.Object;

            var mockMap = new Mock<Map>();
            var map = mockMap.Object;
            map.Matrix = Matrix;

            var unit = new MockUnit(Matrix[0][0], mockMap.Object);
            // These Coordinates could be mocked
            var lineOfSight =
                new List<ICoordinates> { new Coordinates(0, 0), new Coordinates(0, 1), new Coordinates(0, 2) };

            // TODO Refracto can see function to be independent of unit, pass 2 coordinates and a map rather than depending on the unit or added as utility method in Map
            
            // Act
            Assert.IsFalse(unit.CanSee(lineOfSight));
        }

        [TestMethod]
        public void See_WhenLineOfSight_IsNotBroken()
        {
            // Arrange
            var Matrix = new Map(1, 3).Matrix;

            var mockWall = new Mock<ITile>();
            var mockAir = new Mock<ITile>();

            mockWall.SetupGet(w => w.BlocksSight).Returns(true);
            mockAir.SetupGet(w => w.BlocksSight).Returns(false);
            Matrix[0][0].Tile = mockAir.Object;
            Matrix[0][1].Tile = mockAir.Object;
            Matrix[0][2].Tile = mockAir.Object;

            var mockMap = new Mock<Map>();
            var map = mockMap.Object;
            map.Matrix = Matrix;

            var unit = new MockUnit(Matrix[0][0], mockMap.Object);
            // These Coordinates could be mocked
            var lineOfSight =
                new List<ICoordinates> { new Coordinates(0, 0), new Coordinates(0, 1), new Coordinates(0, 2) };

            // TODO: Refracto can see function to be independent of unit, pass 2 coordinates and a map rather than depending on the unit
            
            // Act
            Assert.IsTrue(unit.CanSee(lineOfSight));
        }

        public void NotSee_WhenLineOfSight_IsOutsideMapBounds()
        {
            // Arrange
            var Matrix = new Map(1, 3).Matrix;

            var mockWall = new Mock<ITile>();
            var mockAir = new Mock<ITile>();

            mockWall.SetupGet(w => w.BlocksSight).Returns(true);
            mockAir.SetupGet(w => w.BlocksSight).Returns(false);
            Matrix[0][0].Tile = mockAir.Object;
            Matrix[0][0].Tile = mockAir.Object;
            Matrix[0][2].Tile = mockAir.Object;

            var mockMap = new Mock<Map>();
            var map = mockMap.Object;
            map.Matrix = Matrix;
            var unit = new MockUnit(Matrix[0][0], mockMap.Object);

            // these Coordinates could be mocked
            var lineOfSightOutOfBounds =
                new List<ICoordinates> { new Coordinates(0, 0), new Coordinates(0, 1), new Coordinates(0, 2), new Coordinates(0, 3) };

            // TODO: Refracto can see function to be independent of unit, pass 2 coordinates and a map rather than depending on the unit
            // Act
            Assert.IsFalse(unit.CanSee(lineOfSightOutOfBounds));
        }
    }
}
