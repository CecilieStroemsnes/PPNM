all: out.txt decay_fit.png decay_fit_uncertainty.png fit_with_uncertainty.png

out.txt: main.exe
	mono main.exe > out.txt

decay_fit.png: data.txt plot_decay.gp
	gnuplot plot_decay.gp

decay_fit_uncertainty.png: data.txt plot_decay_uncertainty.gp
	gnuplot plot_decay_uncertainty.gp

fit_with_uncertainty.png: data.txt plot_fit_with_uncertainty.gp
	gnuplot plot_fit_with_uncertainty.gp

matrix.dll: matrix.cs vector.cs
	mcs -target:library -out:matrix.dll matrix.cs vector.cs

main.exe: main.cs matrix.dll
	mcs -target:exe -out:main.exe main.cs -r:matrix.dll

.PHONY: clean
clean:
	rm -f main.exe matrix.dll out.txt *.png
