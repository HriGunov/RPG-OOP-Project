using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Effects;
using Models.Effects.Interfaces;
using Models.Items.Interfaces;
using Models.Unit.Interfaces;
using Models.Unit.Player;

namespace Models.Items
{
    public class Inventory 
    {
        public Inventory(Player player )
        {
            EffectedUnit = player;
        }

        private IUnit effectedUnit;
        private List<IItem> ItemEffects = new List<IItem>();

        public void AddEffect(IItem effect)
        {
            effect.ActivateEffect(EffectedUnit);
            ItemEffects.Add(effect);
        }

        public void RemoveEffect(IItem effect)
        {
            effect.UnactivateEffect(EffectedUnit);
            ItemEffects.Remove(effect);
        }

        public List<IItem> GetEffects()
        {
            return new List<IItem>(ItemEffects);
        }

        public IUnit EffectedUnit
        {
            get => effectedUnit;
            set => effectedUnit = value;
        }
    }
}
