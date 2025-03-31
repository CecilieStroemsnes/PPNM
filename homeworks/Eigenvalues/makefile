all: Out.txt

main.exe: main.cs jacobi.cs matrix.cs vector.cs
	mcs -target:exe -out:main.exe main.cs jacobi.cs matrix.cs vector.cs

Out.txt: main.exe
	mono main.exe > Out.txt

clean:
	rm -f *.exe *.dll Out.txt
