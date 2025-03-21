using System;
using static System.Math;
using static cmath;

class Program{
    public static int Main(){
        complex i = cmath.I;
        complex pi = Math.PI;

        // computation
        complex sqrt_neg1 = cmath.sqrt(new complex(-1,0));
        complex sqrt_i = cmath.sqrt(i);
        complex e_i = cmath.exp(i);
        complex e_i_pi = cmath.exp(i * pi);
        complex i_i = cmath.pow(i, i);
        complex ln_i = cmath.log(i);
        complex sin_i_pi = cmath.sin(i * pi);

        // printing
        Console.WriteLine("Comparing computed and expected values:");
        Console.WriteLine($"sqrt(-1) = {sqrt_neg1} (Expected: ±i) -> {sqrt_neg1.approx(i) || sqrt_neg1.approx(-i)}");
        Console.WriteLine($"sqrt(i) = {sqrt_i} (Expected: ±(1+i)/√2) -> {sqrt_i.approx((1+i)/Sqrt(2)) || sqrt_i.approx((-1-i)/Sqrt(2))}");
        Console.WriteLine($"exp(i) = {e_i} (Expected: cos(1)+i*sin(1)) -> {e_i.approx(cmath.cos(1)+i*cmath.sin(1))}");
        Console.WriteLine($"exp(i*π) = {e_i_pi} (Expected: -1) -> {e_i_pi.approx(-1)}");
        Console.WriteLine($"i^i = {i_i} (Expected: exp(-π/2)) -> {i_i.approx(cmath.exp(-PI/2))}");
        Console.WriteLine($"ln(i) = {ln_i} (Expected: iπ/2) -> {ln_i.approx(i*PI/2)}");
        Console.WriteLine($"sin(i*π) = {sin_i_pi} (Expected: i*sinh(π)) -> {sin_i_pi.approx(i*cmath.sinh(PI))}");

        // task 2
        complex sinh_i = sinh(i);
        complex cosh_i = cosh(i);

        Console.WriteLine("\nTesting hyperbolic functions:");
        Console.WriteLine($"sinh(i) = {sinh_i} (Expected: i sin(1)) -> {sinh_i.approx(i * sin(1))}");
        Console.WriteLine($"cosh(i) = {cosh_i} (Expected: cos(1)) -> {cosh_i.approx(cos(1))}");

        return 0; // Exit successfully

    }
}