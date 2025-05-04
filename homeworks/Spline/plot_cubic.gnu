# plot_cubic.gnu
reset
set terminal pngcairo size 800,600 enhanced font "Arial,12"
set output 'cubic_plot.png'

set title 'Cubic Spline vs. Built-in cspline (cos(x))'
set xlabel 'x'
set ylabel 'y'
set grid
set key top right

plot \
  'cubic_interp.txt' using 1:2 with lines lw 2 lc rgb 'magenta' title 'Implemented cubic splines', \
  'cubic_interp.txt' using 1:2 smooth csplines with lines lw 2 lc rgb 'skyblue' title 'Built-in cubic splines', \
  'cubic_interp.txt' using 1:2 with points pt 2 ps 1 lc rgb 'green' title 'Given data'

