# Symmetric rank-1 update of a size-n symmetric eigenvalue problem

**Author:** Cecilie Strømsnes  
**Course:** Practical Programming and numerical methods (C#) 

## Problem Description
The matrix A to diagonalize is given in the form

A = D + σ * uuᵀ,

Where:
- `D` is a diagonal matrix
- `u` is a column-vector
- `σ` is a scalar (default = 1)

Given the diagonal elements of the matrix D and the elements of the vector u find the eigenvalues of the matrix A using only **O(n²)** operations via the **secular equation**:
f(λ) = 1 + Σᵢ (σ * uᵢ² / (dᵢ - λ)) = 0


## Contents

Files:
- `main.cs`: Runs all tests and prints output
- `rank1update.cs`: Implements the secular function and eigenvalue finder
- `vector.cs`: Vector math helper (provided)
- `Makefile`: Builds and runs the code + plot
- `secular.gnu`: Gnuplot script to visualize f(λ)
- `test.py`: NumPy-based validation script

Generated files:
- `out.txt`: All console output
- `secular.txt`: Data for plotting `f(λ)`
- `secular.png`: Final plot (used in report/exam)

## Test Cases

### 3×3 test
- `D = [1, 2, 3]`, `u = [1, 1, 1]`, `σ = 1`
- Eigenvalues found via secular equation
- Verified visually in `secular.png`

### Random 10×10 test
- Compares sum of eigenvalues to trace of `A`
- Confirms convergence and numerical accuracy

### Comparison with σ values
- Compares eigenvalues for `σ = 1` vs `σ = -1`
- Calculates norm of their difference

## Visualization: `secular.png`

The secular function is plotted using `gnuplot`, based on `secular.txt`.

Features:
- **Blue vertical lines**: poles at `dᵢ`
- **Red dashed lines**: roots (eigenvalues) from `f(λ) = 0`
- **Horizontal black line**: `f(λ) = 0`
- **Orange curve**: the function `f(λ)` itself

This confirms that:
- `f(λ)` diverges at `dᵢ` (poles)
- Crosses 0 between or beyond `dᵢ` → correct eigenvalues

## Build and Run Instructions

bash
make         # Builds and runs everything, generates out.txt + secular.png
make clean   # Removes compiled files and outputs