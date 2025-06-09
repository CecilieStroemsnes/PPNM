using System;
using System.IO;
using System.Globalization;

class Program
{
    static void Main()
    {

        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        double acc = 1e-6, eps = 1e-6;

        Console.WriteLine("\n=============================================================");
        Console.WriteLine("Task A");
        Console.WriteLine("=============================================================\n");
        Console.WriteLine("Testing adaptive integrator on basic functions with known exact results:\n");

        Console.WriteLine($"∫ sqrt(x) dx from 0 to 1 ≈ {Integrator.Adaptive(Math.Sqrt, 0, 1, acc, eps):F4}, expected: {(2.0 / 3):F4}");
        Console.WriteLine($"∫ 1/sqrt(x) dx from 0 to 1 ≈ {Integrator.Adaptive(x => x == 0 ? 0 : 1 / Math.Sqrt(x), 0, 1, acc, eps):F2}, expected: 2");
        Console.WriteLine($"∫ sqrt(1 - x^2) dx from 0 to 1 ≈ {Integrator.Adaptive(x => Math.Sqrt(1 - x * x), 0, 1, acc, eps):F4}, expected: {(Math.PI / 4):F4}");
        Console.WriteLine($"∫ ln(x)/sqrt(x) dx from 0 to 1 ≈ {Integrator.Adaptive(x => x == 0 ? 0 : Math.Log(x) / Math.Sqrt(x), 0, 1, acc, eps):F2}, expected: -4");

        Console.WriteLine("\nTesting error function erf(z) implementation for z = 1.0:\n");
        double z = 1.0;
        double exact = 0.84270079294971486934;
        double[] acc_list = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6, 1e-7, 1e-8 };

        Console.WriteLine("acc\t\tcomputed\t\t\t\terror");
        foreach (double a in acc_list)
        {
            double result = Integrator.Erf(z, a, 0); // eps = 0
            double error = Math.Abs(result - exact);
            Console.WriteLine($"{a:E1}\t{result:E6}\t{error:E2}");
        }

        // Write calc_erf.txt for comparison plot
        using (var calcWriter = new System.IO.StreamWriter("calc_erf.txt"))
        {
            for (double x = 0; x <= 2.0; x += 0.1)
            {
                double val = Integrator.Erf(x, 1e-8, 1e-8); 
                calcWriter.WriteLine($"{x} {val}");
            }
        }

        // Write acc_erf.txt for log-log plot of error vs. accuracy
        using (var accWriter = new System.IO.StreamWriter("acc_erf.txt"))
        {
            foreach (double a in acc_list)
            {
                double result = Integrator.Erf(1.0, a, 0.0);
                double error = Math.Abs(result - exact);
                double logAcc = Math.Log10(a);
                double logErr = Math.Log10(error);
                accWriter.WriteLine($"{logAcc} {logErr}");
            }
        }

        Console.WriteLine("\nTwo plots have been generated for Task A:");
        Console.WriteLine("     plot_erf.png shows computed erf(x) vs. tabulated values.");
        Console.WriteLine("     plot_acc.png shows how the absolute error in erf(1) decreases with tighter accuracy goals (log-log plot).");


        Console.WriteLine("\n=============================================================");
        Console.WriteLine("Task B");
        Console.WriteLine("=============================================================\n");
        Console.WriteLine("This task compares regular adaptive integration and Clenshaw–Curtis transformation on difficult integrals:\n");

        Func<double, double> f1 = x => x == 0 ? 0 : 1 / Math.Sqrt(x);
        double resultA1 = Integrator.Adaptive(f1, 0, 1, acc, eps);
        double resultB1 = Integrator.ClenshawCurtis(f1, 0, 1, acc, eps);
        Console.WriteLine("∫₀¹ dx 1/√x");
        Console.WriteLine($"  Adaptive:         {resultA1}");
        Console.WriteLine($"  Clenshaw–Curtis:  {resultB1}");
        Console.WriteLine($"  Exact:            2\n");

        Func<double, double> f2 = x => x == 0 ? 0 : Math.Log(x) / Math.Sqrt(x);
        double resultA2 = Integrator.Adaptive(f2, 0, 1, acc, eps);
        double resultB2 = Integrator.ClenshawCurtis(f2, 0, 1, acc, eps);
        Console.WriteLine("∫₀¹ dx ln(x)/√x");
        Console.WriteLine($"  Adaptive:         {resultA2}");
        Console.WriteLine($"  Clenshaw–Curtis:  {resultB2}");
        Console.WriteLine($"  Exact:           -4\n");

        Func<double, double> f3 = x => Math.Exp(-x * x);
        double resultInf = Integrator.Integrate(f3, 0, double.PositiveInfinity, acc, eps, useCC: true).Item1;
        Console.WriteLine("∫₀^∞ dx exp(-x²)");
        Console.WriteLine($"  Clenshaw–Curtis (∞): {resultInf}");
        Console.WriteLine($"  Exact:               {Math.Sqrt(Math.PI) / 2}\n");

        // ========== TASK C ==========
        Console.WriteLine("\n=============================================================");
        Console.WriteLine("Task C");
        Console.WriteLine("=============================================================\n");
        Console.WriteLine("We now use the AdaptiveWithError method to estimate the integration error.\n");

        var (value, estErr) = Integrator.AdaptiveWithError(f1, 0, 1);
        double actualErr = Math.Abs(value - 2);
        Console.WriteLine("∫₀¹ dx 1/√x using AdaptiveWithError");
        Console.WriteLine($"  Value:            {value}");
        Console.WriteLine($"  Estimated error:  {estErr}");
        Console.WriteLine($"  Actual error:     {actualErr}\n");

    }
}
    

