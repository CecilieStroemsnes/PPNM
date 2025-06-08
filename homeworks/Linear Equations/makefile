all: out.txt

matrix.dll: matrix.cs vector.cs
	mcs -target:library -out:matrix.dll matrix.cs vector.cs

main.exe: main.cs matrix.dll
	mcs -target:exe -out:main.exe -reference:matrix.dll main.cs

out.txt: main.exe
	mono main.exe > out.txt

.PHONY: clean
clean:
	rm -f matrix.dll main.exe out.txt
