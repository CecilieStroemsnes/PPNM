using System;
using static System.Math;
using static System.Console;
using System.IO;

class Program{
	public static int Main(string[] args) {
	var list = new genlist<double[]>(); //This creates a new list of doubles using the genlist method of the list class from the list.dll file
	char[] delimiters = {' ','\t'}; //This sets the delimiters that the code recognises
	var options = StringSplitOptions.RemoveEmptyEntries; //This specifies how empty entries should be treated, in this case they are removed

	// command-line arguments
	string infile = null, outfile = null;
	foreach(var arg in args){
		var words=arg.Split(':');
		if(words[0]=="-input")infile=words[1];
		if(words[0]=="-output")outfile=words[1];
	}

	// Handle missing filenames
	if( infile==null || outfile==null) {
		Error.WriteLine("wrong filename argument");
		return 1;
	}


	// Actual reading/writing from/to the input/output file
	var instream = new System.IO.StreamReader(infile);
	var outstream = new System.IO.StreamWriter(outfile,append:false);

	// Loop over input file, taking each number in it and adds to generic list
	for(string line = instream.ReadLine();line!=null;line=instream.ReadLine()){

		var word = line.Split(delimiters,options);
		int n = word.Length;
		var numbers = new double[n];
		for(int i=0;i<n;i++) numbers[i] = double.Parse(word[i]);
		list.add(numbers);
       		}
	for(int i=0;i<list.size;i++){
		var numbers = list[i];
		foreach(var number in numbers)Write($"{number : 0.00e+00;-0.00e+00} ");
		WriteLine();
        	}

	return 0;
	}
}
