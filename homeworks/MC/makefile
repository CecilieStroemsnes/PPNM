all: out.txt plain_mc_plot.png quasi_mc_plot.png

main.exe: main.cs mc.cs vector.cs
	mcs -target:exe -out:$@ $^

out.txt: main.exe
	mono main.exe > out.txt

plain_mc_plot.png: unit_circle_estimated.txt unit_circle_actual.txt plot_plain.gnu
	gnuplot plot_plain.gnu

quasi_mc_plot.png: unit_circle_qrand_estimated.txt plot_quasi.gnu
	gnuplot plot_quasi.gnu

clean:
	rm -f *.exe *.dll *.txt *.png
