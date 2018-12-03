using System;
using AudioPlayerManager.Contracts;
using Core.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Unit.Combat.Interfaces;
using Models.Unit.Interfaces;
using Moq;

namespace Core.Tests.CombatManagerTests
{
    [TestClass]
    public class CalculateAttack_Should
    {
        private CombatManager combatManager;

        [TestInitialize]
        public void Initialise()
        {
            var mockLogger = new Mock<ILogger>();
            var mockAudio = new Mock<IAudio>();
            var mockRandom = new Mock<Random>();
            //Random will always return 5
            mockRandom.Setup(r => r.Next()).Returns(5);
            combatManager = new CombatManager(mockLogger.Object, mockAudio.Object, mockRandom.Object);
        }

        //Currently the unit hits its self
        [TestMethod]
        [DataRow(10, 15, 0, 5, 5, 5)]
        [DataRow(10, 15, 0, 5, 5, 10000)]
        [DataRow(100000, 1000000, 0, 5, 5, 10000)]
        [DataRow(100000, 10000, 0, 5, 5, 10000)]
        [DataRow(100000, 10000, 10000, 10000, 10000, 10000)]
        public void NotReturn_Negative_Values(int attackLowBoundary,
            int attackHighBoundary, int attackBonusLowBoundary, int attackBonusHighBoundary, int endurance, int armour)
        {
            var mockAttack = new Mock<IAttack>();
            mockAttack.SetupGet(a => a.AttackBonusLowBoundary).Returns(attackBonusLowBoundary);
            mockAttack.SetupGet(a => a.AttackBonusHighBoundary).Returns(attackBonusHighBoundary);

            var mockUnit = new Mock<IUnit>();
            mockUnit.Setup(x => x.CombatombatStats.GetAtackLowBoundary()).Returns(attackLowBoundary);
            mockUnit.Setup(x => x.CombatombatStats.GetAtackHighBoundary()).Returns(attackHighBoundary);
            mockUnit.Setup(x => x.Attributes.Endurance).Returns(endurance);
            mockUnit.Setup(x => x.CombatombatStats.GetArmour()).Returns(armour);

            Assert.IsTrue(combatManager.CalculateAttackDamage(mockUnit.Object, mockUnit.Object, mockAttack.Object, false) > 0);
        }
    }
}
