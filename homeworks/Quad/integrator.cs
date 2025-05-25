using System;
using static System.Math;

public class Integrator
{
    // Adaptive integrator, task A 
    public static double Adaptive(Func<double, double> f, double a, double b,
                                   double acc = 0.001, double eps = 0.001,
                                   double f2 = double.NaN, double f3 = double.NaN)
    {
        return AdaptiveWithError(f, a, b, acc, eps, f2, f3).Item1;
    }

    // Adaptive integrator with error estimation (Task C)
    public static (double, double) AdaptiveWithError(Func<double, double> f, double a, double b,
                                                     double acc = 0.001, double eps = 0.001,
                                                     double f2 = double.NaN, double f3 = double.NaN)
    {
        double h = b - a;
        if (double.IsNaN(f2)) {
            f2 = f(a + 2 * h / 6);
            f3 = f(a + 4 * h / 6);
        }

        double f1 = f(a + h / 6);
        double f4 = f(a + 5 * h / 6);

        double Q = (2 * f1 + f2 + f3 + 2 * f4) * h / 6;
        double q = (f1 + f2 + f3 + f4) * h / 4;
        double err = Math.Abs(Q - q);

        if (err <= acc + eps * Math.Abs(Q))
            return (Q, err);
        else
        {
            double mid = (a + b) / 2;
            var (Q1, err1) = AdaptiveWithError(f, a, mid, acc / Sqrt(2), eps, f1, f2);
            var (Q2, err2) = AdaptiveWithError(f, mid, b, acc / Sqrt(2), eps, f3, f4);
            return (Q1 + Q2, Sqrt(err1 * err1 + err2 * err2));
        }
    }

    // Clenshaw-Curtis integrator, task B
    public static double ClenshawCurtis(Func<double, double> f, double a, double b, double acc = 1e-3, double eps = 1e-3)
    {
        Func<double, double> new_f = theta => f((a + b) / 2 + (b - a) / 2 * Cos(theta)) * Sin(theta) * (b - a) / 2;
        return Adaptive(new_f, 0.0, PI, acc, eps);
    }

    public static (double, int) Integrate(Func<double, double> f, double a, double b,
                                          double acc = 1e-3, double eps = 1e-3, bool useCC = false)
    {
        // infinite bounds
        if (double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b))
        {
            Func<double, double> f_new = delegate (double x)
            {
                return f(x / (1 - x * x)) * (1 + x * x) / Pow(1 - x * x, 2);
            };
            return useCC ? (ClenshawCurtis(f_new, -1, 1, acc, eps), -1)
                         : (Adaptive(f_new, -1, 1, acc, eps), -1);
        }

        // [a, ∞)
        if (!double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b))
        {
            Func<double, double> f_new = t =>
            {
                double x = a + (1 - t) / t;
                double dx = 1 / (t * t);
                return f(x) * dx;
            };
            return useCC ? (ClenshawCurtis(f_new, 0, 1, acc, eps), -1)
                         : (Adaptive(f_new, 0, 1, acc, eps), -1);
        }

        // [−∞, b]
        if (double.IsNegativeInfinity(a) && !double.IsPositiveInfinity(b))
        {
            Func<double, double> f_new = t =>
            {
                double x = b - (1 - t) / t;
                double dx = 1 / (t * t);
                return f(x) * dx;
            };
            return useCC ? (ClenshawCurtis(f_new, 0, 1, acc, eps), -1)
                         : (Adaptive(f_new, 0, 1, acc, eps), -1);
        }

        // Regular interval
        return useCC ? (ClenshawCurtis(f, a, b, acc, eps), -1)
                     : (Adaptive(f, a, b, acc, eps), -1);
    }

    // error function 
    public static double Erf(double z, double acc = 1e-6, double eps = 1e-6)
    {
        if (z < 0.0) return -Erf(-z, acc, eps);

        if (z <= 1.0)
        {
            Func<double, double> f = x => Exp(-x * x);
            return 2.0 / Sqrt(PI) * Integrate(f, 0, z, acc, eps).Item1;
        }
        else
        {
            Func<double, double> f = x =>
                Exp(-Pow(z + (1 - x) / x, 2)) / (x * x);
            return 1.0 - 2.0 / Sqrt(PI) * Integrate(f, 0, 1, acc, eps).Item1;
        }
    }

    // Result Comparison
    public static bool Compare(double expected, double actual, double acc = 1e-3, double eps = 1e-3)
    {
        return Abs(expected - actual) < acc || Abs(expected - actual) / Max(Abs(expected), Abs(actual)) < eps;
    }
}

