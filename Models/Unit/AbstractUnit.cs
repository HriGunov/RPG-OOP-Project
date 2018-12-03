using System.Collections.Generic;
using Models.Effects;
using Models.Interfaces;
using Models.Unit.Combat;
using Models.Unit.Combat.Interfaces;
using Models.Unit.Interfaces;
using OpenTK.Graphics;

namespace Models.Unit
{
    public abstract class AbstractUnit : IUnit
    {
        private Map map;
        private double healthRegenPerTurn;
        private double currentHealth;
        private double maximumHealth;
        private EffectsOnUnit effects;
        private ICoordinates location;
        private string atackDescription;
        private IAttributes attributes;
        private bool dead;
        private CombatStats combatombatStats;
        private ICollection<IAttack> availableAtacks;

        public AbstractUnit(ICoordinates location, Map map)
        {
            Name = "Unknown";
            Description = "None";
            Location = location;
            Map = map;

            attributes = new Attributes();
            CombatombatStats = new CombatStats();
            effects = new EffectsOnUnit(this);
            healthRegenPerTurn = 1.3 + Attributes.Endurance / 4;
            MaximumHealth = 100;
            CurrentHealth = MaximumHealth;
            availableAtacks = new List<IAttack>();
            availableAtacks.Add(new DefaultAttack());

            Representation = new Symbol('U', Color4.Yellow);
        }

        public IAttributes Attributes
        {
            get => attributes;
            set => attributes = value;
        }

        public EffectsOnUnit Effects { get => effects; private set => effects = value; }
        public double HealthRegenPerTurn
        {
            get => healthRegenPerTurn;
            set => healthRegenPerTurn = value;
        }

        public double CurrentHealth
        {
            get => currentHealth;
            set => currentHealth = value;
        }

        public double MaximumHealth
        {
            get => maximumHealth;
            set => maximumHealth = value;
        }

        public ICoordinates Location
        {
            get => location;
            set => location = value;
        }

        public ICollection<IAttack> AvailableAttacks => availableAtacks;

        public CombatStats CombatombatStats
        {
            get => combatombatStats;
            set => combatombatStats = value;
        }

        public void ReciveAtack(double damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Dead = true;
            }
        }

        virtual public bool Dead
        {
            get => dead;
            set => dead = value;
        }

        public Symbol Representation { get; set; }

        public int Armour { get; set; }

        public Map Map
        {
            get => map;
            set
            {
                map = value;
                if (value != null)
                {
                    Map[Location.CordY, Location.CordX].Tile.Occoupied = true;

                }
            }
        }

        public string Name { get; set; }

        public string Description { get; }
    }
}
