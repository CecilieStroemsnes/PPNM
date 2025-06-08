using static System.Console;

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

    // inverse of a matrix
   public static matrix inverse(matrix Q,matrix R){
        matrix inv = new matrix(Q.size1,Q.size2);
        for(int i=0; i<Q.size2; i++){
            vector b = new vector(Q.size1);
            b[i] = 1;                       // i-th unit vector
            vector x = solve(Q,R,b);        // Solve QRx=b
            for(int j=0; j<Q.size1; j++)    
                inv[j,i] = x[j];            // i-th column of the inverse
        }
        return inv;
   }
}


public class Program{
    public static void Main(string[] args){

        var rnd = new System.Random(1);

        WriteLine();
        WriteLine("=============================================================");
        WriteLine("Task A");
        WriteLine("=============================================================");
        WriteLine();
        
        // QR decomposition of a matrix test
        int n = 4;
        int m = 3;
        matrix A = new matrix(n,m);

        for(int i=0; i<n; i++)
            for(int j=0; j<m; j++)
                A[i,j] = rnd.NextDouble();

        WriteLine($"Generating a random {n}x{m} matrix A and checking its QR decomposition:");
        WriteLine("\nMatrix A:");
        A.print();

        var QR_res = QR.decomp(A);
        WriteLine("\nMatrix Q:");
        QR_res.Item1.print();
        WriteLine("\nMatrix R:");
        QR_res.Item2.print();

        // Check that R is upper triangular
        bool R_isUpper = true;
        double tol = 1e-10;
        for (int i = 0; i < QR_res.Item2.size1; i++) {
            for (int j = 0; j < i; j++) { // Only check entries below the diagonal
                if (System.Math.Abs(QR_res.Item2[i, j]) > tol) {
                    R_isUpper = false;
                    break;
                }
            }
            if (!R_isUpper)
                break;
        }
        WriteLine("\nR is upper triangular: " + R_isUpper);


        // check Q.T*Q = I
        matrix I = QR_res.Item1.T * QR_res.Item1;
        WriteLine("\nQᵀ*Q (should be identity):");
        I.print();

        // check Q*R = A
        matrix QR_product = QR_res.Item1 * QR_res.Item2;
        WriteLine("\nQ*R (should reconstruct A):");
        QR_product.print();

        // inverse test with a square matrix

        WriteLine();
        WriteLine("=============================================================");
        WriteLine("Task B");
        WriteLine("=============================================================");
        WriteLine();

        int size = 3;
        matrix B = new matrix(size,size);
        for(int i=0; i<size; i++)
            for(int j=0; j<size; j++)
                B[i,j] = rnd.NextDouble();

        WriteLine($"Generating a random {size}×{size} matrix A and vector b, solving A x = b, and checking inverse:");
        WriteLine("\nMatrix A:");
        B.print();

        // create random vector b
        vector b = new vector(size);
        for(int i=0; i<size; i++)
            b[i] = rnd.NextDouble();
        WriteLine("\nVector b:");
        b.print();

        var QR_res2 = QR.decomp(B);
        WriteLine("\nMatrix Q:");
        QR_res2.Item1.print();
        WriteLine("\nMatrix R:");
        QR_res2.Item2.print();

        // solve QRx=b
        vector x = QR.solve(QR_res2.Item1,QR_res2.Item2,b);
        WriteLine("\nSolution x:");
        x.print();

        // check B*x=b
        vector Bx = B*x;
        WriteLine("\nB*x (should equal b):");
        Bx.print();

        // compute inverse of B
        matrix B_inv = QR.inverse(QR_res2.Item1,QR_res2.Item2);
        WriteLine("\nInverse of B:");
        B_inv.print();

        // Check AB = I
        matrix prod = B * B_inv;
        WriteLine("\nB*B⁻¹ (should be identity):");
        prod.print();

    }
}