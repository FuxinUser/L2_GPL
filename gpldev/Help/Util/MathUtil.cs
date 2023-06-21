using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Util
{
    public static class MathUtil
    {

        public static double GetPoint(this double value, int number)
        {
            return Math.Round(value, number, MidpointRounding.AwayFromZero);
        }


        public static float GetPoint(this float value, int number)
        {
            return (float)Math.Round(value, number, MidpointRounding.AwayFromZero);
        }


    }
}
