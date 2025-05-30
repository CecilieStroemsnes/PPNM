set terminal pngcairo size 800,600 font "Times,14"
set output 'quasi_mc_plot.png'

set title "Quasi-random Monte Carlo error and 1/√N scaling"
set xlabel "number of points N"
set ylabel "error"
set xrange [0:4000]
set yrange [0:*]
set key top right
set grid

plot \
    "unit_circle_qrand_estimated.txt" using 1:2 with lines lw 2 lc rgb "black" title "quasi-MC error", \
    1/sqrt(x) with lines dt 2 lw 1 lc rgb "black" title "1/√N"
