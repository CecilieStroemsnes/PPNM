using System;

class Program{
    static int Main(){

        // Max integer
        int i=1; while(i+1>i) {i++;}
        Console.WriteLine("max int = {0}\n", i);
        Console.WriteLine("Compared to max int {0}\n", int.MaxValue);

        // Min integer
        int j=1; while(j-1<j) {j--;}
        Console.WriteLine("min int = {0}\n", j);
        Console.WriteLine("Compared to the min int {0}\n", int.MinValue);

        // Machine Epsilon
        double x=1; while(1+x!=1){x/=2;} x*=2;
        float y=1F; while((float)(1F+y) != 1F){y/=2F;} y*=2F;

        Console.WriteLine("Machine epsilon for double {0} \n", x);
        Console.WriteLine("Expected to be {0} for double\n", Math.Pow(2,-52));
        Console.WriteLine("Machine epsilon for float {0}\n", y);
        Console.WriteLine("Expected to be {0} for single point float\n", Math.Pow(2,-23));

        // Tiny Epsilon
        double epsilon = Math.Pow(2,-52);
        double tiny = epsilon/2;
        double a = 1+tiny+tiny;
        double b = tiny+tiny+1;
        Console.WriteLine($"a==b ? {a==b}\n");
        Console.WriteLine($"a>1  ? {a>1}\n");
        Console.WriteLine($"b>1  ? {b>1}\n");
        Console.WriteLine("Explanation:");
        Console.WriteLine("When adding 'tiny' to 1, the result is rounded back to 1 due to floating-point precision.");
        Console.WriteLine("Since this happens twice, 'a' remains exactly 1, making 'a > 1' false.");
        Console.WriteLine("On the other hand, in 'b', we first add 'tiny' to itself, which forms an exact epsilon.");
        Console.WriteLine("When this epsilon is then added to 1, 'b' becomes slightly larger than 1.");
        Console.WriteLine("Therefore, 'a == b' evaluates to false because 'a' is 1, while 'b' is slightly greater than 1.\n");

        // Comparing Doubles
        double d1 = 0.1+0.1+0.1+0.1+0.1+0.1+0.1+0.1;
        double d2 = 8*0.1;

        Console.WriteLine($"d1={d1:e15}");
        Console.WriteLine($"d2={d2:e15}");
        Console.WriteLine($"d1==d2 ? => {d1==d2}");

        Console.WriteLine($"Testing the new approx comparison - d1==d2 ? => {funs.approx(d1,d2)}");

        return 0;
    }
}

