# Compiler
CSC = mcs

# Target files
TARGET_LIB = vec.dll
TARGET_EXE = main.exe
OUTPUT_FILE = Out.txt

# Source files
SRC_LIB = vec.cs
SRC_EXE = main.cs

# Default target
all: $(TARGET_EXE)

# Generate Out.txt by running main.exe
$(OUTPUT_FILE): $(TARGET_EXE)
	mono $(TARGET_EXE) > $(OUTPUT_FILE)

# Compile vec.dll (library)
$(TARGET_LIB): $(SRC_LIB)
	$(CSC) -target:library -out:$(TARGET_LIB) $(SRC_LIB)

# Compile main.exe (depends on vec.dll)
$(TARGET_EXE): $(SRC_EXE) $(TARGET_LIB)
	$(CSC) -target:exe -out:$(TARGET_EXE) -reference:$(TARGET_LIB) $(SRC_EXE)

# Run the program
run: $(TARGET_EXE)
	mono $(TARGET_EXE)

# Clean build files
clean:
	rm -f $(TARGET_LIB) $(TARGET_EXE) $(OUTPUT_FILE)
