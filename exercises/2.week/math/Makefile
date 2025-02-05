.PHONY : all clean

all: Out.txt

Out.txt : main.exe
	mono main.exe > Out.txt
	# mono main.exe > Out.txt

main.exe : main.cs sfuns.dll
	mcs -target:exe -r:sfuns.dll -out:main.exe main.cs

sfuns.dll : sfuns.cs
	mcs -target:library -out:sfuns.dll sfuns.cs

clean:
	$(RM) sfuns.dll main.exe Out.txt #remove all files made during project, only use when moving project
                                         # to architecture or something went wrong

test:
	echo $(RM) 
