using System.Collections.Generic;

using Models.Interfaces;

namespace Core
{
    public interface IMathFunctions
    {
        List<ICoordinates> GetPerimeter(int y0, int x0, int radius);

        List<ICoordinates> LineAlgorithm(int x, int y, int x2, int y2);
    }
}