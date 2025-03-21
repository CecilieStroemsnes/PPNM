Out.txt : main.exe
	mono main.exe > Out.txt

cmath.dll : cmath.cs complex.cs
	mcs -target:library -out:./cmath.dll $^

main.exe : main.cs cmath.dll
	mcs -reference:cmath.dll -target:exe -out:main.exe main.cs

.PHONY: clean
clean:
	rm -f main.exe Out.txt
