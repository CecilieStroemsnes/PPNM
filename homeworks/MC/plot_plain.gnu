set terminal pngcairo size 800,600 font "Times,14"
set output 'plain_mc_plot.png'

set title "Plain Monte Carlo error and 1/√N scaling"
set xlabel "number of points N"
set ylabel "error"
set xrange [0:4000]
set yrange [0:*]
set key top right
set grid

plot \
    "unit_circle_estimated.txt" using 1:2 with lines lw 2 lc rgb "black" title "estimated error", \
    "unit_circle_actual.txt" using 1:2 with lines lw 2 lc rgb "gray" title "actual error", \
    1/sqrt(x) with lines dt 2 lw 1 lc rgb "black" title "1/√N"
