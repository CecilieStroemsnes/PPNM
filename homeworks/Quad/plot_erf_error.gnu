set terminal pngcairo size 800,600 enhanced font 'Arial,10'
set output 'plot_acc.png'

set xlabel "log10(acc)"
set ylabel "log10(|erf(1) - exact|)"
set title "Error vs Accuracy for erf(1)"
set grid

plot "acc_erf.txt" using 1:2 with linespoints lt rgb "blue" pt 7 lw 2 title "Estimated error"
