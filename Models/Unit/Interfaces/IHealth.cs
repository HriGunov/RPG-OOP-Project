namespace Models.Unit.Interfaces
{
    public interface IHealth : IRegenrate
    {
        double CurrentHealth { get; set; }

        double MaximumHealth { get; }
    }
}
