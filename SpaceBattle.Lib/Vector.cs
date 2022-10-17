namespace SpaceBattle.Lib;

public class Vector
{
    public int[] vector;

    public Vector(params int[] nums)
    {
        int size = nums.Length;
        if (size == 0) throw new Exception();
        vector = new int[size];
        for (int i = 0; i < size; i++)
        {
            vector[i] = nums[i];
        }
    }

    public int Size => vector.Length;

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
        if (v1.Size != v2.Size) throw new ArgumentException();
        else
        {
            int[] arr = new int[v1.Size];
            for (int i = 0; i < v1.Size; i++) arr[i] = v1[i] + v2[i];
            return new Vector(arr);
        }
    }

    public static Vector operator -(Vector v1, Vector v2)
    {
        if (v1.Size != v2.Size) throw new ArgumentException();
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

    public override bool Equals(object? obj) => obj is Vector v && vector.SequenceEqual(v.vector);

    public override int GetHashCode()
    {
        return String.Join("", vector.Select(x => x.ToString())).GetHashCode();
    }

}

