using System;
using static System.Math;
using System.Globalization;
using System.IO;

class Program {
    static void Main() {
        var unitCircle = new Func<vector, double>(v => {
            double x = v[0], y = v[1];
            return (x * x + y * y <= 1) ? 1.0 : 0.0;
        });

        var a = new vector(-1, -1);
        var b = new vector(1, 1);
        var Ns = new int[] {100, 200, 300, 400, 500, 750, 1000, 1250, 1500, 2000, 2500, 3000, 3500};
        double exact = PI;

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task A");
        Console.WriteLine("=============================================================\n");

        Console.WriteLine("Saved plain Monte Carlo errors to:");
        Console.WriteLine("  - unit_circle_estimated.txt");
        Console.WriteLine("  - unit_circle_actual.txt");

        Console.WriteLine("The plot plain_mc_plot.png compares these errors with a reference line ~1/√N.");
        Console.WriteLine();

        using (var plainEst = new StreamWriter("unit_circle_estimated.txt"))
        using (var plainAct = new StreamWriter("unit_circle_actual.txt"))
        {
            foreach (int N in Ns) {
                var (result, err) = MC.plainmc(unitCircle, a, b, N);
                plainEst.WriteLine($"{N.ToString(CultureInfo.InvariantCulture)} {err.ToString(CultureInfo.InvariantCulture)}");
                plainAct.WriteLine($"{N.ToString(CultureInfo.InvariantCulture)} {Abs(result - exact).ToString(CultureInfo.InvariantCulture)}");
            }
        }
        Console.WriteLine();
        Console.WriteLine("Estimating a known 3D integral as an additional test:");
        Console.WriteLine();

        var hardIntegral = new Func<vector, double>(v => {
            double x = v[0], y = v[1], z = v[2];
            return 1.0 / (1 - Cos(x) * Cos(y) * Cos(z));
        });

        var a3 = new vector(0, 0, 0);
        var b3 = new vector(PI, PI, PI);
        int N3D = 10_000_000;

        var (rawRes, rawErr) = MC.plainmc(hardIntegral, a3, b3, N3D);
        double res = rawRes / Pow(PI, 3);
        double err3 = rawErr / Pow(PI, 3);
        double refVal = 1.393203929685676859;

        Console.WriteLine($"Estimated:    {res:F12} ± {err3:F12}");
        Console.WriteLine($"Known value:  {refVal}");
        Console.WriteLine($"Actual error: {Abs(res - refVal):F12}");

        // Quasi-random
        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task B");
        Console.WriteLine("=============================================================\n");
        Console.WriteLine("Saved Halton (quasi-random) errors to:");
        Console.WriteLine("  - unit_circle_qrand_estimated.txt");

        Console.WriteLine("The plot quasi_mc_plot.png shows the error compared to 1/√N.");
        Console.WriteLine();

        using (var qrand = new StreamWriter("unit_circle_qrand_estimated.txt"))
        {
            foreach (int N in Ns) {
                var (qres, qerr) = MC.quasi_integrate(unitCircle, a, b, N);
                qrand.WriteLine($"{N.ToString(CultureInfo.InvariantCulture)} {qerr.ToString(CultureInfo.InvariantCulture)}");
            }
        }

    }
}
