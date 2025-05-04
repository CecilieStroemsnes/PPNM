using System;
using static System.Console;
using System.IO;
using System.Globalization;
using static System.Math;


// --------- Task A ---------
public class Program{
    public static double Linterp(double[] x, double[] y, double z) {
    int i = BinSearch(x, z);
    double dx = x[i + 1] - x[i];
    if (!(dx > 0))
        throw new Exception("uups...");
    double dy = y[i + 1] - y[i];
    return y[i] + (dy / dx) * (z - x[i]);
}

    public static int BinSearch(double[] x, double z) {
        if (z < x[0] || z > x[x.Length - 1]) 
            throw new Exception("binsearch: bad z");
        int i = 0, j = x.Length - 1;
        while (j - i > 1) {
            int mid = (i + j) / 2;
            if (z > x[mid])
                i = mid;
            else
                j = mid;
        }
        return i;
    }

    public static double LinterpInteg(double[] x, double[] y, double z) {
        if (z < x[0] || z > x[x.Length - 1])
            throw new Exception("linterpInteg: bad z");

        double integral = 0.0;
        int i = BinSearch(x, z);

        // Sum the areas for all complete intervals from x[0] to x[i]
        for (int j = 0; j < i; j++) {
            double dx = x[j + 1] - x[j];
            double area = 0.5 * dx * (y[j] + y[j + 1]); // trapezoidal area
            integral += area;
        }
        
        // Calculate the partial interval area from x[i] to z
        double dxPartial = z - x[i];
        double slope = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
        double areaPartial = y[i] * dxPartial + 0.5 * slope * dxPartial * dxPartial;
        integral += areaPartial;

        return integral;
    }

    // ------ Task B ------

    public class qspline {
        public double[] x,y,b,c;

        public qspline(double[] xs, double[] ys) {
            int n = xs.Length;
            x = (double[])xs.Clone();
            y = (double[])ys.Clone();
            b = new double[n - 1];
            c = new double[n - 1];

            double[] dx = new double[n - 1];
            double[] dy = new double[n - 1];

            for (int i = 0; i < n - 1; i++) {
                dx[i] = x[i + 1] - x[i];
                dy[i] = y[i + 1] - y[i];
            }

            c[0] = 0;
            for (int i = 1; i < n - 2; i++) {
                c[i + 1] = dy[i + 1] - dy[i] - c[i] * dx[i] * dx[i] / (dx[i + 1] * dx[i + 1]);
            }

            for (int i = 0; i < n - 1; i++) {
                b[i] = dy[i] / dx[i] - c[i] * dx[i];
            }
        }

            public double evaluate(double z) {
                int i = BinSearch(x, z);
                double dx = z - x[i];
                return y[i] + b[i] * dx + c[i] * dx * dx;
            }

            public double derivative(double z) {
                int i = BinSearch(x, z);
                double dx = z - x[i];
                return b[i] + 2 * c[i] * dx;
            }

            public double integral(double z) {
                int i = BinSearch(x, z);
                double result = 0;

                for (int j = 0; j < i; j++) {
                    double dx = x[j + 1] - x[j];
                    result += y[j] * dx + 0.5 * b[j] * dx * dx + c[j] * dx * dx * dx / 3.0;
                }

                double dz = z - x[i];
                result += y[i] * dz + 0.5 * b[i] * dz * dz + c[i] * dz * dz * dz / 3.0;
                return result;
            }

        }


    // ------- Main -------

    public static void Main() {
        var fmt = CultureInfo.InvariantCulture;
        int n = 10;
        double[] x = new double[n], y_cos = new double[n], y_sin = new double[n];
        for (int i = 0; i < n; i++) {
            x[i] = i;
            y_cos[i] = Cos(i);
            y_sin[i] = Sin(i);
        }

        using (var outFile = new StreamWriter("Out.txt")) {
            outFile.WriteLine("A. Linear spline (linear interpolation)\n");
            outFile.WriteLine("x\ty=Cos(x)");
            for (int i = 0; i < n; i++)
                outFile.WriteLine($"{x[i]}	{y_cos[i]}");
            outFile.WriteLine("\nThe linear interpolation and its integral are plotted in linear_*.svg\n");

            outFile.WriteLine("B. Quadratic spline\n");
            outFile.WriteLine("The quadratic spline for y=Sin(x) and its derivative/integral are plotted in quad_*.svg\n");
        }

        // Write data for linear spline
        using (var writer = new StreamWriter("linear_interp.txt")) {
            for (double z = 0; z <= 9; z += 0.1)
                writer.WriteLine($"{z.ToString("F3", fmt)}\t{Linterp(x, y_cos, z).ToString("F6", fmt)}\t{LinterpInteg(x, y_cos, z).ToString("F6", fmt)}");
        }

        // Write data for quadratic spline
        var qs = new qspline(x, y_sin);
        using (var writer = new StreamWriter("quad_interp.txt")) {
            for (double z = 0; z <= 9; z += 0.1) {
                double val = qs.evaluate(z);
                double der = qs.derivative(z);
                double integ = qs.integral(z);
                writer.WriteLine($"{z.ToString("F3", fmt)}\t{val.ToString("F6", fmt)}\t{der.ToString("F6", fmt)}\t{integ.ToString("F6", fmt)}");
            }
        }
    }
}






