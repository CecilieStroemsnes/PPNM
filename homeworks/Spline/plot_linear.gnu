# plot_linear.gnu
reset
set terminal pngcairo size 800,600 enhanced font "Arial,12"
set output 'linear_plot.png'

set title 'Linear Spline Interpolation and Integral (cos(x))'
set xlabel 'x'
set ylabel 'Value'
set grid
set key top right

plot \
  'linear_interp.txt' using 1:2 with lines lw 2 lc rgb 'magenta' title 'Linear Interpolation', \
  '' using 1:3 with lines lw 2 lc rgb 'cyan' title 'Linear Integral', \
  cos(x) with lines lw 1 lc rgb 'gray' title 'cos(x)'