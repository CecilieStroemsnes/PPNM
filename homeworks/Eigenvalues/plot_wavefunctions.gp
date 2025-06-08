set terminal svg size 600,400
set output 'wavefunctions.svg'
set xlabel 'r'
set ylabel 'Wavefunction f(r)'
set title 'Lowest eigenfunctions of hydrogen atom'
set grid
plot 'wavefunctions.txt' using 1:2 with lines title 'f₀(r)', \
     '' using 1:3 with lines title 'f₁(r)', \
     '' using 1:4 with lines title 'f₂(r)'

