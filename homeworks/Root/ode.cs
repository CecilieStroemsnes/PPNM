using System;
using static System.Math;
using System.Collections.Generic;

public static class ode {

    // Runge-Kutta 1-2 
    public static (vector, vector) rkstep12(
        Func<double, vector, vector> f,
        double x,
        vector y,
        double h
    ) {
        vector k0 = f(x, y);                   // Euler step
        vector k1 = f(x + h / 2, y + k0 * (h / 2)); // Midpoint step
        vector yh = y + k1 * h;                // Better estimate
        vector δy = (k1 - k0) * h;             // Error estimate
    return (yh, δy);
    }

    // Adaptive driver
    public static vector adaptiveDriver(
            Func<double, vector, vector> f,      // dy/dx = f(x, y)
            double a,                            // initial x-value
            vector ya,                           // initial y-vector
            double b,                            // final x-value
            genlist<double> xlist = null,        // optional: store x-values
            genlist<vector> ylist = null,        // optional: store y-values
            double h = 0.05,                     // initial step size
            double acc = 1e-3,                   // absolute accuracy goal
            double eps = 1e-3                    // relative accuracy goal
        ) {
            if (a > b) throw new Exception("Start point a must be less than end point b.");

            double x = a;
            vector y = ya.copy();

            // Store initial values if output lists are given
            if (xlist != null && ylist != null) {
                xlist.add(x);
                ylist.add(y);
            }

            while (x < b) {
                if (x + h > b) h = b - x;  // don't overshoot the endpoint

                var (yh, δy) = rkstep12(f, x, y, h);  // take a step

                double tol = (acc + eps * yh.norm()) * Sqrt(h / (b - a));  // tolerance
                double err = δy.norm();  // actual error

                if (err <= tol) {
                    // Accept step
                    x += h;
                    y = yh;

                    if (xlist != null && ylist != null) {
                        xlist.add(x);
                        ylist.add(y);
                    }
                }

                // Adjust step size
                if (err > 0)
                    h *= Min(Pow(tol / err, 0.25) * 0.95, 2.0);
                else
                    h *= 2.0;
            }

            return y;
        }
}
