using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hnefatafl.Hnefatafl
{
    public class Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position? CreatePosition(string s)
        {
            var coordsAsStrings = s.Split(",");
            if (coordsAsStrings.Length != 2) return null;
            try
            {
                var x = int.Parse(coordsAsStrings[0]);
                var y = int.Parse(coordsAsStrings[1]);
                return new Position(x,y);
            }
            catch (Exception) { return null; }
        }
    }
}
