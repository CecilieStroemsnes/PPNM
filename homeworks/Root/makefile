all: out.txt wavefunction.png convergence_acc.png convergence_eps.png convergence_rmin.png convergence_rmax.png

out.txt: root.exe
	mono root.exe > out.txt

root.exe: main.cs root.cs vector.cs matrix.cs ode.cs genlist.cs hydrogen.cs
	mcs -target:exe -out:root.exe main.cs root.cs vector.cs matrix.cs ode.cs genlist.cs hydrogen.cs

wavefunction.png convergence_acc.png convergence_eps.png convergence_rmin.png convergence_rmax.png: \
	plots.gnu acc.txt eps.txt rmin.txt rmax.txt out_hydrogen.txt
	gnuplot plots.gnu

.PHONY: clean
clean:
	rm -f root.exe out.txt *.txt *.png
