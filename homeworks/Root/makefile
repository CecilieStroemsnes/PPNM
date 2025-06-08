CSFILES = main.cs root.cs vector.cs matrix.cs ode.cs genlist.cs hydrogen.cs
TARGET = root.exe

all: $(TARGET)

$(TARGET): $(CSFILES)
	mcs -target:exe -out:$(TARGET) $(CSFILES)

cat: $(TARGET)
	mono $(TARGET) > out.txt
	cat out.txt
	gnuplot plot_hydrogen.gnu
	gnuplot plot_rmax.gnu
	gnuplot plot_rmin.gnu
	gnuplot plot_acc.gnu
	gnuplot plot_eps.gnu

clean:
	rm -f $(TARGET) out.txt out_hydrogen.txt *.txt *.png
