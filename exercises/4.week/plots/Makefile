OUT = main.exe

all: run-plot

$(OUT): main.cs
	mcs -out:$(OUT) main.cs

run: $(OUT)
	mono $(OUT)

plot: erf_data.txt gamma_data.txt lngamma_data.txt erf_table_values gamma_table_values lngamma_table_values
	# Plot erf
	echo "set terminal pngcairo size 800,600; \
	      set output 'erf_plot.png'; \
	      set title 'Error Function (erf)'; \
	      set xlabel 'x'; set ylabel 'erf(x)'; set grid; \
	      plot 'erf_data.txt' using 1:2 with lines lw 2 lc rgb 'purple' title 'Computed Erf(x)', \
	           'erf_table_values' using 1:2 with points pt 7 ps 1.5 lc rgb 'red' title 'Tabulated Values'" \
	| gnuplot

	# Plot gamma
	echo "set terminal pngcairo size 800,600; \
	      set output 'gamma_plot.png'; \
	      set title 'Gamma Function (Γ)'; \
	      set xlabel 'x'; set ylabel 'Γ(x)'; set grid; \
	      plot 'gamma_data.txt' using 1:2 with lines lw 2 lc rgb 'purple' title 'Computed Γ(x)', \
	           'gamma_table_values' using 1:2 with points pt 7 ps 1.5 lc rgb 'red' title 'Tabulated Values'" \
	| gnuplot

	# Plot lngamma
	echo "set terminal pngcairo size 800,600; \
	      set output 'lngamma_plot.png'; \
	      set title 'Log-Gamma Function (lnΓ)'; \
	      set xlabel 'x'; set ylabel 'lnΓ(x)'; set grid; \
	      plot 'lngamma_data.txt' using 1:2 with lines lw 2 lc rgb 'purple' title 'Computed lnΓ(x)', \
	           'lngamma_table_values' using 1:2 with points pt 7 ps 1.5 lc rgb 'red' title 'Tabulated Values'" \
	| gnuplot

run-plot: run plot

clean:
	rm -f $(OUT) erf_data.txt gamma_data.txt lngamma_data.txt \
	      erf_plot.png gamma_plot.png lngamma_plot.png
