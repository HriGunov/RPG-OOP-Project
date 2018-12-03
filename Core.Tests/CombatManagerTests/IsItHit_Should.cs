using System;
using AudioPlayerManager.Contracts;
using Core.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Core.Tests.CombatManagerTests
{
    [TestClass]
    public class IsItHit_Should
    {
        private CombatManager combatManager;
        [TestInitialize]
        public void Initialise()
        {
            var mockLogger = new Mock<ILogger>();
            var mockAudio = new Mock<IAudio>();
            var mockRandom = new Mock<Random>();
            combatManager = new CombatManager(mockLogger.Object, mockAudio.Object, mockRandom.Object);
        }

        [TestMethod]
        [DataRow(-5)]
        [DataRow(-50)]
        [DataRow(int.MinValue)]
        public void ThrowException_WhenRolledInput_IsInvalid(int sut)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { combatManager.IsItHit(sut, 50); });
        }

        [TestMethod]
        [DataRow(-5)]
        [DataRow(-50)]
        [DataRow(int.MinValue)]
        public void ThrowException_WhenChanseInput_IsInvalid(int sut)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => { combatManager.IsItHit(50, sut); });
        }

        [TestMethod]
        [DataRow(5, 0)]
        [DataRow(50, 1)]
        [DataRow(2, 1)]
        [DataRow(75, 15)]
        [DataRow(1000, 15)]
        [DataRow(100, 99)]
        public void Return_Correct_Result(int roll, int Chance)
        {
            combatManager.IsItHit(roll, Chance);
        }
    }
}
