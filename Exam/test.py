import numpy as np

D = np.diag([1, 2, 3])
u = np.array([[1], [1], [1]])
A = D + u @ u.T
B = D - u @ u.T

evals = np.linalg.eigvalsh(A)
evals2 = np.linalg.eigvalsh(B)

print("Eigenvalues of A = D + uuᵀ (σ = 1):")
print(np.sort(evals))
print()

print("Eigenvalues of A = D - uuᵀ (σ = -1):")
print(np.sort(evals2))
