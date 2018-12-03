using Models.Tile.Interfaces;

namespace Models.Interfaces
{
    public interface IPosition : ICoordinates
    {
        ITile Tile { get; set; }
    }
}
