set terminal pngcairo size 800,600
set output 'convergence_rmax.png'
set title "Convergence with rmax"
set xlabel "rmax"
set ylabel "E₀"
set grid
set xrange [3.5:10.5]
set yrange [-0.502:-0.48]
set key top right
plot "rmax.txt" using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
     "" using 1:(-0.5) with lines lc rgb "gray" lw 2 title "Exact E₀ = -0.5"
