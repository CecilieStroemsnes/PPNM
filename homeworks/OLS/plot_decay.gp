# Set the terminal and output file.
set terminal pngcairo size 800,600 enhanced font 'Verdana,10'
set output 'fit_with_uncertainty.png'

# Set title and axes labels.
set title 'Fit with Uncertainties on Coefficients'
set xlabel 'Time (days)'
set ylabel 'Activity (relative units)'

# Best-fit parameters from your output.
c0 = 4.95866       # ln(a)
c1 = -0.17062      # -λ

# Uncertainties of the coefficients.
dc0 = 0.03540
dc1 = 0.00716

# Define the best-fit function (in the original activity scale).
f(x) = exp(c0 + c1*x)

# Define the four variations by adding/subtracting the uncertainties.
f1(x) = exp((c0+dc0) + (c1+dc1)*x)
f2(x) = exp((c0+dc0) + (c1-dc1)*x)
f3(x) = exp((c0-dc0) + (c1+dc1)*x)
f4(x) = exp((c0-dc0) + (c1-dc1)*x)

# Optionally, define a style for the uncertainty curves.
set style line 1 lt 2 lw 1 pt 7 linecolor rgb "blue"
set style line 2 lt 3 lw 1 pt 7 linecolor rgb "blue"

# Plot the experimental data with error bars, the best-fit curve, and the uncertainty variations.
plot 'data.txt' using 1:2:3 with yerrorbars title 'Experimental data', \
     f(x) with lines lw 3 linecolor rgb "red" title 'Best Fit', \
     f1(x) with lines ls 1 title 'Fit: c0+dc0, c1+dc1', \
     f2(x) with lines ls 1 title 'Fit: c0+dc0, c1-dc1', \
     f3(x) with lines ls 2 title 'Fit: c0-dc0, c1+dc1', \
     f4(x) with lines ls 2 title 'Fit: c0-dc0, c1-dc1'
