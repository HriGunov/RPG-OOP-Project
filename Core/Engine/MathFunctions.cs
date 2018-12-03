using System;
using System.Collections.Generic;

using Models;
using Models.Interfaces;

namespace Core
{
    public class MathFunctions : IMathFunctions
    {

        public MathFunctions()
        {

        }

        //https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm  // startingPosX    startingPosY
        // not guarnteed to lineCoordiantes to to start from (x,y) and go to (x2,y2). It could be the reversed
        public List<ICoordinates> LineAlgorithm(int x, int y, int x2, int y2)
        {
            List<ICoordinates> pathTraveled = new List<ICoordinates>();

            int w = x2 - x;
            int h = y2 - y;

            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                pathTraveled.Add(new Coordinates(x, y));
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
            return pathTraveled;
        }

        //Midpoint circle algorithm
        public List<ICoordinates> GetPerimeter(int y0, int x0, int radius)
        {
            List<ICoordinates> listToReturn = new List<ICoordinates>();
            int x = radius - 1;
            int y = 0;
            int dx = 1;
            int dy = 1;
            int err = dx - (radius << 1);

            while (x >= y)
            {
                listToReturn.Add(new Models.Coordinates(y0 + y, x0 + x));
                listToReturn.Add(new Models.Coordinates(y0 + x, x0 + y));
                listToReturn.Add(new Models.Coordinates(y0 + x, x0 - y));
                listToReturn.Add(new Models.Coordinates(y0 + y, x0 - x));
                listToReturn.Add(new Models.Coordinates(y0 - y, x0 - x));
                listToReturn.Add(new Models.Coordinates(y0 - x, x0 - y));
                listToReturn.Add(new Models.Coordinates(y0 - x, x0 + y));
                listToReturn.Add(new Models.Coordinates(y0 - y, x0 + x));

                if (err <= 0)
                {
                    y++;
                    err += dy;
                    dy += 2;
                }

                if (err > 0)
                {
                    x--;
                    dx += 2;
                    err += dx - (radius << 1);
                }
            }

            return listToReturn;
        }
    }
}
