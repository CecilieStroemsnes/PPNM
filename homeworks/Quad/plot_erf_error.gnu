set logscale xy
set xlabel "acc"
set ylabel "absolute error"
set grid
set title "Convergence of Adaptive Integration on erf(1)"
plot "-" using 1:2 with points pointtype 7 title "error vs acc"
1e-1 1.90e-4
1e-2 1.90e-4
1e-3 2.92e-5
1e-4 4.30e-7
1e-5 2.82e-8
1e-6 1.25e-9
1e-7 3.84e-11
1e-8 2.13e-12
e

