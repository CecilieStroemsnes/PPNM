set terminal pngcairo size 800,600
set output 'convergence_rmin.png'
set title "Convergence with rmin"
set xlabel "rmin"
set ylabel "E₀"
set grid
set xrange [0:0.25]
set yrange [-0.518:-0.495]
set key top right
plot "rmin.txt" using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
     "" using 1:(-0.5) with lines lc rgb "gray" lw 2 title "Exact E₀ = -0.5"
