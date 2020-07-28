using System;
using System.Collections.Generic;
using System.Text;

namespace Teleport.Common
{
    public static class Extensions
    {
        public static double ConvertMilesToKilometers(this double miles)
        {
            //
            // Multiply by this constant and return the result.
            //
            return miles * 1.609344;
        }

        public static double ConvertKilometersToMiles(this double kilometers)
        {
            //
            // Multiply by this constant.
            //
            return kilometers * 0.621371192;
        }
    }
}
