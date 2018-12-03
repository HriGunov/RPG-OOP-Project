using Models.Interfaces;

namespace Models.Tile.Interfaces
{
    public interface ITile : ISightBlocking, IVisible, IVisualise, IActivatable, IOccoupiable, IRegisterKey
    {
    }
}
