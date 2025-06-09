from scipy.integrate import quad
import numpy as np

def f1(x): return 1 / np.sqrt(x)
def f2(x): return np.log(x) / np.sqrt(x)
def f3(x): return np.exp(-x**2)

print("Integral of 1/sqrt(x) from 0 to 1:")
res1, err1 = quad(f1, 0, 1)
print("  Result:", res1, "Estimated error:", err1)

print("Integral of ln(x)/sqrt(x) from 0 to 1:")
res2, err2 = quad(f2, 0, 1)
print("  Result:", res2, "Estimated error:", err2)

print("Integral of exp(-x^2) from 0 to ∞:")
res3, err3 = quad(f3, 0, np.inf)
print("  Result:", res3, "Estimated error:", err3)

