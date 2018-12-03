using System;

namespace Models.Interfaces
{
    public interface ICoordinates : IEquatable<ICoordinates>
    {
        int CordY { get; }

        int CordX { get; }

        int DistanceTo(ICoordinates targetCoordinates);

        ICoordinates GetCoordinatesWithOffSet(int deltaY, int delaX);

        void Change(int cordY, int cordX);

        void Change(ICoordinates cordiantes);
    }
}
