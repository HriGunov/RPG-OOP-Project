using Models.Tile.Abstract;

namespace Models.Tile
{
    public class SecretTile : DefaultTile
    {
        public static char registrationKey = '$';

        public SecretTile() : base(registrationKey, true, true)
        {

        }

        public override char RegistrationKey => registrationKey;
    }
}
