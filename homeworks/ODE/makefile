all: out.txt sho.png damped.png newton_orbit.png relativistic_orbit.png

out.txt: main.exe
	mono main.exe > out.txt

main.exe: main.cs ode.cs vector.cs genlist.cs
	mcs -target:exe -out:main.exe main.cs ode.cs vector.cs genlist.cs

sho.png damped.png circular_orbit.png newton_orbit.png relativistic_orbit.png: \
	plots.gnuplot sho.txt damped.txt circular_orbit.txt newton_orbit.txt relativistic_orbit.txt
	gnuplot plots.gnuplot

.PHONY: clean
clean:
	rm -f main.exe out.txt *.txt *.png *.pdf
