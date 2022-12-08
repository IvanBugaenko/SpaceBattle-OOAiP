namespace SpaceBattle.Lib;

public class MyAngle
{
    private int n, m;

    private static int gcd(int x, int y)
    {
        return Math.Abs(y) == 0 ? Math.Abs(x) : gcd(Math.Abs(y), Math.Abs(x) % Math.Abs(y));
    }

    public MyAngle(int n, int m)
    {
        if (m == 0) throw new ArgumentException();
        if (n >= 0 && m < 0 || n <= 0 && m < 0) 
        {
            m *= -1;
            n *= -1;
        }
        int d = gcd(n, m);
        this.n = n / d;
        this.m = m / d;
    }

    public static MyAngle operator + (MyAngle a, MyAngle b)
    {
        int p = a.n * b.m + b.n * a.m;
        int q = a.m * b.m;
        int d = gcd(p, q);
        return new MyAngle(p / d, q / d);
    } 

    public static bool operator ==(MyAngle a, MyAngle b) => a.n == b.n && a.m == b.m;

    public static bool operator !=(MyAngle a, MyAngle b) => !(a == b);

    public override bool Equals(object? obj) => obj is MyAngle a && n == a.n && m == a.m;

    public override int GetHashCode() => (this.n.ToString() + this.m.ToString()).GetHashCode();
}
