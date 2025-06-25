using System;
using static System.Console;
using static System.Math;

public static class rank1update {

    // Secular function: f(λ) = 1 + sum_i (σ * u_i^2 / (d_i - λ))
    public static double secular(double lambda, vector d, vector u, double sigma) {
        double sum = 0;
        for (int i = 0; i < d.size; i++) {
            sum += u[i]*u[i] / (d[i] - lambda);
        }
        return 1 + sigma * sum;
    }

    // Find eigenvalues by solving secular = 0 between d[i] and d[i+1]
    public static vector find_eigenvalues(vector d_in, vector u, double sigma = 1, double acc = 1e-8) {
        int n = d_in.size;
        vector d = d_in.copy();
        Array.Sort(d); // sort d ascending

        vector evals = new vector(n);
        int found = 0;

        // Search for eigenvalues between d[0] and d[n-1]
        for (int i = 0; i < n - 1; i++) {
            double left = d[i] + 1e-8;
            double right = d[i+1] - 1e-8;

            if (secular(left, d, u, sigma) * secular(right, d, u, sigma) < 0) {
                double root = bisection(lambda => secular(lambda, d, u, sigma), left, right, acc);
                evals[found++] = root;
            }
        }

        // One eigenvalue lies outside the spectrum: below d[0] or above d[n-1]
        double offset = 1.0;
        while (found < n) {
            // Check left side
            double left = d[0] - offset;
            double right = d[0] - 1e-8;
            if (secular(left, d, u, sigma) * secular(right, d, u, sigma) < 0) {
                double root = bisection(lambda => secular(lambda, d, u, sigma), left, right, acc);
                evals[found++] = root;
                break;
            }

            // Check right side
            left = d[n-1] + 1e-8;
            right = d[n-1] + offset;
            if (secular(left, d, u, sigma) * secular(right, d, u, sigma) < 0) {
                double root = bisection(lambda => secular(lambda, d, u, sigma), left, right, acc);
                evals[found++] = root;
                break;
            }

            offset *= 2;
            if (offset > 1e6) throw new Exception("Could not bracket final root");
        }

        return evals;
    }

    // Bisection root finder for secular function
    public static double bisection(Func<double, double> f, double a, double b, double acc = 1e-8) {
        double fa = f(a);
        double fb = f(b);
        if (fa * fb > 0) throw new Exception("Root not bracketed");

        while (b - a > acc) {
            double c = (a + b) / 2;
            double fc = f(c);
            if (fa * fc < 0) {
                b = c;
                fb = fc;
            } else {
                a = c;
                fa = fc;
            }
        }
        return (a + b) / 2;
    }
}
