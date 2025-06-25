all: out.txt secular.png

out.txt: rank1update.exe
	mono rank1update.exe > out.txt

rank1update.exe: main.cs rank1update.cs vector.cs
	mcs -target:exe -out:rank1update.exe main.cs rank1update.cs vector.cs

secular.png: secular.txt secular.gnu
	gnuplot secular.gnu

.PHONY: clean
clean:
	rm -f *.exe out.txt secular.txt secular.png
