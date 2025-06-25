set terminal pngcairo size 1000,600 enhanced font 'Arial,14'
set output 'secular.png'

set title "Secular Function f(λ) and Eigenvalues from Rank-1 Update" font ",16"
set xlabel "λ (eigenvalue candidates)" offset 0,1
set ylabel "f(λ)" offset 1,0
set grid front lw 1 lc rgb "#dddddd"

set xrange [0.5:7.0]
set yrange [-100:100]
set key top right box

# Style for arrows and line widths
set style line 1 lc rgb "orange" lw 3     # f(λ)
set style line 2 lc rgb "black" dt 2 lw 2 # f(λ)=0 line
set style line 3 lc rgb "red" dt 2 lw 2   # roots
set style line 4 lc rgb "blue" lw 2 # poles

# Poles at d_i = 1, 2, 3
set arrow from 1.0, graph 0 to 1.0, graph 1 nohead ls 4
set arrow from 2.0, graph 0 to 2.0, graph 1 nohead ls 4
set arrow from 3.0, graph 0 to 3.0, graph 1 nohead ls 4

# Roots at known eigenvalues (from main.cs)
set arrow from 1.324869, graph 0 to 1.324869, graph 1 nohead ls 3
set arrow from 2.460811, graph 0 to 2.460811, graph 1 nohead ls 3
set arrow from 5.214319, graph 0 to 5.214319, graph 1 nohead ls 3

# Horizontal axis f(λ) = 0
set arrow from graph 0, first 0 to graph 1, first 0 nohead ls 2

plot \
 'secular.txt' using 1:2 with lines lc rgb "orange" lw 3 title "f(λ)", \
 1/0 with lines lc rgb "blue" lw 2 title "poles (dᵢ)", \
 1/0 with lines lc rgb "red" dt 2 lw 2 title "roots (λᵢ)", \
 1/0 with lines lc rgb "black" dt 2 lw 2 title "f(λ) = 0"