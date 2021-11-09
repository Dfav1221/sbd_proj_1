using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class Extensions
    {
        public static double GeometricMean(this IEnumerable<int> numbers)
        {
            var product = 1;
            var enumerable = numbers as int[] ?? numbers.ToArray();
            enumerable.ToList().ForEach(n => product *= n);
            double size = enumerable.Length;
            var root = 1 / size;
            return Math.Pow(product, root);
        }
    }
}