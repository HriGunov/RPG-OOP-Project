using Models.Interfaces;

namespace Models.Unit
{
    public class UnitPostion : Coordinates, ICoordinates
    {
        public UnitPostion(int cordY, int cordX) : base(cordY, cordX)
        {

        }

        //Used for then we want to change the position of a unit w/o creating a new onject
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