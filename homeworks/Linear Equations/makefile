TIME = /usr/bin/time -p

# Generate Out.txt by running main.exe with Mono.
Out.txt: main.exe
	mono main.exe > Out.txt

# Build matrix.dll from matrix.cs and vector.cs.
matrix.dll: matrix.cs vector.cs
	mcs -target:library -out:matrix.dll matrix.cs vector.cs

# Build main.exe, referencing the library.
main.exe: main.cs matrix.dll
	mcs -target:exe -out:main.exe -reference:matrix.dll main.cs

.PHONY: clean
clean:
	rm -f main.exe Out.txt matrix.dll
