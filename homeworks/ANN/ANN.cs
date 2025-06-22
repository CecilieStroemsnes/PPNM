using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;
using System.Linq;

public class ann {
    public int n; // number of neurons
    public List<double> parameters = new List<double>();

    // Activation function and its derivative
    public Func<double, double> f = x => x * Exp(-x * x); // gaussian wavelet
    public Func<double, double> df = x => Exp(-x * x) * (1 - 2 * x * x); // derivative
    public Func<double, double> ddf = x => 2 * x * Exp(-x * x) * (2 * x * x - 3); // second derivative
    public Func<double, double> F = x => -0.5 * Exp(-x * x); // antiderivative

    // Constructor
    public ann(int n) {
        this.n = n;
        for (int i = 0; i < 3 * n; i++) parameters.Add(0);
    }

    // Setters and getters
    public double a(int i) => parameters[i];
    public double b(int i) => parameters[i + n];
    public double w(int i) => parameters[i + 2 * n];
    public void set_a(int i, double x) => parameters[i] = x;
    public void set_b(int i, double x) => parameters[i + n] = x;
    public void set_w(int i, double x) => parameters[i + 2 * n] = x;

    // Response function
    public double response(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++) {
            double zi = (x - a(i)) / b(i);
            sum += w(i) * f(zi);
        }
        return sum;
    }

    // Cost function
    public double cost(double[] xdata, double[] ydata) {
        double sum = 0;
        for (int i = 0; i < xdata.Length; i++) {
            double diff = response(xdata[i]) - ydata[i];
            sum += diff * diff;
        }
        return sum / xdata.Length;
    }

    // Train function
    public void train(double[] xdata, double[] ydata, double acc = 1e-3, int maxSteps = 5000) {
        Random rand = new Random();
        for (int i = 0; i < n; i++) {
            //set_a(i, xdata[0] + (xdata[xdata.Length - 1] - xdata[0]) * i / (n - 1));
            double xmin = xdata.Min(), xmax = xdata.Max();
            set_a(i, xmin + (xmax - xmin) * i / (n - 1));
            //set_b(i, 0.5 + rand.NextDouble());
            set_b(i, 1.0); 
            set_w(i, rand.NextDouble() * 2 - 1);
        }

        vector p0 = new vector(parameters.ToArray());

        Func<vector, double> cost = (vector p) => {
            var net = new ann(n);
            for (int i = 0; i < 3 * n; i++) net.parameters[i] = p[i];
            return net.cost(xdata, ydata);
        };

        (vector pmin, int steps) = minimizer.qnewton(cost, p0, acc, maxSteps);
        for (int i = 0; i < 3 * n; i++) parameters[i] = pmin[i];

        Console.Error.WriteLine($"Training complete in {steps} steps.");
    }

    public double derivative(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++) {
            double ai = a(i);
            double bi = b(i);
            double wi = w(i);
            double zi = (x - ai) / bi;
            sum += wi * df(zi) / bi;
        }
        return sum;
    }

    public double second_derivative(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++) {
            double ai = a(i);
            double bi = b(i);
            double wi = w(i);
            double zi = (x - ai) / bi;
            sum += wi * ddf(zi) / (bi * bi);
        }
        return sum;
    }

    public double antiderivative(double x) {
        double sum = 0;
        for (int i = 0; i < n; i++) {
        double ai = a(i);
        double bi = b(i);
        double wi = w(i);
            double zi = (x - ai) / bi;
            sum += wi * bi * F(zi);
        }
        return sum;
    }

}

public class QRdecomp {
    private matrix Q, R;
    public QRdecomp(matrix A) {
        int n = A.size1, m = A.size2;
        Q = new matrix(n, m);
        R = new matrix(m, m);
        for (int j = 0; j < m; j++) {
            vector v = A.column(j);
            for (int i = 0; i < j; i++) {
                R[i, j] = Q.column(i).dot(v);
                v -= Q.column(i) * R[i, j];
            }
            R[j, j] = v.norm();
            Q.set_col(j, v / R[j, j]);
        }
    }
    public vector solve(vector b) {
        vector y = Q.transpose() * b;
        int n = R.size2;
        vector x = new vector(n);
        for (int i = n - 1; i >= 0; i--) {
            double s = y[i];
            for (int k = i + 1; k < n; k++)
                s -= R[i, k] * x[k];
            x[i] = s / R[i, i];
        }
        return x;
    }
}
