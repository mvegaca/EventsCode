﻿using System;

namespace AppStudio.Uwp
{
    public static class MathExtensions
    {
        public static int Mod(this int value, int module)
        {
            int res = value % module;
            return res >= 0 ? res : (res + module) % module;
        }

        public static int IncMod(this int value, int module)
        {
            return (value + 1).Mod(module);
        }

        public static int DecMod(this int value, int module)
        {
            return (value - 1).Mod(module);
        }

        public static double Mod(this double value, double module)
        {
            double res = value % module;
            return res >= 0 ? res : (res + module) % module;
        }

        public static double AsDouble(this string str)
        {
            double d = 0.0;
            if (!String.IsNullOrEmpty(str))
            {
                Double.TryParse(str, out d);
            }
            return d;
        }
    }
}
