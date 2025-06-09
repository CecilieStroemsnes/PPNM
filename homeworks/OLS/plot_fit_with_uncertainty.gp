# plot_fit_with_uncertainty.gp
# Plot best fit and fits shifted by ± coefficient uncertainties

set terminal pngcairo size 800,600 enhanced font 'Verdana,10'
set output 'fit_with_uncertainty.png'

set title 'Fit Sensitivity to Coefficient Uncertainties'
set xlabel 'Time (days)'
set ylabel 'Activity (relative units)'

# Linearized fit coefficients (from out.txt)
c0 = 4.95866       # ln(a)
c1 = -0.17062      # -λ

# Their uncertainties
dc0 = 0.03540
dc1 = 0.00716

# Convert back to activity scale
f(x)  = exp(c0 + c1 * x)
f1(x) = exp((c0+dc0) + (c1+dc1) * x)
f2(x) = exp((c0+dc0) + (c1-dc1) * x)
f3(x) = exp((c0-dc0) + (c1+dc1) * x)
f4(x) = exp((c0-dc0) + (c1-dc1) * x)

set key top right

plot 'data.txt' using 1:2:3 with yerrorbars title 'Data ± measurement error', \
     f(x)  with lines lw 3 linecolor rgb 'black' title 'Best fit', \
     f1(x) with lines lt 2 lw 1 title 'c0+dc0, c1+dc1', \
     f2(x) with lines lt 2 lw 1 title 'c0+dc0, c1-dc1', \
     f3(x) with lines lt 3 lw 1 title 'c0-dc0, c1+dc1', \
     f4(x) with lines lt 3 lw 1 title 'c0-dc0, c1-dc1'

