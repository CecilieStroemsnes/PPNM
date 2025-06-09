using System;
using System.Collections.Generic;
using static System.Math;
using System.IO;
using System.Globalization;


class main {
    // Simple harmonic oscillator: y'' = -y
    static Func<double, vector, vector> SHO = (x, y) => new vector(
        y[1],
        -y[0]
    );

    // Damped oscillator: y'' = -b y' - c sin(y)
    static Func<double, vector, vector> DAMP = (x, y) => {
        double b = 0.25, c = 5;
        return new vector(y[1], -b * y[1] - c * Sin(y[0]));
    };

    // Newtonian orbit: u'' = 1 - u
    static Func<double, vector, vector> NEWTON = (phi, u) => new vector(
        u[1],
        1 - u[0]
    );

    // Relativistic orbit: u'' = 1 - u + ε u²
    static Func<double, vector, vector> RELATIVISTIC = (phi, u) => {
        double eps = 0.01;
        return new vector(u[1], 1 - u[0] + eps * u[0] * u[0]);
    };

    static void Main() {

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task A");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        Console.WriteLine("Simple Harmonic Oscillator:");
        
        // -------------- SHO ----------------
        var xs = new genlist<double>();
        var ys = new genlist<vector>();

        ode.adaptiveDriver(SHO, 0, new vector(0, 1), 4 * PI, xs, ys);

        Console.WriteLine("Steps taken: {0}", xs.size);
        //Console.WriteLine("Final x: {0}", xs[xs.size - 1]);
        //Console.WriteLine("Final y(x): {0}", ys[ys.size - 1][0]);
        Console.WriteLine("Figure: sho.png");
        Console.WriteLine();

        using (var writer = new StreamWriter("sho.txt")) {
            for (int i = 0; i < xs.size; i++)
                writer.WriteLine($"{xs[i].ToString(CultureInfo.InvariantCulture)} {ys[i][0].ToString(CultureInfo.InvariantCulture)}");
        }

        Console.WriteLine("Damped Harmonic Oscillator:");

        // -------------- Damped Oscillator ----------------
        xs = new genlist<double>();
        ys = new genlist<vector>();

        ode.adaptiveDriver(DAMP, 0, new vector(0, 1), 4 * PI, xs, ys);

        Console.WriteLine("Steps taken: {0}", xs.size);
        //Console.WriteLine("Final x: {0}", xs[xs.size - 1]);
        //Console.WriteLine("Final y(x): {0}", ys[ys.size - 1][0]);
        Console.WriteLine("Figure: damped.png");
        Console.WriteLine();

        using (var writer = new StreamWriter("damped.txt")) {
            for (int i = 0; i < xs.size; i++)
                writer.WriteLine($"{xs[i].ToString(CultureInfo.InvariantCulture)} {ys[i][0].ToString(CultureInfo.InvariantCulture)}");
        }

        // task B ------------------------------
        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Task B");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        Console.WriteLine("Newtonian circular orbit:");
        // -------------- Newtonian Circular Orbit ----------------
        xs = new genlist<double>();
        ys = new genlist<vector>();

        ode.adaptiveDriver(NEWTON, 0, new vector(1, 0.01), 12 * PI, xs, ys);
        Console.WriteLine("Steps taken: {0}", xs.size);
        //Console.WriteLine("Final φ: {0}", xs[xs.size - 1]);
        //Console.WriteLine("Final u(φ): {0}", ys[ys.size - 1][0]);
        Console.WriteLine("Figure: circular_orbit.png");
        Console.WriteLine();

        using (var writer = new StreamWriter("circular_orbit.txt")) {
            for (int i = 0; i < xs.size; i++) {
                double r = 1 / ys[i][0];
                double x = r * Cos(xs[i]);
                double y = r * Sin(xs[i]);
                writer.WriteLine($"{x.ToString(CultureInfo.InvariantCulture)} {y.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        Console.WriteLine("Newtonian Elliptical Orbit:");
        // -------------- Newtonian Orbit ----------------
        xs = new genlist<double>();
        ys = new genlist<vector>();

        double u0 = 1;
        double uprime0 = -0.5;

        ode.adaptiveDriver(NEWTON, 0, new vector(u0, uprime0), 4 * PI, xs, ys);

        Console.WriteLine("Steps taken: {0}", xs.size);
        //Console.WriteLine("Final φ: {0}", xs[xs.size - 1]);
        //Console.WriteLine("Final u(φ): {0}", ys[ys.size - 1][0]);
        Console.WriteLine("Figure: newton_orbit.png");
        Console.WriteLine();

        using (var writer = new StreamWriter("newton_orbit.txt")) {
            for (int i = 0; i < xs.size; i++) {
                double r = 1 / ys[i][0];
                double x = r * Cos(xs[i]);
                double y = r * Sin(xs[i]);
                writer.WriteLine($"{x.ToString(CultureInfo.InvariantCulture)} {y.ToString(CultureInfo.InvariantCulture)}");
            }
        }

        Console.WriteLine("Relativistic Orbit with Precession:");

        // -------------- Relativistic Orbit ----------------
        xs = new genlist<double>();
        ys = new genlist<vector>();

        ode.adaptiveDriver(RELATIVISTIC, 0, new vector(u0, uprime0), 12 * PI, xs, ys);

        Console.WriteLine("Steps taken: {0}", xs.size);
        //Console.WriteLine("Final φ: {0}", xs[xs.size - 1]);
        //Console.WriteLine("Final u(φ): {0}", ys[ys.size - 1][0]);
        Console.WriteLine("Figure: relativistic_orbit.png");
        Console.WriteLine();

        using (var writer = new StreamWriter("relativistic_orbit.txt")) {
            for (int i = 0; i < xs.size; i++) {
                double r = 1 / ys[i][0];
                double x = r * Cos(xs[i]);
                double y = r * Sin(xs[i]);
                writer.WriteLine($"{x.ToString(CultureInfo.InvariantCulture)} {y.ToString(CultureInfo.InvariantCulture)}");
            }
        }
    }
}
