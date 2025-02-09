using System;
using static System.Math;

public static class sfuns
{
    // Single precision gamma function
    public static double fgamma(double x)
    {
        if (x <= 0) return double.NaN; // Gamma function undefined for non-positive values
        if (x < 9) return fgamma(x + 1) / x; // Recurrence relation

        // Stirling's approximation
        double lnfgamma = x * Log(x + 1 / (12 * x - 1 / (10 * x))) - x + Log(2 * PI / x) / 2;
        return Exp(lnfgamma);
    }

    // Log-Gamma function
    public static double lngamma(double x)
    {
        if (x <= 0) return double.NaN; // Log-Gamma undefined for non-positive values
        if (x < 9) return lngamma(x + 1) - Log(x); // Recurrence relation

        // Stirling's approximation for ln(gamma)
        return x * Log(x + 1 / (12 * x - 1 / (10 * x))) - x + Log(2 * PI / x) / 2;
    }
}

