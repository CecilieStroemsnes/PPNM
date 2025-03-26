N = 100000000
TIME = /usr/bin/time -p -a -o $@

Out.txt : main.exe Makefile
	echo "Manual multithreading" > $@
	$(TIME) mono $< -terms:$(N) -threads:1 >> $@
	echo "" >> $@
	$(TIME) mono $< -terms:$(N) -threads:2 >> $@
	echo "" >> $@
	$(TIME) mono $< -terms:$(N) -threads:3 >> $@
	echo "" >> $@
	$(TIME) mono $< -terms:$(N) -threads:4 >> $@
	echo "" >> $@
	echo "Pitfalls in multiprocessing" >> $@
	$(TIME) mono $< -terms:$(N) -threads:1 -pitfalls:true >> $@

main.exe : main.cs
	mcs -out:$@ $<

.PHONY: clean
clean:
	rm -f main.exe Out.txt