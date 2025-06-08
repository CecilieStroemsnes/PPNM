using System;
using static System.Math;

public static class root {
    public static vector newton(
        Func<vector,vector>f, /* the function to find the root of */
        vector start,         /* the start point */
        double acc = 1e-2,     /* accuracy goal: on exit ‖f(x)‖ should be <acc */
        vector dx = null)      /* optional δx-vector for calculation of jacobian */
    {
        vector x=start.copy();
        vector fx=f(x);
        vector z, fz;
        double λmin = 1e-6; 

        do{ /* Newton's iterations */
            if(fx.norm() < acc) break; /* job done */
            
            matrix J = jacobian(f,x,fx,dx);
            var QRJ = new QRdecomp(J);
            vector Dx = QRJ.solve(-fx); /* Newton's step */
            
            double λ=1;
            do{ /* linesearch */
                z = x+λ*Dx;
                fz = f(z);
                if( fz.norm() < (1-λ/2)*fx.norm() ) break;
                if( λ < λmin ) break;
                λ/=2;
                }while(true);
            
                x=z; 
                fx=fz;

                if (Dx.norm() < (dx != null ? dx.norm() : 1e-8)) break; /* job done */
        }while(true);
        
        return x;
    }


    public static matrix jacobian(
        Func<vector,vector> f,
        vector x,
        vector fx=null,
        vector dx=null
    ){
        int n = x.size;
        if(fx == null) fx = f(x);
        if(dx == null) {
            dx = new vector(n);
            for(int i=0;i < n;i++) 
                dx[i] = Math.Abs(x[i])*Pow(2,-26); 
        }

        matrix J = new matrix(x.size);
        for(int j=0; j < x.size; j++){
            x[j] += dx[j];
            vector dfx = f(x)-fx;
            
            for(int i=0; i < x.size; i++) {
                J[i,j]=dfx[i]/dx[j];
            }
            x[j] -= dx[j];
            }
        return J;
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
}