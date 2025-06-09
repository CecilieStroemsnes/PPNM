set terminal pngcairo size 800,600
set xlabel "x"
set ylabel "y"

set output 'sho.png'
set title 'Simple Harmonic Oscillator'
plot 'sho.txt' using 1:2 with lines title 'y(x)'

set output 'damped.png'
set title 'Damped Harmonic Oscillator'
plot 'damped.txt' using 1:2 with lines title 'y(x)'

set output 'circular_orbit.png'
set size square
set title 'Newtonian Circular Orbit'
set xlabel "x"
set ylabel "y"
set xrange [-1.2:1.2]
set yrange [-1.2:1.2]
plot 'circular_orbit.txt' using 1:2 with lines notitle

set output 'newton_orbit.png'
set size square
set title 'Newtonian Elliptical Orbit'
set xlabel "x"
set ylabel "y"
set xrange [-2:2]
set yrange [-2:2]
plot 'newton_orbit.txt' using 1:2 with lines notitle

set output 'relativistic_orbit.png'
set size square
set title 'Relativistic Orbit with Precession'
set xlabel "x"
set ylabel "y"
plot 'relativistic_orbit.txt' using 1:2 with lines title 'orbit'
