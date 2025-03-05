using System;
using static System.Console; // Allows using WriteLine without Console.

class Program
{
    static void Main()
    {
        vec v1 = new vec(1, 2, 3);
        vec v2 = new vec(3, 2, 1);

        WriteLine("Initialized two vectors:");
        v1.print();
        v2.print();

        WriteLine("Sum of vectors:");
        vec v3 = v1 + v2;
        v3.print();

        WriteLine("Difference of vectors:");
        vec v4 = v1 - v2;
        v4.print();

        WriteLine("Scaled vector:");
        vec v5 = v1 * 2;
        v5.print();

        WriteLine("Dot product:");
        var a = v1 % v2; // Using '%' since that is your dot product operator
        WriteLine(a);

        WriteLine("Cross product:");
        vec v6 = v1.cross(v2); // Missing cross product function in `vec`
        v6.print();

        WriteLine("Cross product dot product:");
        var b = v6 % v1; // Again, using '%' as defined
        WriteLine(b);
    }
}
