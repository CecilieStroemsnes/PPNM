set terminal svg size 600,400
set output 'scaling.svg'

set title "Jacobi Diagonalization Scaling"
set xlabel "Matrix size n"
set ylabel "Time [s]"
set key top left

f(n) = a*n**3
a = 1e-8  # initial guess

fit f(x) 'scaling_times.txt' using 1:2 via a

plot 'scaling_times.txt' using 1:2 with points pt 7 title 'Timing data', \
     f(x) with lines lw 2 title sprintf("fit: a·n³, a = %.2e", a)

