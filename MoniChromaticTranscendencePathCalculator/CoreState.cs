using System.Diagnostics.CodeAnalysis;

namespace MoniChromaticTranscendencePathCalculator;

struct CoreState(Chroma chroma, ChromaticCapacitor capacitors = ChromaticCapacitor.None)
{
    public Chroma Chroma = chroma;
    public int Depth;
    public int ChromaticStabilizerUsed;
    public int ChromaticCapacitorUsed;
    public ChromaticCapacitor Capacitors = capacitors;
}
struct Depth : IGetter<CoreState, int>
{
    public static int Get(CoreState obj)
    {
        return obj.Depth;
    }
}
struct ChromaticStabilizerUsed : IGetter<CoreState, int>
{
    public static int Get(CoreState obj)
    {
        return obj.ChromaticStabilizerUsed;
    }
}
struct ChromaticCapacitorUsed : IGetter<CoreState, int>
{
    public static int Get(CoreState obj)
    {
        return obj.ChromaticCapacitorUsed;
    }
}
sealed class CoreStateComparer<T1, T2, T3> : IComparer<CoreState>
    where T1 : IGetter<CoreState, int>
    where T2 : IGetter<CoreState, int>
    where T3 : IGetter<CoreState, int>
{
    public int Compare(CoreState x, CoreState y)
    {
        int c = T1.Get(x).CompareTo(T1.Get(y));
        if (c != 0) return c;
        c = T2.Get(x).CompareTo(T2.Get(y));
        if (c != 0) return c;
        return T3.Get(x).CompareTo(T3.Get(y));
    }
}