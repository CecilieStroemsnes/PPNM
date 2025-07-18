using System;
using static System.Math;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

class Program {

    // Breit-Wigner function
    static double BreitWigner(double E, double m, double gamma, double A) {
        return A / ((E - m)*(E - m) + gamma*gamma/4.0);
    }

    static Func<vector, double> MakeDeviationFunction(List<double> E, List<double> sigma, List<double> dsigma) {
        return (vector pars) => {
            double m = pars[0];
            double gamma = pars[1];
            double A = pars[2];

            // Reject unphysical values by returning a very high cost
            //if (m < 120 || m > 130 || gamma <= 0 || gamma > 5 || A <= 0 || A > 30)
            //    return 1e6;

            double sum = 0;
            for (int i = 0; i < E.Count; i++) {
                double F = A / ((E[i] - m)*(E[i] - m) + gamma*gamma/4.0);
                double diff = (F - sigma[i]) / dsigma[i];
                sum += diff * diff;
            }
            return sum;
        };
    }

    static void Main() {

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task A");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        // Rosenbrock function
        Func<vector, double> rosenbrock = v => Math.Pow(1 - v[0], 2) + 100 * Math.Pow(v[1] - v[0] * v[0], 2);

        var start1 = new vector(2.0, 2.0);
        var (min1, steps1) = Newton.Minimize(rosenbrock, start1);
        Console.WriteLine($"Rosebrock guess: {start1}");
        Console.WriteLine($"Rosenbrock minimum: {min1}, steps: {steps1}");
        Console.WriteLine();

        // Himmelblau function
        Func<vector, double> himmelblau = v => Math.Pow(v[0]*v[0] + v[1] - 11, 2) + Math.Pow(v[0] + v[1]*v[1] - 7, 2);

        var start2 = new vector(-2.0, 2.0);
        var (min2, steps2) = Newton.Minimize(himmelblau, start2);
        Console.WriteLine($"Himmelblau guess: {start2}");
        Console.WriteLine($"Himmelblau minimum: {min2}, steps: {steps2}");

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task B");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        var energy = new List<double> {
            101, 103, 105, 107, 109, 111, 113, 115, 117, 119,
            121, 123, 125, 127, 129, 131, 133, 135, 137, 139,
            141, 143, 145, 147, 149, 151, 153, 155, 157, 159
        };
        
        var signal = new List<double> {
            -0.25, -0.30, -0.15, -1.71, 0.81, 0.65, -0.91, 0.91, 0.96, -2.52,
            -1.01, 2.01, 4.83, 4.58, 1.26, 1.01, -1.26, 0.45, 0.15, -0.91,
            -0.81, -1.41, 1.36, 0.50, -0.45, 1.61, -2.21, -1.86, 1.76, -0.50
        };
        
        var error = new List<double> {
            2.0, 2.0, 1.9, 1.9, 1.9, 1.9, 1.9, 1.9, 1.6, 1.6,
            1.6, 1.6, 1.6, 1.6, 1.3, 1.3, 1.3, 1.3, 1.3, 1.3,
            1.1, 1.1, 1.1, 1.1, 1.1, 1.1, 1.1, 0.9, 0.9, 0.9
        };

        using (var writer = new StreamWriter("data.txt")) {
            for (int i = 0; i < energy.Count; i++) {
                writer.WriteLine($"{energy[i].ToString(CultureInfo.InvariantCulture)} {signal[i].ToString(CultureInfo.InvariantCulture)} {error[i].ToString(CultureInfo.InvariantCulture)}");
            }
        }

        // Deviation function D(m, gamma, A)
        var D = MakeDeviationFunction(energy, signal, error);
       
        // Initial guess for parameters: m, Gamma, A
        var start3 = new vector(126.0, 2.0, 10.0);
        var (best, steps) = Newton.Minimize(D, start3, acc: 1e-10, maxSteps: 5000);
       
        //Console.WriteLine($"Initial D = {D(start3):F4}");
        //Console.WriteLine($"Final   D = {D(best):F4}");

        Console.WriteLine($"Best fit parameters:");
        Console.WriteLine($"Mass  = {best[0]:F4} GeV");
        Console.WriteLine($"Width = {best[1]:F4} GeV");
        Console.WriteLine($"A     = {best[2]:F4}");
        Console.WriteLine($"Steps = {steps}");

        using (var fit = new StreamWriter("fit.txt")) {
            for (double e = 100; e <= 160; e += 0.1) {
                double f = BreitWigner(e, best[0], best[1], best[2]);
                fit.WriteLine($"{e.ToString(CultureInfo.InvariantCulture)} {f.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        Console.WriteLine("Fit data written to fit.txt");
        Console.WriteLine("Data points written to data.txt");
        Console.WriteLine("A plot of the fit can be found in higgs_fit.png.");

        var gradNorm = Newton.Gradient(D, best).norm();
        //Console.WriteLine($"Final gradient norm: {gradNorm:F6}");

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task C");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        Func<vector, double> testFunc = v => Math.Pow(1 - v[0], 2) + 100 * Math.Pow(v[1] - v[0] * v[0], 2); // Rosenbrock

        var start4 = new vector(1.0, 1.0);

        var gradFwd = Newton.Gradient(testFunc, start4);
        var gradCen = Newton.GradientCentral(testFunc, start4);

        Console.WriteLine("Comparing gradient approximations at the known minimum (x, y) = (1, 1):");
        Console.WriteLine("Gradient using forward difference:");
        gradFwd.print();
        Console.WriteLine("Gradient using central difference:");
        gradCen.print();
        Console.WriteLine("→ The central difference gives a gradient closer to zero, as expected at a minimum.");
        Console.WriteLine();

        var hessFwd = Newton.Hessian(testFunc, start4);
        var hessCen = Newton.HessianCentral(testFunc, start4);

        Console.WriteLine("Comparing Hessian approximations at the same point:");
        Console.WriteLine("Hessian using forward difference:");
        hessFwd.print();
        Console.WriteLine("Hessian using central difference:");
        hessCen.print();
        Console.WriteLine("→ The central difference gives a symmetric and more accurate Hessian.");
        Console.WriteLine();



    }

}

