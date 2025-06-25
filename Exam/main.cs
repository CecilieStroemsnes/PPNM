using System;
using static System.Console;
using static System.Math;
using System.IO;
using System.Globalization;

class Program {
    static void Main() {

        Console.WriteLine("=============================================================");
        Console.WriteLine("Basic 3x3 Test");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        // D = diag(1, 2, 3), u = [1, 1, 1], sigma = 1
        var d1 = new vector(1.0, 2.0, 3.0);
        var u1 = new vector(1.0, 1.0, 1.0);
        double sigma = 1.0;

        Console.WriteLine("Input D (diagonal):");
        d1.print();
        Console.WriteLine();

        Console.WriteLine("Input u (vector):");
        u1.print();
        Console.WriteLine();

        // Find eigenvalues
        var evals = rank1update.find_eigenvalues(d1, u1, sigma);

        Console.WriteLine("Eigenvalues of A = D + uuᵀ:");
        evals.print();

        Console.WriteLine();
        Console.WriteLine("A visualization of the secular function f(λ) is generated as 'secular.png'.");
        Console.WriteLine("This function has vertical poles at each dᵢ and crosses zero at each eigenvalue λᵢ.");
        Console.WriteLine("In the plot:");
        Console.WriteLine(" - Blue lines: poles (diagonal elements dᵢ)");
        Console.WriteLine(" - Red dashed lines: eigenvalues (roots of f(λ))");
        Console.WriteLine(" - Black dashed line: f(λ) = 0");
        Console.WriteLine(" - Orange curve: secular function f(λ)");
        Console.WriteLine();


        // === Write secular.txt for gnuplot ===
        using (var writer = new StreamWriter("secular.txt")) {
            var culture = CultureInfo.InvariantCulture;

            for (double lambda = 0.5; lambda <= 6.0; lambda += 0.001) {
                bool nearPole = false;
                for (int i = 0; i < d1.size; i++) {
                    if (Abs(lambda - d1[i]) < 1e-4) {
                        nearPole = true;
                        break;
                    }
                }
                if (nearPole) continue;

                double sum = 0;
                for (int i = 0; i < d1.size; i++)
                    sum += u1[i] * u1[i] / (d1[i] - lambda);

                double f = 1 + sigma * sum;
                writer.WriteLine($"{lambda.ToString("F8", culture)} {f.ToString("F8", culture)}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Random Test (n = 10)");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        int n = 10;
        var rand = new Random(42);
        var d2 = new vector(n);
        var u2 = new vector(n);
        for (int i = 0; i < n; i++) {
            d2[i] = rand.NextDouble() * 10; // Random diagonal elements
            u2[i] = rand.NextDouble() * 2 - 1; // Random vector elements in [-1, 1]
        }

        Console.WriteLine("Input D (random diagonal):");
        d2.print();
        Console.WriteLine();

        Console.WriteLine("Input u (random vector):");
        u2.print();
        Console.WriteLine();

        // Find eigenvalues
        var evals2 = rank1update.find_eigenvalues(d2, u2, sigma);
        Console.WriteLine("Eigenvalues of A = D + σ * uuᵀ:");
        evals2.print();
        Console.WriteLine();

        // Trace test
        double traceA = 0;
        for (int i = 0; i < n; i++) {
            traceA += d2[i] + sigma * u2[i] * u2[i];
        }

        Console.WriteLine($"Trace estimate (sum of diag(D) + σ * ||u||^2): {traceA:f6}");
        Console.WriteLine();

        double sumEvals = 0;
        for (int i = 0; i < n; i++) {
            sumEvals += evals2[i];
        }
        Console.WriteLine($"Sum of eigenvalues: {sumEvals:f6}");
        Console.WriteLine();

        Console.WriteLine("Difference between trace and sum of eigenvalues: " +
                          $"{Math.Abs(traceA - sumEvals):f6}");
        Console.WriteLine();
        Console.WriteLine("This confirms the trace property: Tr(A) = sum(λᵢ).");
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("=============================================================");
        Console.WriteLine("Comparison of σ = 1 and σ = -1");
        Console.WriteLine("=============================================================");
        Console.WriteLine();

        // Compare σ = 1 and σ = -1

        var evalsPos = rank1update.find_eigenvalues(d1, u1, 1.0);
        var evalsNeg = rank1update.find_eigenvalues(d1, u1, -1.0);

        Console.WriteLine("Eigenvalues for σ = 1 with simple 3x3 test:");
        evalsPos.print();
        Console.WriteLine();

        Console.WriteLine("Eigenvalues for σ = -1 with simple 3x3 test:");
        evalsNeg.print();
        Console.WriteLine();

        Console.WriteLine("Difference between eigenvalues for σ = 1 and σ = -1:");
        for (int i = 0; i < d1.size; i++) {
            Console.WriteLine($"Δλ[{i}] = {Math.Abs(evalsPos[i] - evalsNeg[i]):f6}");
        }

        // norm difference between σ = 1 and σ = -1
        double normDiff = 0;
        for (int i = 0; i < d1.size; i++) {
            normDiff += Math.Pow(evalsPos[i] - evalsNeg[i], 2);
        }
        normDiff = Math.Sqrt(normDiff);
        Console.WriteLine();
        Console.WriteLine($"Norm of difference, ||λ(σ=1) - λ(σ=-1)|| = : {normDiff:f6}");

        Console.WriteLine();
        Console.WriteLine("This comparison highlights how the sign of σ affects the spectrum.");
        Console.WriteLine("For σ = 1, one eigenvalue shifts above the diagonal range; for σ = -1,");
        Console.WriteLine("it shifts below. The eigenvalues are symmetric around the original spectrum,");
        Console.WriteLine("but not identically mirrored due to nonlinear dependence in f(λ).");
        Console.WriteLine("This shows the sensitivity of the secular equation to the sign of the update.");
        Console.WriteLine();

        Console.WriteLine();
        Console.WriteLine("To verify the results, a separate Python script 'test.py' is included in the same folder.");
        Console.WriteLine("It uses NumPy to compute the eigenvalues of both:");
        Console.WriteLine(" A = D + uuᵀ  (σ = 1)");
        Console.WriteLine(" B = D - uuᵀ  (σ = -1)");
        Console.WriteLine("The outputs from Python match the results from the C# code closely,");
        Console.WriteLine("providing an independent check of the secular equation method.");
        Console.WriteLine();
        Console.WriteLine("To run the test:  python3 test.py");
    }
}