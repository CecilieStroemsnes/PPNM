OUT = Out.txt
EXE = main.exe
SRC = main.cs matrix.cs vector.cs jacobi.cs
PLOTS = dr.svg rmax.svg wavefunctions.svg

$(OUT): $(EXE)
	mono $(EXE) > $(OUT)
	gnuplot plot_convergence_dr.gp
	gnuplot plot_convergence_rmax.gp
	gnuplot plot_wavefunctions.gp
	gnuplot plot_scaling.gp

$(EXE): $(SRC)
	mcs -target:exe -out:$(EXE) $(SRC)

clean:
	rm -f *.exe *.dll *.svg *.txt *.log $(OUT)


