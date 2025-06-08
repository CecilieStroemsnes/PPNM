set terminal svg size 600,400
set output 'rmax.svg'
set xlabel 'rmax'
set ylabel 'ε₀'
set title 'Convergence test: ε₀ vs rmax'
set grid
plot 'convergence_rmax.txt' using 1:2 with linespoints title 'ε₀(rmax)'

