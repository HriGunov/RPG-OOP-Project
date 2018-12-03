using Models.Interfaces;

namespace Models.Unit.Interfaces
{
    public interface ILocation
    {
        ICoordinates Location { get; set; }
    }
}
