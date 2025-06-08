set terminal pngcairo size 800,600
set output 'convergence_eps.png'
set logscale x
set title "Convergence with eps"
set xlabel "eps (log scale)"
set ylabel "E₀"
set grid
set xrange [1e-6:1e-2]
set yrange [-0.50001:-0.49997]
set format x "10^{%T}"
set key top right
plot "eps.txt" using 1:2 with points pointtype 7 lc rgb "purple" title "Computed E₀", \
     "" using 1:(-0.5) with lines lc rgb "gray" lw 2 title "Exact E₀ = -0.5"
