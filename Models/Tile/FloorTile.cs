using Models.Tile.Abstract;

namespace Models.Tile
{
    public class FloorTile : DefaultTile
    {
        private Symbol type;
        public static char registrationKey = '.';

        public FloorTile() : base(registrationKey, false, true, false)
        {

        }

        public override char RegistrationKey => registrationKey;
    }
}
