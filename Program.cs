
class Fraction
{
    public int numerator;
    public int denominator;

    public static Fraction operator +(Fraction left, Fraction right)
    {
        Fraction result = new Fraction();
        result.numerator = left.numerator * right.denominator + right.numerator * left.denominator;
        result.denominator = left.denominator * right.denominator;
        return result;
    }

    public static Fraction operator -(Fraction fraction)
    {
        Fraction result = new Fraction();
        result.numerator = -fraction.numerator;
        result.denominator = fraction.denominator;
        return result;
    }

    public static Fraction operator ++(Fraction origin)
    {
        Fraction copy = new Fraction();
        copy.numerator = origin.numerator + origin.denominator;
        copy.denominator = origin.denominator;
        return copy;
    }

    public static Fraction operator --(Fraction origin)
    {
        Fraction copy = new Fraction();
        copy.numerator = origin.numerator - origin.denominator;
        copy.denominator = origin.denominator;
        return copy;
    }

    public static bool operator ==(Fraction left, Fraction right)
    {
        double first = (double)left.numerator / left.denominator;
        double second = (double)right.numerator / right.denominator;
        if(first == second)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(Fraction left, Fraction right)
    {
        return !(left == right);
    }

    public static bool operator true(Fraction origin)
    {
        return origin.numerator != 0;
    }

    public static bool operator false(Fraction origin)
    {
        return origin.numerator == 0;
    }

    public override string ToString()
    {
        return this.numerator + "/" + this.denominator;
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Clear();
        Fraction left = new Fraction();
        Fraction right = new Fraction();
        left.numerator = 1;
        left.denominator = 2;
        right.numerator = 2;
        right.denominator = 4;

        if(left)
        {
            System.Console.WriteLine("true");
        }
    }
}