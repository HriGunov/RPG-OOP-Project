namespace Models.Tile.Interfaces
{
    public interface IVisualise
    {
        /// <summary>
        /// Get tile's char representation.
        /// </summary>
        Symbol Representation();

        bool SeenBefore { get; set; }

        Symbol VisibleSymbol { get; set; }
    }
}
