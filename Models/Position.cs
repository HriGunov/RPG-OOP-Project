using System;

using Models.Tile;
using Models.Tile.Interfaces;
using Models.Interfaces;

namespace Models
{
    public class Position : IPosition
    {
        private int cordX;
        private int cordY;

        private ITile tile;

        public Position(int cordY, int cordX) : this(cordY, cordX, new WallTile())
        {

        }

        public Position(int cordY, int cordX, ITile tile)
        {
            this.CordY = cordY;
            this.CordX = cordX;
            this.Tile = tile ?? throw new ArgumentNullException("Tile cannot be null");
        }

        public int CordY
        {
            get { return cordY; }
            set { cordY = value; }
        }

        public int CordX
        {
            get { return cordX; }
            set { cordX = value; }
        }

        //Chebyshev distance
        public int DistanceTo(ICoordinates targetCoordinates)
        {
            if (Math.Abs(CordX - targetCoordinates.CordX) > Math.Abs(CordY - targetCoordinates.CordY))
            {
                return Math.Abs(CordX - targetCoordinates.CordX);
            }

            return Math.Abs(CordY - targetCoordinates.CordY);
        }

        public ICoordinates GetCoordinatesWithOffSet(int deltaCordY, int deltaCordX)
        {
            return new Coordinates(CordY + deltaCordY, CordX + deltaCordX);
        }

        public ITile Tile
        {
            get { return tile; }
            set { tile = value; }
        }

        public bool Equals(ICoordinates other)
        {
            if (this.CordX == other.CordX && this.CordY == other.CordY)
            {
                return true;
            }

            return false;
        }

        public void Change(int cordY, int cordX)
        {
            this.CordY = cordY;
            this.CordX = cordX;
        }

        public void Change(ICoordinates cordiantes)
        {
            Change(cordiantes.CordY, cordiantes.CordX);
        }
    }
}
