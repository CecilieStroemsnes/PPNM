# plot_integral.gnu
reset
set terminal pngcairo size 800,600 enhanced font "Arial,12"
set output 'integral_plot.png'

set title 'Spline Integrals Comparison'
set xlabel 'x'
set ylabel 'Integral Value'
set grid
set key top left

plot \
  'linear_interp.txt' using 1:3 with lines lw 2 lc rgb 'cyan' title 'Linear Integral (cos)', \
  'quad_interp.txt'   using 1:4 with lines lw 2 lc rgb 'blue' title 'Quadratic Integral (sin)', \
  sin(x) with lines lw 1 lc rgb 'gray' title 'True sin(x)', \
  (cos(x) == cos(x) ? 0 : 1) notitle
