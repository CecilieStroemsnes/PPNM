all: out.txt linear_plot.svg quad_plot.svg

out.txt: main.exe
	mono main.exe > out.txt

linear_plot.svg: linear_interp.txt plot_linear.gnu
	gnuplot plot_linear.gnu

quad_plot.svg: quad_interp.txt plot_quad.gnu
	gnuplot plot_quad.gnu

main.exe: main.cs
	mcs -target:exe -out:main.exe main.cs

.PHONY: clean
clean:
	rm -f main.exe out.txt linear_interp.txt quad_interp.txt *.svg *.png
