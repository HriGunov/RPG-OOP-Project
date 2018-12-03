using System.Collections.Generic;
using Models.Unit.Enemies.Interfaces;

namespace Models.Interfaces
{
    public interface IMap
    {
        int Height { get; }
        int Width { get; }
        Position[][] Matrix { get; }
        Position this[int row, int col] { get; }

        bool Contains(int row, int col);
        bool Contains(ICoordinates pos);

        ICollection<Position> Neighbours(ICoordinates pos);
        ICollection<Position> Neighbours(int CordY, int CordX);
        List<IEnemy> Enemies { get; set; }
        List<IEnemy> AwakenedEnemies { get; set; }
    }
}
