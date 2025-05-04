# plot_quad.gnu
reset
set terminal pngcairo size 800,600 enhanced font "Arial,12"
set output 'quad_plot.png'

set title 'Quadratic Spline Interpolation, Derivative, and Integral (sin(x))'
set xlabel 'x'
set ylabel 'Value'
set grid
set key top right

plot \
  'quad_interp.txt' using 1:2 with lines lw 2 lc rgb 'purple' title 'Quadratic Interpolation', \
  '' using 1:3 with lines lw 2 lc rgb 'green' title 'Quadratic Derivative', \
  '' using 1:4 with lines lw 2 lc rgb 'blue' title 'Quadratic Integral', \
  sin(x) with lines lw 1 lc rgb 'black' title 'sin(x)'