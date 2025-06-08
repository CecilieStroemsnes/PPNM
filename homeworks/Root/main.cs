using System;
using System.IO;
using System.Globalization;
using static System.Math;

class Program {
    static void Main() {
        Console.WriteLine("Task A: Newton's method with numerical Jacobian and back-tracking line-search");
        Console.WriteLine("=============================================================");
        Console.WriteLine();
        
        vector start = new vector(2.0, 2.0);

        Func<vector, vector> gradRosenbrock = (v) => {
            double x = v[0], y = v[1];
            return new vector(
                -2*(1 - x) - 400*x*(y - x*x),
                200*(y - x*x)
            );
        };

        Console.WriteLine("Solving ∇f(x, y) = 0 for the Rosenbrock function:");
        Console.WriteLine("f(x, y) = (1 - x)² + 100(y - x²)²");
        Console.WriteLine("∂f/∂x = -2(1 - x) - 400x(y - x²)");
        Console.WriteLine("∂f/∂y = 200(y - x²)");
        Console.WriteLine();

        vector rootResult = root.newton(gradRosenbrock, start);
        Console.WriteLine("Found minimum of Rosenbrock at:");
        rootResult.print();

        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Solving ∇f(x, y) = 0 for Himmelblau's function:");
        Console.WriteLine("f(x, y) = (x² + y - 11)² + (x + y² - 7)²");
        Console.WriteLine("∂f/∂x = 4x(x² + y - 11) + 2(x + y² - 7)");
        Console.WriteLine("∂f/∂y = 2(x² + y - 11) + 4y(x + y² - 7)");
        Console.WriteLine();

        Func<vector, vector> gradHimmelblau = (v) => {
            double x = v[0], y = v[1];
            double u = x*x + y - 11;
            double v2 = x + y*y - 7;
            return new vector(
                4*x*u + 2*v2,
                2*u + 4*y*v2
            );
        };

        // Try multiple starting points to find different minima
        vector[] starts = {
            new vector(3.0, 2.0),
            new vector(-2.0, 2.0),
            new vector(-4.0, -3.0),
            new vector(3.5, -2.0)
        };

        for (int i = 0; i < starts.Length; i++) {
            vector result = root.newton(gradHimmelblau, starts[i]);
            Console.WriteLine($"Minimum #{i+1} found at:");
            result.print();
            Console.WriteLine();
        }

        Console.WriteLine("Task B: Bound states of hydrogen atom with shooting method for boundary value problems");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        double rmin = 0.001;
        double rmax = 8.0;
        double acc = 1e-6;
        double eps = 1e-6;

        genlist<double> xlist = new genlist<double>();
        genlist<vector> ylist = new genlist<vector>();

        Func<vector, vector> wavefunction_solver = (vector E_guess_vec) => {
            double E = E_guess_vec[0];

            Func<double, vector, vector> radial_SE = (r, y) => {
                double f = y[0], df = y[1];
                return new vector(df, -2 * (E + 1 / r) * f);
            };

            vector y0 = new vector(rmin - rmin*rmin, 1 - 2*rmin);
            xlist = new genlist<double>();
            ylist = new genlist<vector>();
            vector yb = ode.adaptiveDriver(radial_SE, rmin, y0, rmax, xlist, ylist, 0.01, acc, eps);
            return new vector(yb[0]);
        };

        vector E0vec = root.newton(wavefunction_solver, new vector(-1.0), 1e-6);
        double E0 = E0vec[0];
        Console.WriteLine($"Found ground state energy: E0 = {E0:F6} (expected: -0.5)\n");

        // Write numerical and exact wavefunctions
        using (var out_wave = new StreamWriter("out_hydrogen.txt")) {
            for (int i = 0; i < xlist.size; i++) {
                double r = xlist[i];
                double f_num = ylist[i][0];
                double f_exact = r * Exp(-r);
                out_wave.WriteLine($"{r.ToString(CultureInfo.InvariantCulture)} {f_num.ToString(CultureInfo.InvariantCulture)} {f_exact.ToString(CultureInfo.InvariantCulture)}");
            }
        }
        Console.WriteLine("Wrote numerical and exact wavefunctions to out_hydrogen.txt");
        Console.WriteLine("Plot of wavefunction can be found in wavefunction.png\n");

        // convergence tests

        int n = 5; // Number of steps per test
        double E_guess = -0.8;

        Console.WriteLine("\nRunning convergence tests...");

        var out_rmax = new StreamWriter("rmax.txt");
        for (int i = 0; i < n; i++) {
            double rmax_val = 4.0 + i * 1.5; // [4.0, 5.5, 7.0, 8.5, 10.0]
            double rmin_val = 0.001;
            double acc_val = 1e-6, eps_val = 1e-6;

            Func<vector, vector> test_solver = (E_guess_vec) => {
                double E = E_guess_vec[0];
                Func<double, vector, vector> radial_SE = (r, y) => new vector(y[1], -2 * (E + 1 / r) * y[0]);
                vector y0 = new vector(rmin_val - rmin_val * rmin_val, 1 - 2 * rmin_val);
                ode.adaptiveDriver(radial_SE, rmin_val, y0, rmax_val, null, null, 0.01, acc_val, eps_val);
                return new vector(ode.adaptiveDriver(radial_SE, rmin_val, y0, rmax_val)[0]);
            };

            double E_result = root.newton(test_solver, new vector(E_guess), 1e-6)[0];
            out_rmax.WriteLine($"{rmax_val.ToString(CultureInfo.InvariantCulture)} {E_result.ToString(CultureInfo.InvariantCulture)}");
        }
        out_rmax.Close();

        var out_rmin = new StreamWriter("rmin.txt");
        for (int i = 0; i < n; i++) {
            double rmin_val = 0.01 + i * 0.05; // [0.01, 0.06, ..., 0.21]
            double rmax_val = 8.0;
            double acc_val = 1e-6, eps_val = 1e-6;

            Func<vector, vector> test_solver = (E_guess_vec) => {
                double E = E_guess_vec[0];
                Func<double, vector, vector> radial_SE = (r, y) => new vector(y[1], -2 * (E + 1 / r) * y[0]);
                vector y0 = new vector(rmin_val - rmin_val * rmin_val, 1 - 2 * rmin_val);
                return new vector(ode.adaptiveDriver(radial_SE, rmin_val, y0, rmax_val)[0]);
            };

            double E_result = root.newton(test_solver, new vector(E_guess), 1e-6)[0];
            out_rmin.WriteLine($"{rmin_val.ToString(CultureInfo.InvariantCulture)} {E_result.ToString(CultureInfo.InvariantCulture)}");
        }
        out_rmin.Close();

        var out_acc = new StreamWriter("acc.txt");
        for (int i = 0; i < n; i++) {
            double acc_val = Pow(10, -2 - i); // 1e-2, 1e-3, ..., 1e-6
            double rmin_val = 0.001, rmax_val = 8.0;
            double eps_val = 1e-6;

            Func<vector, vector> test_solver = (E_guess_vec) => {
                double E = E_guess_vec[0];
                Func<double, vector, vector> radial_SE = (r, y) => new vector(y[1], -2 * (E + 1 / r) * y[0]);
                vector y0 = new vector(rmin_val - rmin_val * rmin_val, 1 - 2 * rmin_val);
                return new vector(ode.adaptiveDriver(radial_SE, rmin_val, y0, rmax_val, null, null, 0.01, acc_val, eps_val)[0]);
            };

            double E_result = root.newton(test_solver, new vector(E_guess), acc_val)[0];
            out_acc.WriteLine($"{acc_val.ToString(CultureInfo.InvariantCulture)} {E_result.ToString(CultureInfo.InvariantCulture)}");
        }
        out_acc.Close();

        var out_eps = new StreamWriter("eps.txt");
        for (int i = 0; i < n; i++) {
            double eps_val = Pow(10, -2 - i); // 1e-2, ..., 1e-6
            double rmin_val = 0.001, rmax_val = 8.0;
            double acc_val = 1e-6;

            Func<vector, vector> test_solver = (E_guess_vec) => {
                double E = E_guess_vec[0];
                Func<double, vector, vector> radial_SE = (r, y) => new vector(y[1], -2 * (E + 1 / r) * y[0]);
                vector y0 = new vector(rmin_val - rmin_val * rmin_val, 1 - 2 * rmin_val);
                return new vector(ode.adaptiveDriver(radial_SE, rmin_val, y0, rmax_val, null, null, 0.01, acc_val, eps_val)[0]);
            };

            double E_result = root.newton(test_solver, new vector(E_guess), 1e-6)[0];
            out_eps.WriteLine($"{eps_val.ToString(CultureInfo.InvariantCulture)} {E_result.ToString(CultureInfo.InvariantCulture)}");
        }
        out_eps.Close();

        Console.WriteLine("Convergence tests complete. Data saved to rmax.txt, rmin.txt, acc.txt, eps.txt.");

        Console.WriteLine("Plots of convergence tests can be found in convergence_acc.png, convergence_eps.png, convergence_rmax.png, convergence_rmin.png");



    }
}