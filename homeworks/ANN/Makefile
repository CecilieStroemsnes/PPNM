EXE = ANN.exe
SRC = main.cs ANN.cs minimizer.cs vector.cs matrix.cs

.PHONY: all clean

all: plot.png plot_deriv.png plot_dderiv.png plot_antideriv.png

$(EXE): $(SRC)
	mcs -out:$(EXE) $(SRC)

out.txt: $(EXE)
	mono $(EXE) > out.txt

plot.png: out.txt plot.gp
	gnuplot plot.gp

plot_deriv.png: out.txt plot_deriv.gp
	gnuplot plot_deriv.gp

plot_dderiv.png: out.txt plot_dderiv.gp
	gnuplot plot_dderiv.gp

plot_antideriv.png: out.txt plot_antideriv.gp
	gnuplot plot_antideriv.gp

clean:
	rm -f *.exe *.dll out.txt plot*.png ann_*.txt true_*.txt antideriv.txt deriv.txt second_deriv.txt best_params.txt
