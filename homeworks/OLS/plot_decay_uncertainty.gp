# plot_decay_uncertainty.gp
# Plot decay with uncertainty envelope

set terminal pngcairo size 800,600 enhanced font 'Verdana,10'
set output 'decay_fit_uncertainty.png'

set title 'Radioactive Decay of ThX with Uncertainty Envelope'
set xlabel 'Time (days)'
set ylabel 'Activity (relative units)'

# Best-fit parameters (from out.txt)
a = 142.40354
lambda = 0.17062

# Uncertainties (from out.txt)
sigma_ln_a = 0.03540
sigma_lambda = 0.00716

# Best-fit curve
f(x) = a * exp(-lambda * x)

# Propagated uncertainty in y:
sigma_y(x) = a * exp(-lambda * x) * sqrt( sigma_ln_a**2 + (x * sigma_lambda)**2 )

# Upper and lower bounds
upper(x) = f(x) + sigma_y(x)
lower(x) = f(x) - sigma_y(x)

set key top right

plot 'data.txt' using 1:2:3 with yerrorbars title 'Data ± measurement error', \
     f(x) with lines lw 2 linecolor rgb 'red' title 'Best fit', \
     upper(x) with lines lt 2 lw 1 title 'Upper envelope', \
     lower(x) with lines lt 2 lw 1 title 'Lower envelope'

