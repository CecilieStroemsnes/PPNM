# === File Targets ===

EXE = main.exe
SRC = main.cs integrator.cs

OUT = out.txt
CALC_ERF = calc_erf.txt
TAB_ERF = tab_erf.txt
ACC_ERF = acc_erf.txt

PLOT_ERF = plot_erf.png
PLOT_ACC = plot_acc.png

# === Compilation ===

all: $(EXE)

$(EXE): $(SRC)
	mcs -out:$(EXE) $(SRC)

run: $(EXE)
	mono $(EXE)

cat:
	cat $(OUT)

# === Generate Plot Data ===

# You must update main.cs to output these files
$(CALC_ERF): run

$(ACC_ERF): run

# Tabulated values: manual or from source
$(TAB_ERF):
	echo "0.0 0.000000" > $(TAB_ERF)
	echo "0.5 0.5204998778" >> $(TAB_ERF)
	echo "1.0 0.8427007929" >> $(TAB_ERF)
	echo "1.5 0.9661051465" >> $(TAB_ERF)
	echo "2.0 0.9953222650" >> $(TAB_ERF)

# === Plots ===

$(PLOT_ERF): $(TAB_ERF) $(CALC_ERF)
	graph -T png -x 0 2 -y 0 1 \
		-X "x" -Y "erf(x)" -L "erf(x) comparison plot" --bitmap-size 800x600 \
		-S 4 -m 0 $(TAB_ERF) \
		-S 1 -m 1 $(CALC_ERF) \
	> $(PLOT_ERF)

$(PLOT_ACC): $(ACC_ERF)
	graph -T png -x -8 -1 -y -13 -3 \
		-X "Log(accuracy)" -Y "Log(error)" -L "Accuracy vs error in erf(1)" --bitmap-size 800x600 \
		-S 1 -m 1 $(ACC_ERF) \
	> $(PLOT_ACC)

plot: $(PLOT_ERF) $(PLOT_ACC)

# === Cleanup ===

clean:
	rm -f *.exe *.dll *.png $(CALC_ERF) $(TAB_ERF) $(ACC_ERF) $(OUT)
