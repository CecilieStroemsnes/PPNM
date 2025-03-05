using System;

public class vec
{
    public double x, y, z;

    // Constructors
    public vec() { x = 0; y = 0; z = 0; }

    public vec(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    // Operators
    public static vec operator*(vec v, double c){ return new vec(c*v.x, c*v.y, c*v.z); } // Multiply vector by scalar
    public static vec operator*(double c, vec v){ return v * c; } // Commutative multiplication
    public static vec operator+(vec u, vec v){ return new vec(u.x + v.x, u.y + v.y, u.z + v.z); } // Add two vectors
    public static vec operator-(vec u){ return new vec(-u.x, -u.y, -u.z); } // Negate vector
    public static vec operator-(vec u, vec v){ return u + (-v); } // Subtract two vectors
    public static double operator%(vec u, vec v){ return u.x * v.x + u.y * v.y + u.z * v.z; } // Dot product

    // Cross Product
    public vec cross(vec other)
    {
        return new vec(
            this.y * other.z - this.z * other.y,
            this.z * other.x - this.x * other.z,
            this.x * other.y - this.y * other.x
        );
    }

    // Override ToString for printing
    public override string ToString() { return $"{x} {y} {z}"; } 

    // Implement print() method
    public void print()
    {
        Console.WriteLine(this);
    }

    // Comparison methods
    public static bool approx(double a, double b, double acc = 1e-6, double eps = 1e-6)
    {
        if (Math.Abs(a - b) < acc) return true;
        if (Math.Abs(a - b) < (Math.Abs(a) + Math.Abs(b)) * eps) return true;
        return false;
    }

    public bool approx(vec other)
    {
        if (!approx(this.x, other.x)) return false;
        if (!approx(this.y, other.y)) return false;
        if (!approx(this.z, other.z)) return false;
        return true;
    }

    public static bool approx(vec u, vec v) => u.approx(v);
}
