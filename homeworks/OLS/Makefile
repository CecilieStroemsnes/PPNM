TIME = /usr/bin/time -p

Out.txt: main.exe
	mono main.exe > Out.txt

matrix.dll: matrix.cs vector.cs
	mcs -target:library -out:matrix.dll matrix.cs vector.cs

main.exe: main.cs matrix.dll
	mcs -target:exe -out:main.exe main.cs -r:matrix.dll

.PHONY: clean
clean:
	rm -f main.exe matrix.dll Out.txt
