set terminal pngcairo size 800,600
set output 'wavefunction.png'

set title "Hydrogen Atom: Numerical vs Exact Ground State Wavefunction"
set xlabel "r"
set ylabel "f(r)"
set grid
set key outside

plot "out_hydrogen.txt" using 1:2 with points pointtype 7 title "Numerical", \
     "out_hydrogen.txt" using 1:3 with lines linewidth 2 title "Exact: r*exp(-r)"
