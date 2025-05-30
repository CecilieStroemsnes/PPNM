using System;
using static System.Math;

public static class MC {
    // plain mc integrator
    public static (double, double) plainmc(Func<vector, double> f, vector a, vector b, int N) {
        int dim = a.size;
        double V = 1;
        for (int i = 0; i < dim; i++) V *= b[i] - a[i];

        double sum = 0, sum2 = 0;
        var x = new vector(dim);
        var rnd = new Random();

        for (int i = 0; i < N; i++) {
            for (int k = 0; k < dim; k++)
                x[k] = a[k] + rnd.NextDouble() * (b[k] - a[k]);

            double fx = f(x);
            sum += fx;
            sum2 += fx * fx;
        }

        double mean = sum / N, sigma = Sqrt(sum2 / N - mean * mean);
        return (mean * V, sigma * V / Sqrt(N));
    }

    // corput sequence generator
    static public double corput(int n, int b = 2){
        double q = 0, bk = 1.0 / b;
        
        while (n > 0) {
            q += (n % b) * bk;
            n /= b;
            bk /= b;
        }
        return q;
    }

    // --- Halton sequence (scaled to [a,b]) ---
    public static void halton(int n, int dim, vector k, int[] primes, vector a, vector b) {
        for (int i = 0; i < dim; i++) {
            k[i] = corput(n, primes[i]) * (b[i] - a[i]) + a[i];
        }
    }

    // --- Prime bases for Halton sequences ---
    public static int[] primes1 = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
    public static int[] primes2 = { 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89 };


    // quasi-random MC integrator
    public static (double, double) quasi_integrate(Func<vector, double> f, vector a, vector b, int N) {
        int dim = a.size;
        double V = 1;
        for (int i = 0; i < dim; i++) V *= b[i] - a[i];

        double sum1 = 0;
        double sum2 = 0;
        var x = new vector(dim);

        for (int i = 0; i < N; i++) {
            halton(i, dim, x, primes1, a, b);
            sum1 += f(x);

            halton(i, dim, x, primes2, a, b);
            sum2 += f(x);
        }

        double mean = sum1 / N;
        double error = Abs(sum1 - sum2) / N;

        return (mean * V, error * V);
    }
}