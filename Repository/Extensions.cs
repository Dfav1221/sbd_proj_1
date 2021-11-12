using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class Extensions
    {
        public static double GeometricMean(this int[] numbers)
        {
            var product = numbers.Aggregate(1, (current, number) => current * number);

            double size = numbers.Length;
            var root = 1 / size;
            return Math.Pow(product, root);
        }
    }
}