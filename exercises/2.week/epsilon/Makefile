# C# compiler
CSC = mcs

# Output files
OUTFILE = Out.txt
MAIN_EXE = main.exe
LIBRARY = funs.dll

# Source files
MAIN_SRC = main.cs
FUNS_SRC = funs.cs

# Run the main program and save output in Out.txt
Out.txt: $(MAIN_EXE)
	mono $(MAIN_EXE) > $(OUTFILE)

# Compile main.exe
$(MAIN_EXE): $(MAIN_SRC) $(LIBRARY)
	$(CSC) -target:exe -out:$(MAIN_EXE) -reference:$(LIBRARY) $(MAIN_SRC)

# Compile the shared library funs.dll
$(LIBRARY): $(FUNS_SRC)
	$(CSC) -target:library -out:$(LIBRARY) $(FUNS_SRC)

# Phony target for cleaning up
.PHONY: clean
clean:
	rm -f $(MAIN_EXE) $(OUTFILE) $(LIBRARY)

