out.txt: main.exe
	mono main.exe -input:input.txt -output:out.txt

.PHONY: clean
clean:
	rm -f main.exe GenList.dll out.txt

GenList.dll: GenList.cs
	mcs -target:library -out:GenList.dll GenList.cs

main.exe: main.cs GenList.dll
	mcs -target:exe -out:main.exe -reference:GenList.dll main.cs
