# Default target
all: higgs_fit.png

# Rule to run the program and generate fit.txt and out.txt
fit.txt: main.exe
	mono main.exe > out.txt 2> log

run: main.exe
	mono main.exe > plotdata.txt
	gnuplot plot.gnuplot < plotdata.txt

# Compile the program
main.exe: main.cs Newton.cs matrix.cs vector.cs
	mcs -target:exe -out:main.exe main.cs Newton.cs matrix.cs vector.cs

# Plot using gnuplot
higgs_fit.png: fit.txt data.txt plot.gnuplot
	gnuplot plot.gnuplot

# Clean generated files
clean:
	rm -f main.exe out.txt log fit.txt higgs_fit.png data.txt
