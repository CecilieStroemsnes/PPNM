# plot_decay.gp
# Basic decay fit: experimental data with error bars + best-fit curve

set terminal pngcairo size 800,600 enhanced font 'Verdana,10'
set output 'decay_fit.png'

set title 'Radioactive Decay of ThX'
set xlabel 'Time (days)'
set ylabel 'Activity (relative units)'

# Best-fit parameters (from your output)
a = 142.40354
lambda = 0.17062

# Model function
f(x) = a * exp(-lambda * x)

set key top right

# Plot data (with y-error bars) and best-fit curve
plot 'data.txt' using 1:2:3 with yerrorbars title 'Experimental data', \
     f(x) with lines lw 2 title sprintf("Best fit: y = %.5f e^{-%.5f x}", a, lambda)
