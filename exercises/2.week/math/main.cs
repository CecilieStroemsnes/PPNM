using System;
using static System.Math;

class main {
	static int Main(string[] args) {
		Console.WriteLine("hello");
		
		// math
		double sqrt2 = Sqrt(2.0);
		double power = Pow(21.0, 1.0 / 5.0);
		double e_pi = Pow(E, PI);
		double pi_e = Pow(PI, E);

		Console.WriteLine($"sqrt2^2 = {sqrt2 * sqrt2}" );
		Console.WriteLine($"21^(1/5) = {power}");
		Console.WriteLine($"e^pi = {e_pi}");
		Console.WriteLine($"pi^2 = {pi_e}");
	
		
		return 0;
	}
}



