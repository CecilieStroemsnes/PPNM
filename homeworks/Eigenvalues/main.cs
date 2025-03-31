using System;
using System.IO;
using System.Globalization;
using System.Diagnostics;

public class main {
    public static void Main(string[] args) {

        System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        // Optional Task C: check Jacobi scaling O(n³)
        if (args.Length > 0 && args[0] == "-scaling") {
        using (var writer = new StreamWriter("scaling_times.txt")) {
            writer.WriteLine("# n time_seconds");
            for (int n_scaling = 20; n_scaling <= 200; n_scaling += 20) {
                matrix A_scaling = new matrix(n_scaling, n_scaling);
                var rnd_scaling = new Random();
                for (int i = 0; i < n_scaling; i++)
                    for (int j = i; j < n_scaling; j++)
                        A_scaling[i, j] = A_scaling[j, i] = rnd_scaling.NextDouble();

                var sw = Stopwatch.StartNew();
                var result = jacobi.cyclic(A_scaling);
                sw.Stop();

                double seconds = sw.Elapsed.TotalSeconds;
                writer.WriteLine($"{n_scaling} {seconds:F6}");
                Console.WriteLine($"n={n_scaling}, time={seconds:F4}s");
            }
        }
        return;
        }


        // Task A

        Console.WriteLine("Task A: Jacobi diagonalization with cyclic sweeps");
        
        int n = 5;
        matrix A = new matrix(n,n);
        var rnd = new Random(1);

        // make A symmetric
        for (int i = 0; i < n; i++)
            for (int j = i; j < n; j++)
                A[i,j] = A[j,i] = rnd.NextDouble();
        
        Console.WriteLine("A = ");
        A.print();

        (vector w, matrix V) = jacobi.cyclic(A);

        Console.WriteLine("\nEigenvalue, w = ");
        w.print();

        Console.WriteLine("\nEigenvector, V = ");
        V.print();

        matrix D = new matrix(n,n);
        for (int i = 0; i < n; i++)
            D[i,i] = w[i];

        matrix Vt = V.transpose();
        matrix VtAV = Vt*A*V;
        matrix VDVt = V*D*Vt;
        matrix VtV = Vt*V;
        matrix VVt = V*Vt;

        Console.WriteLine("\nCheck V^T A V = D");
        VtAV.print();

        Console.WriteLine("\nCheck V D V^T = A");
        VDVt.print();

        Console.WriteLine("\nCheck V^T V = I");
        VtV.print();

        Console.WriteLine("\nCheck V V^T = I");
        VVt.print();


        // Task B
        Console.WriteLine("\nTask B: Hydrogen atom, s-wave radial Schrödinger equation on a grid");

        double rmax = 10, dr = 0.3;

        for (int i = 0; i < args.Length -1; i++) {
            if (args[i] == "-rmax") rmax = double.Parse(args[i+1]);
            if (args[i] == "-dr") dr = double.Parse(args[i+1]);
        }

        int npoints = (int)(rmax/dr) - 1;
        vector r = new vector(npoints);
        for(int i=0;i<npoints;i++) r[i]=dr*(i+1);

        matrix H = new matrix(npoints,npoints);
        for(int i=0;i<npoints-1;i++){
        H[i,i]  =-2*(-0.5/dr/dr);
        H[i,i+1]= 1*(-0.5/dr/dr);
        H[i+1,i]= 1*(-0.5/dr/dr);
        }
        
        H[npoints-1,npoints-1]=-2*(-0.5/dr/dr);
        
        for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];

        Console.WriteLine($"Using rmax={rmax}, dr={dr}, npoints={npoints}");
        (vector energies, matrix wavefunctions) = jacobi.cyclic(H);

        energies.print("Lowest hydrogen eigenvalues (ε₀, ε₁, ...):");


        // Convergence test dr
        using (var writer = new StreamWriter("convergence_dr.txt")) {
            writer.WriteLine("# dr    ε0");
            for (double dr_test = 0.5; dr_test >= 0.05; dr_test -= 0.05) {
                int np = (int)(10 / dr_test) - 1;
                vector r_conv = new vector(np);
                for (int i = 0; i < np; i++) r_conv[i] = dr_test * (i + 1);

                matrix H_conv = new matrix(np, np);
                for (int i = 0; i < np - 1; i++) {
                    H_conv[i, i] = -2 * (-0.5 / dr_test / dr_test);
                    H_conv[i, i + 1] = 1 * (-0.5 / dr_test / dr_test);
                    H_conv[i + 1, i] = 1 * (-0.5 / dr_test / dr_test);
                }
                H_conv[np - 1, np - 1] = -2 * (-0.5 / dr_test / dr_test);
                for (int i = 0; i < np; i++) H_conv[i, i] += -1 / r_conv[i];

                (vector e_conv, matrix V_conv) = jacobi.cyclic(H_conv);
                writer.WriteLine($"{dr_test:f5} {e_conv[0]:f8}");
            }
        }

        // Convergence test rmax
        using (var writer = new StreamWriter("convergence_rmax.txt")) {
            writer.WriteLine("# rmax    ε0");
            for (double rmax_test = 5; rmax_test <= 20; rmax_test += 2.5) {
                double dr_fixed = 0.2;
                int np = (int)(rmax_test / dr_fixed) - 1;
                vector r_conv = new vector(np);
                for (int i = 0; i < np; i++) r_conv[i] = dr_fixed * (i + 1);

                matrix H_conv = new matrix(np, np);
                for (int i = 0; i < np - 1; i++) {
                    H_conv[i, i] = -2 * (-0.5 / dr_fixed / dr_fixed);
                    H_conv[i, i + 1] = 1 * (-0.5 / dr_fixed / dr_fixed);
                    H_conv[i + 1, i] = 1 * (-0.5 / dr_fixed / dr_fixed);
                }
                H_conv[np - 1, np - 1] = -2 * (-0.5 / dr_fixed / dr_fixed);
                for (int i = 0; i < np; i++) H_conv[i, i] += -1 / r_conv[i];

                (vector e_conv, matrix V_conv) = jacobi.cyclic(H_conv);
                writer.WriteLine($"{rmax_test:f5} {e_conv[0]:f8}");
            }
        }

        // wavefunctions
        using (var writer = new StreamWriter("wavefunctions.txt")) {
            double norm = 1.0 / Math.Sqrt(dr);
            writer.WriteLine("# r f0(r) f1(r) f2(r)");
            for (int i = 0; i < npoints; i++) {
                double rval = r[i];
                double f0 = norm * wavefunctions[i, 0];
                double f1 = norm * wavefunctions[i, 1];
                double f2 = norm * wavefunctions[i, 2];
                writer.WriteLine($"{rval:f5} {f0:f8} {f1:f8} {f2:f8}");
            }
        }
        Console.WriteLine("Saved wavefunctions to wavefunctions.txt");

    }

}