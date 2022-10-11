namespace SpaceBattle.Lib;

public class Vector
{
    public int[] vector = { 0 };
    public int Size = 0;

    public Vector(params int[] nums)
    {
        Size = nums.Length;
        vector = new int[Size];
        for (int i = 0; i < Size; i++)
        {
            vector[i] = nums[i];
        }
    }

    public override string ToString()
    {
        string s = "Vector(";
        for (int i = 0; i < Size - 1; i++) s += ($"{vector[i]}, ");
        s += ($"{vector[Size - 1]})");
        return s;
    }

    public int this[int index]
    {
        get
        {
            return vector[index];
        }

        set
        {
            vector[index] = value;
        }
    }

    public static Vector operator +(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size) throw new System.ArgumentException();
        else
        {
            int[] arr = new int[v1.Size];
            for (int i = 0; i < v1.Size; i++) arr[i] = v1[i] + v2[i];
            return new Vector(arr);
        }
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size) throw new System.ArgumentException();
        else
        {
            int[] arr = new int[v1.Size];
            for (int i = 0; i < v1.Size; i++) arr[i] = v1[i] - v2[i];
            return new Vector(arr);
        }
    }

    public static Vector operator *(int alfa, Vector v1)
    {
        int[] arr = new int[v1.Size];
        for (int i = 0; i < v1.Size; i++) arr[i] = alfa * v1[i];
        return new Vector(arr);
    }

    public static bool operator ==(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size) return false;
        for (int i = 0; i < v1.Size; i++)
        {
            if (v1[i] != v2[i]) return false;
        }
        return true;

    }

    public static bool operator !=(Vector v1, Vector v2) => !(v1 == v2);

    public override bool Equals(object? obj)
    {
        return obj is Vector v && vector.SequenceEqual(v.vector);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(vector);
    }

    public static bool operator <(Vector a, Vector b)
    {
        if (a == b) return false;
        for (int i = 0; i < Math.Min(a.Size, b.Size); i++)
            if (a[i] > b[i]) return false;
        if (a.Size > b.Size) return false;
        return true;
    }

    public static bool operator >(Vector v1, Vector v2)
    {
        return v2 < v1;
    }
}

