using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using static System.Math;

class Program {
    static void Main() {

        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        using (var output = new StreamWriter("out.txt")) {
            output.WriteLine();
            output.WriteLine("=============================================================");
            output.WriteLine("Task A");
            output.WriteLine("=============================================================");
            output.WriteLine();
            output.WriteLine("Test of simple neural network using quasi-Newton minimisation");
            output.WriteLine("Activation: Gaussian wavelet, f(x) = x * exp(-x²)");
            output.WriteLine();
            output.WriteLine("Output:");
            output.WriteLine("- plot.png                : ANN fit vs. true function");
            output.WriteLine("- ann.txt                 : ANN predictions");
            output.WriteLine("- true.txt                : True values");
            output.WriteLine();

            var xdata = new List<double>();
            var ydata = new List<double>();
            for (double x = -1; x <= 1; x += 0.1) {
                xdata.Add(x);
                ydata.Add(Cos(5 * x - 1) * Exp(-x * x));
            }

            int neurons = 30;
            var net = new ann(neurons);
            net.train(xdata.ToArray(), ydata.ToArray());

            using (var annfile = new StreamWriter("ann.txt"))
            using (var truefile = new StreamWriter("true.txt")) {
                for (double x = -1; x <= 1; x += 0.05) {
                    annfile.WriteLine($"{x:F5} {net.response(x):F8}");
                    truefile.WriteLine($"{x:F5} {Cos(5 * x - 1) * Exp(-x * x):F8}");
                }
            }

            output.WriteLine();
            output.WriteLine("=============================================================");
            output.WriteLine("Task B");
            output.WriteLine("=============================================================");
            output.WriteLine();
            output.WriteLine("The ANN was trained to learn f(x) = cos(x) and can now compute:");
            output.WriteLine();
            output.WriteLine("- The first derivative    f'(x) ≈ ann'(x)");
            output.WriteLine("- The second derivative   f''(x) ≈ ann''(x)");
            output.WriteLine("- The antiderivative      ∫f(x) dx ≈ ∫ann(x)dx");
            output.WriteLine();
            output.WriteLine("Results are saved in:");
            output.WriteLine("- plot_deriv.png");
            output.WriteLine("- plot_dderiv.png");
            output.WriteLine("- plot_antideriv.png");
            output.WriteLine();
            output.WriteLine("Numeric values can be found in:");
            output.WriteLine("- ann_deriv.txt vs. true_deriv.txt");
            output.WriteLine("- ann_dderiv.txt vs. true_dderiv.txt");
            output.WriteLine("- ann_antideriv.txt vs. true_antideriv.txt");
        }

        Func<double, double> f = x => Cos(x);   
        Func<double, double> f_prime = x => -Sin(x);   
        Func<double, double> f_doubleprime = x => -Cos(x);
        Func<double, double> f_integral = x => Sin(x);  

        var xB = new List<double>();
        var yB = new List<double>();
        for (double x = -3; x <= 3; x += 0.3) {
            xB.Add(x);
            yB.Add(f(x));
        }

        var netB = new ann(40);
        netB.train(xB.ToArray(), yB.ToArray(), 1e-4, 5000);

        double dx = 0.05;
        using (var deriv = new StreamWriter("ann_deriv.txt"))
        using (var dderiv = new StreamWriter("ann_dderiv.txt"))
        using (var anti = new StreamWriter("ann_antideriv.txt"))
        using (var t_deriv = new StreamWriter("true_deriv.txt"))
        using (var t_dderiv = new StreamWriter("true_dderiv.txt"))
        using (var t_anti = new StreamWriter("true_antideriv.txt")) {
            for (double x = -3.0; x <= 3.0; x += dx) {
                double ann1 = netB.derivative(x);
                double ann2 = netB.second_derivative(x);
                double annA = netB.antiderivative(x);
                double tr1 = f_prime(x);
                double tr2 = f_doubleprime(x);
                double trA = f_integral(x);

                deriv.WriteLine($"{x:F2} {ann1:F5}");
                dderiv.WriteLine($"{x:F2} {ann2:F5}");
                anti.WriteLine($"{x:F2} {annA:F5}");
                t_deriv.WriteLine($"{x:F2} {tr1:F5}");
                t_dderiv.WriteLine($"{x:F2} {tr2:F5}");
                t_anti.WriteLine($"{x:F2} {trA:F5}");
            }
        }
    }
}
