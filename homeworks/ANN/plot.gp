set terminal pngcairo size 1000,700 enhanced font 'Helvetica,14'
set output 'plot.png'

set title "Neural Network Fit of g(x) = cos(5x - 1) * exp(-x^2)"
set xlabel "x"
set ylabel "y"
set grid
set key right top
set yrange [-1.1:1.1]

plot \
    'true.txt' using 1:2 with lines lw 2 lc rgb "blue" title 'Target function', \
    'ann.txt'  using 1:2 with points pt 7 ps 1.2 lc rgb "red" title 'ANN approximation'
