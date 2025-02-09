using System;
using System.Globalization; // make . instead of ,
using static System.Math;

class Program
{
    static void Main()
    {
	CultureInfo.CurrentCulture = CultureInfo.InvariantCulture; //ensure dot decimal

        // Part 1: Compute Math Constants
        Console.WriteLine($"Sqrt(2) = {Sqrt(2)}");
        Console.WriteLine($"2^(1/5) = {Pow(2, 1.0/5)}");
        Console.WriteLine($"e^π = {Pow(E, PI)}");
        Console.WriteLine($"π^e = {Pow(PI, E)}");

        Console.WriteLine("\nGamma Function Tests:");
        
        // Expected Gamma function values for integers: Γ(n) = (n-1)!
        for (int n = 1; n <= 10; n++)
        {
            double gammaValue = sfuns.fgamma(n);
            Console.WriteLine($"Γ({n}) = {gammaValue}, Expected: {Factorial(n-1)}");
        }

        Console.WriteLine("\nLog-Gamma Function Tests:");
        for (double x = 1; x <= 10; x++)
        {
            double lngammaValue = sfuns.lngamma(x);
            Console.WriteLine($"ln(Γ({x})) = {lngammaValue}");
        }
    }

    // Helper function to compute factorial for expected Gamma values
    static double Factorial(int n)
    {
        if (n == 0) return 1;
        double result = 1;
        for (int i = 1; i <= n; i++) result *= i;
        return result;
    }
}
