namespace Models.Tile.Interfaces
{
    public interface IWalkable
    {
        /// <summary>
        /// A property which shows whether a character can walk over something.
        /// </summary>
        bool CanWalk { get; }
    }
}
