using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Unit.Interfaces;
using Models.Unit.Profession.Interfaces;

namespace Models.Unit.Profession
{
    class AbstractProfession : IProfession
    {

        private string name;
        private string description;
        private Attributes deltaAttributes;


        public AbstractProfession(Attributes atributes) : this(atributes,"The Abstractor")
        {
            
        }

        public AbstractProfession(Attributes atributes, string name) : this(atributes, name, "Abstract description!")
        {
             
        }

        public AbstractProfession(Attributes atributes, string name, string description)
        {
            this.DeltaAttributes = atributes;
            this.name = name;
            description = "It's a warrior LOL.";
        }

        public string Name => name;

        public string Description => description;

        public Attributes DeltaAttributes
        {
            get => deltaAttributes;
            set => deltaAttributes = value;
        }


        public void ActivateEffect(IUnit unit)
        {
            unit.Attributes = new Attributes(
                unit.Attributes.Strength + deltaAttributes.Strength,
                unit.Attributes.Perception + deltaAttributes.Perception,
                unit.Attributes.Endurance + deltaAttributes.Endurance,
                unit.Attributes.Agility + deltaAttributes.Agility
            );
        }

        public void UnactivateEffect(IUnit unit)
        {

            unit.Attributes = new Attributes(
                unit.Attributes.Strength - deltaAttributes.Strength,
                unit.Attributes.Perception - deltaAttributes.Perception,
                unit.Attributes.Endurance - deltaAttributes.Endurance,
                unit.Attributes.Agility - deltaAttributes.Agility
            );
        }
    }
}
