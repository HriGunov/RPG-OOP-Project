using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Unit.Interfaces;
using Models.Unit.Profession.Interfaces;

namespace Models.Unit.Profession
{
    class Warrior : AbstractProfession
    {
        public Warrior( ) : base(new Attributes(5,1,2,-2), "The Warrior", "Figths things.")
        {
        }
    }
}
