namespace DataManagement.Extensions;

public static class GeometricMeanExtension
{
    public static double Mean(this List<int> numbers)
    {
        var uLongNumbers = numbers.Select(n => (ulong) n).ToList();
        ulong product = 1;
        uLongNumbers.ForEach(n=> product*=n);
        
        var root = 1.0 / numbers.Count;

        return Math.Pow(product, root);
    }
}