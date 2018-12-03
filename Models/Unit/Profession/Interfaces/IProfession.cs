using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Effects.Interfaces;
using Models.Interfaces;

namespace Models.Unit.Profession.Interfaces
{
    public interface IProfession : IUnitEffect
    {
       
     
        string Name { get; }
        string Description { get; }
        Attributes DeltaAttributes { get; set; }
    }
}
