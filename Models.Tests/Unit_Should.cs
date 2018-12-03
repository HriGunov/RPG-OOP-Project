using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Unit.Enemies.Animals;

namespace Models.Tests
{
    [TestClass]
    public class Unit_Should
    {
        [TestMethod]
        public void DieAfterIfItReceivesMoreDamageThanCurrentHP()
        {
            // Arrange 
            var unit = new EnemyDog(null, null);

            // Act
            unit.ReciveAtack(unit.CurrentHealth + 1);

            // Assert
            Assert.IsTrue(unit.Dead);
        }

        [TestMethod]
        public void IsAliveIfDamageIsLessThanCurrentHP()
        {
            // Arrange 
            var unit = new EnemyDog(null, null);

            // Act
            unit.ReciveAtack(unit.CurrentHealth - 1);

            // Assert
            Assert.IsFalse(unit.Dead);
        }

        [TestMethod]
        public void ShouldCorrectlyTakeDamage()
        {
            // Arrange 
            var unit = new EnemyDog(null, null);

            double unitInitialHealth = unit.CurrentHealth;

            // Act
            unit.ReciveAtack(unitInitialHealth - 1);

            // Assert
            Assert.AreEqual(1, unit.CurrentHealth);
        }
    }
}
