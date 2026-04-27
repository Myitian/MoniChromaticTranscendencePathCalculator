namespace MoniChromaticTranscendencePathCalculator;

public sealed class CustomOrderedStructComparer<T, P1, V1, P2, V2, P3, V3> : IComparer<T>
    where T : struct
    where P1 : IGetter<T, V1>
    where V1 : IComparable<V1>
    where P2 : IGetter<T, V2>
    where V2 : IComparable<V2>
    where P3 : IGetter<T, V3>
    where V3 : IComparable<V3>
{
    public int Compare(T x, T y)
    {
        int c = P1.Get(x).CompareTo(P1.Get(y));
        if (c != 0) return c;
        c = P2.Get(x).CompareTo(P2.Get(y));
        if (c != 0) return c;
        return P3.Get(x).CompareTo(P3.Get(y));
    }
}