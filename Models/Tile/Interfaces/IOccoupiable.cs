namespace Models.Tile.Interfaces
{
    public interface IOccoupiable : IWalkable
    {
        bool Occoupied { get; set; }
    }
}
