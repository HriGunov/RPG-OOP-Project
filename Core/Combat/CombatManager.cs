using System;
using System.Collections.Generic;
using System.Linq;

using AudioPlayerManager.Common;
using AudioPlayerManager.Contracts;
using Core.Extension;
using Core.Logger;
using Models.Unit.Combat.Interfaces;
using Models.Unit.Enemies.Interfaces;
using Models.Unit.Interfaces;

namespace Core
{
    public class CombatManager
    {
        public bool OnePunchMode = false;
        private readonly ILogger logger;
        private readonly IAudio audio;
        private readonly Random randomProvider;

        //cheats

        public CombatManager(ILogger logger, IAudio audio, Random randomProvider)
        {
            this.logger = logger;
            this.audio = audio;
            this.randomProvider = randomProvider;
        }

        public void HandlePlayerAtack(Models.Unit.Player.IPlayer player, IEnemy target)
        {
            var successfulHit = HandleAttack(player, target);
            if (successfulHit)
            {
                audio.Play((Sound)(7 + randomProvider.Next(3)));
                if (target.Dead || OnePunchMode)
                {
                    EnemyDied(target);
                    Engine.KillCounter++;
                    audio.Play(Sound.EnemyDie);
                }
            }
            else
            {
                //should be dodge sound
                //Audio.Play(Sound.GameOver);
            }
        }

        private void EnemyDied(IEnemy target)
        {
            target.Map[target.Location.CordY, target.Location.CordX].Tile.Occoupied = false;
            target.Map.AwakenedEnemies.Remove(target);
            target.Map.Enemies.Remove(target);
        }

        public bool HandleAttack(IUnit attacker, IUnit target)
        {

            var attackMethod = ChoseAttack(attacker);
            int atackChance = CalculateHitChance(attacker, target, attackMethod);

            int rolledNumber = randomProvider.Next(100);
            var rollMsg = $"rolled {rolledNumber}, needed { atackChance}";

            var isCriticalHit = rolledNumber >= 95;
            if (IsItHit(rolledNumber, atackChance) || isCriticalHit)
            {
                double damageToBeDealth = CalculateAttackDamage(attacker, target, attackMethod, isCriticalHit);

                logger.Log($"{attacker.Name} {rollMsg}." + '\n' + $"The {attacker.Name} {(isCriticalHit ? "crically " : "")}{attackMethod.AttackVerb} the {target.Name} for {damageToBeDealth:F1} dmg.");

                target.ReciveAtack(damageToBeDealth);
                return true;
            }
            else
            {
                logger.Log($"{attacker.Name} {rollMsg}. \n{attacker.Name} did not hit {target.Name}.");
                return false;
            }
        }

        public IAttack ChoseAttack(IUnit sourceUnit)
        {
            var attackRoll = randomProvider.Next(sourceUnit.AvailableAttacks.Sum(attack => attack.AttackFrequency));
            sourceUnit.AvailableAttacks.OrderBy(attack => attack.AttackFrequency);
            int frequencyCounter = 0;
            foreach (var attack in sourceUnit.AvailableAttacks)
            {
                frequencyCounter += attack.AttackFrequency;
                if (frequencyCounter >= attackRoll)
                {
                    return attack;
                }
            }
            throw new Exception("Could not chose attack from available attacks");
        }
        public int CalculateHitChance(IUnit attacker, IUnit target, IAttack attack)
        {

            var accurate = attacker.CombatombatStats.GetAtackRating() + attacker.Attributes.Agility * 0.5;
            var evasion = ((target.Attributes.Agility * 0.05 + target.CombatombatStats.GetDodge()) * 0.5) * attack.AttackDifficulty;

            var chance = (int)(((accurate - evasion) / accurate) * 100D);
            if (chance < 0)
                return 5;
            else if (100 < chance)
                return 100;
            return chance;
        }

        public double CalculateAttackDamage(IUnit attacker, IUnit target, IAttack attack, bool critical = false)
        {

            double attackLow = attacker.CombatombatStats.GetAtackLowBoundary() + attack.AttackBonusLowBoundary;
            double attackHigh = attacker.CombatombatStats.GetAtackHighBoundary() + attack.AttackBonusHighBoundary;

            double atackRoll = randomProvider.Next(
                (int)(attackHigh - attackLow),
                (int)attackHigh);

            double pureAttack = attackLow + atackRoll;
            double deffence = (target.CombatombatStats.GetArmour() + target.Attributes.Endurance) * 5;
            double damageToBeDealth = (pureAttack * pureAttack) / deffence; //randomProvider.Next(attacker.combatStats /(attacker.BaseAtack + (target.Armour + target.Attributes.Endurance)) + randomProvider.Next(attacker.AttackBonus) ;
            if (critical)
            {
                damageToBeDealth *= 1.5;
            }

            damageToBeDealth -= target.CombatombatStats.GetArmour();

            if (damageToBeDealth <= 0)
            {
                damageToBeDealth = 1;
            }

            return damageToBeDealth;
        }

        public bool IsItHit(int rolledNumber, int attackChance)
        {
            if (attackChance < 0)
            {
                throw new ArgumentOutOfRangeException("Attack chance cannot be a negitive number.");
            }
            if (rolledNumber < 0)
            {
                throw new ArgumentOutOfRangeException("You couldn't have rolled a negitive number.");
            }
            if (rolledNumber >= attackChance)
            {
                return true;
            }
            return false;
        }

        public void MassRegenerate(ICollection<IEnemy> enemies, Models.Unit.Player.IPlayer player)
        {
            foreach (var enemy in enemies)
            {
                enemy.RegenarateHP();
            }
            player.RegenarateHP();
        }
    }
}
