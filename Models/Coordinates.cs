using System;
using Models.Interfaces;

namespace Models
{
    public class Coordinates : ICoordinates
    {
        private int cordX;
        private int cordY;

        public Coordinates(int cordY, int cordX)
        {
            this.CordY = cordY;
            this.CordX = cordX;
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