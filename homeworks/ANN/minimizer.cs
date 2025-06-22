using System;
using static System.Math;

public static class minimizer {

    public static vector gradient(Func<vector, double> f, vector x) {
        double fx = f(x);
        vector grad = new vector(x.size);
        for (int i = 0; i < x.size; i++) {
            double dx = (1 + Abs(x[i])) * Pow(2, -26);
            x[i] += dx;
            grad[i] = (f(x) - fx) / dx;
            x[i] -= dx;
        }
        return grad;
    }

    public static (vector, int) qnewton(Func<vector, double> f, vector start, double acc = 1e-3, int maxSteps = 1000) {
        int steps = 0;
        int n = start.size;
        vector x = start.copy();
        matrix B = new matrix(n, n);
        B.setid();

        while (gradient(f, x).norm() > acc && steps < maxSteps) {
            vector grad = gradient(f, x);
            vector dx = -B * grad;

            double lambda = 1.0;
            while (true) {
                if (f(x + lambda * dx) < f(x)) {
                    vector xNew = x + lambda * dx;
                    vector gradNew = gradient(f, xNew);
                    vector y = gradNew - grad;
                    vector u = dx - B * y;
                    double uy = u % y;

                    if (Abs(uy) > Pow(2, -26)) {
                        matrix dB = matrix.outer(u, u) / uy;
                        B += dB;
                    }

                    x = xNew;
                    break;
                }

                lambda /= 2.0;
                if (lambda < 1.0 / 1024) {
                    x += lambda * dx;
                    B.setid(); // reset if we hit a wall
                    break;
                }
            }

            steps++;
        }

        return (x, steps);
    }
}