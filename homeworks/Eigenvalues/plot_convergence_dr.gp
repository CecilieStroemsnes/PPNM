set terminal svg size 600,400
set output 'dr.svg'
set xlabel 'dr'
set ylabel 'ε₀'
set title 'Convergence test: ε₀ vs dr'
set grid
plot 'convergence_dr.txt' using 1:2 with linespoints title 'ε₀(dr)'

