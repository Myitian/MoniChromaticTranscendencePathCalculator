using System.Diagnostics.CodeAnalysis;

namespace MoniChromaticTranscendencePathCalculator;

struct CoreState
{
    public int Depth;
    public int ChromaticStabilizerUsed;
    public int ChromaticCapacitorUsed;
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