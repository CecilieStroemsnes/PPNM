all: main.exe

main.exe: main.cs vector.cs mc.cs
	mcs -target:exe -out:$@ $^

run: main.exe
	mono main.exe

cat: main.exe
	mono main.exe > out.txt
	cat out.txt

plot: main.exe
	mono main.exe > out.txt
	gnuplot plot_plain.gnu
	gnuplot plot_quasi.gnu
	open plain_mc_plot.png
	open quasi_mc_plot.png

clean:
	rm -f *.exe *.dll out.txt error_plot.png
