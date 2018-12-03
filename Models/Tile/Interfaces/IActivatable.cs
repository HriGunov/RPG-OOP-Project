namespace Models.Tile.Interfaces
{
    public interface IActivatable
    {
        bool CanActivate { get; }

        void Activate();
    }
}
