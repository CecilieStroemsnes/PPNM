CSC    = mcs
TARGET = main.exe
SRC    = main.cs

# Output files
OUTTXT     = Out.txt
LINEAR     = linear_interp.txt
QUAD       = quad_interp.txt
PLOTS      = linear_plot.svg quad_plot.svg integral_plot.svg compare_plot.svg

all: $(OUTTXT) $(PLOTS)

# Compile C# program
$(TARGET): $(SRC)
	$(CSC) -out:$(TARGET) $(SRC)

# Run program to generate data and Out.txt
$(OUTTXT) $(LINEAR) $(QUAD): $(TARGET)
	mono $(TARGET)

# Generate plots using gnuplot
linear_plot.svg: plot_linear.gnu $(LINEAR)
	gnuplot $<

quad_plot.svg: plot_quad.gnu $(QUAD)
	gnuplot $<

integral_plot.svg: plot_integral.gnu $(LINEAR) $(QUAD)
	gnuplot $<

compare_plot.svg: plot_compare.gnu $(LINEAR) $(QUAD)
	gnuplot $<

.PHONY: clean
clean:
	rm -f $(TARGET) $(OUTTXT) $(LINEAR) $(QUAD) $(PLOTS)