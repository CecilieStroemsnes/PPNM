# Files
EXE = main.exe
SRC = main.cs integrator.cs
PLOT_ACC = plot_acc.png
PLOT_ERF = plot_erf.png

all: out.txt $(PLOT_ACC) $(PLOT_ERF)

out.txt: $(EXE)
	mono $(EXE) > out.txt

$(EXE): $(SRC)
	mcs -target:exe -out:$(EXE) $(SRC)

$(PLOT_ACC): plot_erf_error.gnu acc_erf.txt
	gnuplot plot_erf_error.gnu

$(PLOT_ERF): plot_erf.gnu calc_erf.txt tab_erf.txt
	gnuplot plot_erf.gnu

.PHONY: clean
clean:
	rm -f $(EXE) *.dll out.txt acc_erf.txt calc_erf.txt *.png *.pdf
