using Models.Effects.Interfaces;

namespace Models.Items.Interfaces
{
    public interface IItem : IUnitEffect
    {
        string ItemName { get; set; }

        string ItemDescription { get; set; }
    }
}
