using System;
using System.IO;
using System.Globalization;

class Program
{
    static void Main()
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        double acc = 1e-6, eps = 1e-6;

        using (StreamWriter writer = new StreamWriter("out.txt"))
        {
            writer.WriteLine("----------- Task A -----------\n");
            writer.WriteLine("Testing basic integrals:\n");

            writer.WriteLine($"∫ sqrt(x) dx from 0 to 1 ≈ {Integrator.Adaptive(Math.Sqrt, 0, 1, acc, eps)}, expected: {2.0 / 3}");

            writer.WriteLine($"∫ 1/sqrt(x) dx from 0 to 1 ≈ {Integrator.Adaptive(x => 1 / Math.Sqrt(x), 0, 1, acc, eps)}, expected: 2");

            writer.WriteLine($"∫ sqrt(1 - x^2) dx from 0 to 1 ≈ {Integrator.Adaptive(x => Math.Sqrt(1 - x * x), 0, 1, acc, eps)}, expected: {Math.PI / 4}");

            writer.WriteLine($"∫ ln(x)/sqrt(x) dx from 0 to 1 ≈ {Integrator.Adaptive(x => Math.Log(x) / Math.Sqrt(x), 0, 1, acc, eps)}, expected: -4");

            writer.WriteLine("\nTesting erf(z) implementation:\n");

            double z = 1.0;
            double exact = 0.84270079294971486934;

            double[] acc_list = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6, 1e-7, 1e-8 };

            writer.WriteLine("acc\t\tcomputed\t\t\terror");
            foreach (double a in acc_list)
            {
                double result = Integrator.Erf(z, a, 0); // eps = 0
                double error = Math.Abs(result - exact);
                writer.WriteLine($"{a:E1}\t{result:E17}\t{error:E2}");
            }
        }

        // write calc_erf.txt
        using (StreamWriter calcWriter = new StreamWriter("calc_erf.txt"))
        {
            for (double x = 0; x <= 2.0; x += 0.1)
            {
                double val = Integrator.Erf(x, 1e-8, 1e-8); 
                calcWriter.WriteLine($"{x} {val}");
            }
        }

        // write acc_erf.txt
        using (StreamWriter accWriter = new StreamWriter("acc_erf.txt"))
        {
                double exact = 0.84270079294971486934;
                double[] accs = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6, 1e-7, 1e-8 };

                foreach (double a in accs)
                {
                    double result = Integrator.Erf(1.0, a, 0.0);
                    double error = Math.Abs(result - exact);
                    double logAcc = Math.Log10(a);
                    double logErr = Math.Log10(error);
                    accWriter.WriteLine($"{logAcc} {logErr}");
                }
        }

        using (StreamWriter writer = File.AppendText("out.txt"))
        {
            writer.WriteLine("\n----------- Task B -----------\n");
            writer.WriteLine("Clenshaw–Curtis transformation and infinite limits\n");

            // Test 1: ∫₀¹ dx 1/√x = 2
            Func<double, double> f1 = x => x == 0 ? 0 : 1 / Math.Sqrt(x);
            double resultA1 = Integrator.Adaptive(f1, 0, 1, acc, eps);
            double resultB1 = Integrator.ClenshawCurtis(f1, 0, 1, acc, eps);
            writer.WriteLine("∫₀¹ dx 1/√x");
            writer.WriteLine($"  Adaptive:         {resultA1}");
            writer.WriteLine($"  Clenshaw–Curtis:  {resultB1}");
            writer.WriteLine($"  Exact:            2");

            // Test 2: ∫₀¹ dx ln(x)/√x = -4
            Func<double, double> f2 = x => x == 0 ? 0 : Math.Log(x) / Math.Sqrt(x);
            double resultA2 = Integrator.Adaptive(f2, 0, 1, acc, eps);
            double resultB2 = Integrator.ClenshawCurtis(f2, 0, 1, acc, eps);
            writer.WriteLine("\n∫₀¹ dx ln(x)/√x");
            writer.WriteLine($"  Adaptive:         {resultA2}");
            writer.WriteLine($"  Clenshaw–Curtis:  {resultB2}");
            writer.WriteLine($"  Exact:           -4");

            // Test 3: ∫₀^∞ dx exp(-x²) = √π / 2
            Func<double, double> f3 = x => Math.Exp(-x * x);
            double resultInf = Integrator.Integrate(f3, 0, double.PositiveInfinity, acc, eps, useCC: true).Item1;
            writer.WriteLine("\n∫₀^∞ dx exp(-x²)");
            writer.WriteLine($"  Clenshaw–Curtis (∞): {resultInf}");
            writer.WriteLine($"  Exact:               {Math.Sqrt(Math.PI) / 2}");
        }

        using (StreamWriter writer = File.AppendText("out.txt"))
        {
            writer.WriteLine("\n----------- Task C -----------\n");
            writer.WriteLine("Adaptive integration with error estimation\n");

            Func<double, double> f = x => x == 0 ? 0 : 1 / Math.Sqrt(x); // Avoid x=0 crash
            var (value, estErr) = Integrator.AdaptiveWithError(f, 0, 1);
            double exact = 2;
            double actualErr = Math.Abs(value - exact);

            writer.WriteLine($"∫₀¹ dx 1/√x ≈ {value}");
            writer.WriteLine($"Estimated error: {estErr}");
            writer.WriteLine($"Actual error:    {actualErr}");
        }

    }
}
    

