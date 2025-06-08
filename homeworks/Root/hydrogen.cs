using System;
using static System.Math;

public static class hydrogen {

    // Create ODE system for given energy E
    public static Func<double, vector, vector> schrodingerODE(double E) {
        return (r, y) => {
            double f = y[0], df = y[1];
            vector dydr = new vector(2);
            dydr[0] = df;
            dydr[1] = -2 * (E + 1 / r) * f;
            return dydr;
        };
    }

    // The shooting function M(E) = f_E(rmax)
    public static double shoot(double E, double rmin, double rmax, double acc=1e-6, double eps=1e-6) {
        vector y0 = new vector(2);
        y0[0] = rmin - rmin*rmin;  // f(rmin)
        y0[1] = 1 - 2*rmin;        // f'(rmin) ≈ d/dr [r - r^2] = 1 - 2r

        var f = schrodingerODE(E);
        vector yb = ode.adaptiveDriver(f, rmin, y0, rmax, null, null, 0.01, acc, eps);
        return yb[0];  // return f(rmax)
    }

    // Bisection root finder for M(E)
    public static double findE0(double rmin, double rmax, double Emin, double Emax, double acc=1e-6) {
        double M1 = shoot(Emin, rmin, rmax);
        double M2 = shoot(Emax, rmin, rmax);
        if (M1 * M2 > 0) throw new Exception("Root not bracketed");

        while (Abs(Emax - Emin) > acc) {
            double Emid = 0.5 * (Emin + Emax);
            double Mmid = shoot(Emid, rmin, rmax);

            if (M1 * Mmid < 0) {
                Emax = Emid;
                M2 = Mmid;
            } else {
                Emin = Emid;
                M1 = Mmid;
            }
        }

        return 0.5 * (Emin + Emax);
    }
}
