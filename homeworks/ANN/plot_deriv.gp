set terminal pngcairo size 800,600 enhanced font "Arial,12"
set xlabel 'x'
set ylabel "f'(x)"
set grid
set key top left
set output 'plot_deriv.png'
set title 'Task B: First Derivative of ANN Approximation vs. Target'
plot \
    'true_deriv.txt' using 1:2 with lines lc rgb 'blue' lw 2 title 'Target Derivative', \
    'ann_deriv.txt' using 1:2 with points lc rgb 'red' pt 7 ps 1.2 title 'ANN Derivative'
