using System;
using System.IO;

class Program {
    public static int Main(string[] args) {
        string infile = null, outfile = null;

        // Parse command-line arguments
        foreach (var arg in args) {
            var words = arg.Split(':');
            if (words[0] == "-input") infile = words[1];
            if (words[0] == "-output") outfile = words[1];
        }

        // Handle missing filenames
        if (infile == null || outfile == null) {
            Console.Error.WriteLine("Error: Missing input or output file.");
            return 1;
        }

        // Create a list to store arrays of doubles
        var list = new GenList<double[]>();
        char[] delimiters = { ' ', '\t' };

        // Read from input file
        using (var instream = new StreamReader(infile)) {
            string line;
            while ((line = instream.ReadLine()) != null) {
                var words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                double[] numbers = Array.ConvertAll<string, double>(words, w => double.TryParse(w, out double n) ? n : 0);
                list.Add(numbers);
            }
        }

        // Write to output file
        using (var outstream = new StreamWriter(outfile, false)) {
            for (int i = 0; i < list.Size; i++) {
                outstream.WriteLine(string.Join(" ", Array.ConvertAll(list[i], n => $"{n:0.00e+00}")));
            }
        }

        return 0;
    }
}
