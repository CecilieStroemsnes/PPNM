using System;
using static System.Console;
using System.IO;
using System.Globalization;

public static class QR{
    
    // QR decomposition of a matrix A

    public static (matrix, matrix) decomp(matrix A) {
    matrix Q = A.copy();
    matrix R = new matrix(A.size2, A.size2);

    /* orthogonalize Q and fill-in R */
    for (int i = 0; i < A.size2; i++) {
        R[i, i] = Q[i].norm();   // Compute the norm of the i-th column
        Q[i] /= R[i, i];         // Normalize the i-th column
        for (int j = i + 1; j < A.size2; j++) {
            R[i, j] = Q[i] % Q[j];    // Compute dot product (assuming '%' is overloaded for dot product)
            Q[j] -= R[i, j] * Q[i];     // Remove the projection from the j-th column
        }
    }

    return (Q, R);
    }

   // solve QRx=b
    public static vector solve(matrix Q, matrix R, vector b) {
    vector x = Q.T * b;  // This should compute y = Qᵀ * b
    for (int i = Q.size2 - 1; i >= 0; i--) {
        for (int j = i + 1; j < Q.size2; j++)
            x[i] -= R[i, j] * x[j];
        x[i] /= R[i, i];
    }
    return x;
    }

   // determinant of a matrix
   public static double det(matrix R){
        double det=1;
        for(int i=0; i<R.size1; i++)
            det *= R[i,i];
        return det;
   }

   public static vector solveUpperTriangular(matrix R, vector b){
        int m = b.size;
        vector x = new vector(m);
        for (int i = m - 1; i >= 0; i--){
            double sum = 0;
            for (int j = i + 1; j < m; j++){
                sum += R[i, j] * x[j];
            }
            x[i] = (b[i] - sum) / R[i, i];
        }
        return x;
    }

    // Return R⁻¹ such that cov = R⁻¹ (R⁻¹)ᵀ.
    public static matrix inverse(matrix Q, matrix R){
        int m = R.size1;  // R is m x m.
        matrix Rinv = new matrix(m, m);
        for (int i = 0; i < m; i++){
            // Create the i-th unit vector of length m.
            vector e = new vector(m);
            e[i] = 1;
            // Solve R x = e using the upper-triangular solver.
            vector x = solveUpperTriangular(R, e);
            for (int j = 0; j < m; j++){
                Rinv[j, i] = x[j];  // x becomes the i-th column of R⁻¹.
            }
        }
        return Rinv;
    }
}

// make least square fit
public static class LS{
    public static (vector, matrix) lsfit(Func<double,double>[] fs, vector x, vector y, vector dy) {
        int n = x.size;     // Number of data points
        int m = fs.Length; // Number of fitting/basis functions

        matrix A = new matrix(n, m);
        vector b = new vector(n);

        for(int i = 0; i < n; i++){
            for(int j = 0; j < m; j++)
                A[i,j] = fs[j](x[i])/dy[i];
            b[i] = y[i]/dy[i];
        }
        var (Q,R) = QR.decomp(A);
        vector c = QR.solve(Q,R,b);
        matrix A_inv = QR.inverse(Q,R);
        matrix cov = A_inv * A_inv.T;

    return (c,cov);
    }
}

// RadioactiveDecayFit
public class Program{
    static void Main(string[] args){
        double[] t = {1, 2, 3, 4, 6, 9, 10, 13, 15};
        double[] y = {117, 100, 88, 72, 53, 29.5, 25.2, 15.2, 11.1};
        double[] dy = {6, 5, 4, 4, 4, 3, 3, 2, 2};

        int n = t.Length;

        // Transform data: compute Y = ln(y) and dY = dy/y for each data point.
        double[] Y = new double[n];
        double[] dY = new double[n];
        for (int i = 0; i < n; i++){
            Y[i] = Math.Log(y[i]);
            dY[i] = dy[i] / y[i];
        }

        Func<double, double>[] fs = new Func<double, double>[] {
            (x) => 1.0,  // constant term
            (x) => x     // linear term
        };

        // Convert arrays to your vector type (as defined in your vector.cs file)
        vector t_vec = new vector(t);
        vector Y_vec = new vector(Y);
        vector dY_vec = new vector(dY);

        // Perform the weighted least squares fit.
        // Using tuple properties to avoid tuple deconstruction syntax issues.
        var result = LS.lsfit(fs, t_vec, Y_vec, dY_vec);
        vector c = result.Item1;
        matrix cov = result.Item2;

        // Extract fit parameters:
        // c[0] is ln(a), so a = exp(c[0]).
        // c[1] is -λ, so λ = -c[1].
        double ln_a = c[0];
        double c2 = c[1];
        double a_fit = Math.Exp(ln_a);
        double lambda_fit = -c2;
        double T_half = Math.Log(2) / lambda_fit;
        
        // Compute uncertainties
        double sigma_ln_a = Math.Sqrt(cov[0,0]);
        double sigma_c2   = Math.Sqrt(cov[1,1]);  // uncertainty in c2, which is uncertainty in (-λ)
        double sigma_lambda = sigma_c2;           // since λ = -c2
        // Propagate uncertainty to half-life: T_half = ln(2)/λ, so dT/dλ = -ln2/λ^2.
        double sigma_T = (Math.Log(2) / (lambda_fit * lambda_fit)) * sigma_lambda;

        // Print the results.
        Console.WriteLine("Fit results for ln(y) = ln(a) - λt:");
        Console.WriteLine("ln(a) = {0:F5} ± {1:F5}", ln_a, sigma_ln_a);
        Console.WriteLine("-λ    = {0:F5} ± {1:F5}", c2, sigma_c2);
        Console.WriteLine();
        Console.WriteLine("Interpreted parameters:");
        Console.WriteLine("a (activity factor) = {0:F5}", a_fit);
        Console.WriteLine("λ (decay constant)  = {0:F5} ± {1:F5}", lambda_fit, sigma_lambda);
        Console.WriteLine("Estimated half-life T1/2 = {0:F5} ± {1:F5} days", T_half, sigma_T);
        
        // Modern half-life for 224Ra is about 3.66 days.
        double modern_half_life = 3.66;
        Console.WriteLine();
        Console.WriteLine("Modern half-life for 224Ra: {0:F2} days", modern_half_life);
        if (Math.Abs(T_half - modern_half_life) <= sigma_T) {
            Console.WriteLine("The estimated half-life agrees with the modern value within the uncertainty.");
        } else {
            Console.WriteLine("The estimated half-life does NOT agree with the modern value within the uncertainty.");
        }
    }
}