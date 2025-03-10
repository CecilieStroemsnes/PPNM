# Compiler
CC = mcs
SRC = main.cs
OUTPUT = main.exe

# Target to generate the output file
Out.txt: $(OUTPUT) input.txt
	mono $(OUTPUT) -input:input.txt -output:Out.txt

# Target to compile the program
$(OUTPUT): $(SRC)
	$(CC) $(SRC)

# Clean target
.PHONY: clean
clean:
	rm -f $(OUTPUT) Out.txt input.txt
