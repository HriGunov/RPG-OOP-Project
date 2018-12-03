using Models.Tile.Abstract;

namespace Models.Tile
{
    public class WallTile : DefaultTile
    {
        public static char registrationKey = '#';

        public WallTile() : base(registrationKey)
        {

        }

        public override char RegistrationKey => registrationKey;
    }
}
