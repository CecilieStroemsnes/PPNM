# Compiler
CSC = mcs

# Executable name
OUT = main.exe

# Source files
SRC = main.cs ode.cs vector.cs genlist.cs

# Default target
all: $(OUT)

# Compile the project
$(OUT): $(SRC)
	$(CSC) -out:$(OUT) $(SRC)

# Run the program
run: $(OUT)
	mono $(OUT)

# Add gnuplot target
plot: 
	gnuplot plots.gnuplot

# Run everything in one step
allrun: all run plot


# Clean up generated files
clean:
	rm -f *.exe *.txt
