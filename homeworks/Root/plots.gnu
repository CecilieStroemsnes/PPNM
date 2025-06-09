# plots.gnu — all figures

### Wavefunction comparison
set terminal pngcairo size 800,600
set output 'wavefunction.png'
set title "Hydrogen Atom: Numerical vs Exact Ground State Wavefunction"
set xlabel "r"
set ylabel "f(r)"
set grid
plot \
    'out_hydrogen.txt' using 1:2 with points pointtype 7 lc rgb "purple" title "Numerical", \
    '' using 1:3 with lines lw 2 lc rgb "dark-green" title "Exact: r*exp(-r)"

### Convergence with acc
set output 'convergence_acc.png'
set logscale x
set title "Convergence with acc"
set xlabel "acc (log scale)"
set ylabel "E₀"
set xrange [1e-6:1e-2]
set yrange [-0.50001:-0.49997]
plot \
    'acc.txt' using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
    '' using 1:(-0.5) with lines lc rgb "gray" lw 2 title "Exact E₀ = -0.5"

### Convergence with eps
set output 'convergence_eps.png'
set logscale x
set title "Convergence with eps"
set xlabel "eps (log scale)"
plot \
    'eps.txt' using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
    '' using 1:(-0.5) with lines lc rgb "gray" lw 2 notitle

### Convergence with rmin
set output 'convergence_rmin.png'
unset logscale x
set title "Convergence with rmin"
set xlabel "rmin"
set xrange [0:0.25]
set yrange [-0.518:-0.495]
plot \
    'rmin.txt' using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
    '' using 1:(-0.5) with lines lc rgb "gray" lw 2 notitle

### Convergence with rmax
set output 'convergence_rmax.png'
set title "Convergence with rmax"
set xlabel "rmax"
set xrange [3.5:10.5]
set yrange [-0.502:-0.48]
plot \
    'rmax.txt' using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
    '' using 1:(-0.5) with lines lc rgb "gray" lw 2 notitle