set terminal pngcairo size 800,600 enhanced font 'Arial,10'
set output 'plot_erf.png'

set xlabel "x"
set ylabel "erf(x)"
set title "Comparison of Computed vs Tabulated erf(x)"
set grid

plot "tab_erf.txt" using 1:2 with linespoints lt rgb "red" pt 7 lw 2 title "Tabulated", \
     "calc_erf.txt" using 1:2 with lines lt rgb "blue" lw 2 title "Computed"

