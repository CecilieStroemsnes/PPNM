using System;
using System.IO;

class Program {
    static void Main() {
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.InvariantCulture;

        // Skriv error function data
        using (StreamWriter writer = new StreamWriter("erf_data.txt")) {
            for (double x = -3; x <= 3; x += 0.1) {
                writer.WriteLine($"{x} {erf(x)}");
            }
        }
        Console.WriteLine("Error function data written to erf_data.txt");

        // Skriv gamma function data
        using (StreamWriter writer = new StreamWriter("gamma_data.txt")) {
            for (double x = 0.5; x <= 5.5; x += 0.1) {
                writer.WriteLine($"{x} {sgamma(x)}");
            }
        }
        Console.WriteLine("Gamma function data written to gamma_data.txt");

        // Skriv log-gamma function data
        using (StreamWriter writer = new StreamWriter("lngamma_data.txt")) {
            for (double x = 0.5; x <= 5.5; x += 0.1) {
                writer.WriteLine($"{x} {lngamma(x)}");
            }
        }
        Console.WriteLine("Log-Gamma function data written to lngamma_data.txt");
    }

    static double erf(double x){
        // Single precision error function (Abramowitz and Stegun, from Wikipedia)
        if(x < 0) return -erf(-x);
        double[] a = {0.254829592, -0.284496736, 1.421413741, -1.453152027, 1.061405429};
        double t = 1.0 / (1.0 + 0.3275911 * x);
        double sum = t * (a[0] + t * (a[1] + t * (a[2] + t * (a[3] + t * a[4]))));
        return 1.0 - sum * Math.Exp(-x * x);
    }

    public static double sgamma(double x){
        if (x < 0) return Math.PI / (Math.Sin(Math.PI * x) * sgamma(1 - x));
        if (x < 9) return sgamma(x + 1) / x;
        double lngamma = Math.Log(2 * Math.PI) / 2 + (x - 0.5) * Math.Log(x) - x
                       + (1.0 / 12) / x - (1.0 / 360) / (x * x * x)
                       + (1.0 / 1260) / (x * x * x * x * x);
        return Math.Exp(lngamma);
    }

    static double lngamma(double x){
        if (x <= 0) throw new ArgumentException("lngamma: x<=0");
        if (x < 9) return lngamma(x + 1) - Math.Log(x);
        return x * Math.Log(x + 1 / (12 * x - 1 / (10 * x))) - x + Math.Log(2 * Math.PI / x) / 2;
    }
}
