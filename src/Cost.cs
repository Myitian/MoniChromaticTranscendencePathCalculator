namespace MoniChromaticTranscendencePathCalculator;

public struct Cost
{
    public int Depth;
    public int ChromaticStabilizerUsed;
    public int ChromaticCapacitorUsed;

    public struct DepthGetter : IGetter<Cost, int>
    {
        public static int Get(Cost obj)
        {
            return obj.Depth;
        }
    }
    public struct ChromaticStabilizerUsedGetter : IGetter<Cost, int>
    {
        public static int Get(Cost obj)
        {
            return obj.ChromaticStabilizerUsed;
        }
    }
    public struct ChromaticCapacitorUsedGetter : IGetter<Cost, int>
    {
        public static int Get(Cost obj)
        {
            return obj.ChromaticCapacitorUsed;
        }
    }
}