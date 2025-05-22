set terminal pngcairo size 800,600
set xlabel "x"
set ylabel "y"

set output 'sho.png'
set title 'Simple Harmonic Oscillator'
plot 'sho.txt' using 1:2 with lines title 'y(x)'

set output 'damped.png'
set title 'Damped Harmonic Oscillator'
plot 'damped.txt' using 1:2 with lines title 'y(x)'

set output 'newton_orbit.png'
set size square
set title 'Newtonian Orbit'
set xlabel "x"
set ylabel "y"
plot 'newton_orbit.txt' using 1:2 with lines title 'orbit'

set output 'relativistic_orbit.png'
set size square
set title 'Relativistic Orbit with Precession'
set xlabel "x"
set ylabel "y"
plot 'relativistic_orbit.txt' using 1:2 with lines title 'orbit'
