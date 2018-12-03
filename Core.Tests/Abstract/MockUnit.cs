using Models;
using Models.Interfaces;
using Models.Unit;

namespace Core.Tests.Abstract
{
    // TODO: Doesn't work for some reason (it cannot access the extensions methods)
    public class MockUnit : AbstractUnit
    {
        public MockUnit(ICoordinates location, Map map) : base(location, map)
        {
        }
    }
}
