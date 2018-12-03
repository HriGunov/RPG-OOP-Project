namespace Models.Tile.Interfaces
{
    public interface ISightBlocking
    {
        /// <summary>
        /// Get bool whether line of sight is broken.
        /// </summary>
        bool BlocksSight { get; }
    }
}
