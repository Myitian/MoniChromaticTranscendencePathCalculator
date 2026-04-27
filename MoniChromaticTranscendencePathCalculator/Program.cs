namespace MoniChromaticTranscendencePathCalculator;

static class Program
{
    static readonly CustomOrderedStructComparer<CoreState, ChromaticStabilizerUsed, int, ChromaticCapacitorUsed, int, Depth, int> comparer = new();
    static void Main()
    {
        Chroma chroma = Chroma.Red, required;
        for (PrismaticCore core = PrismaticCore.Inert; core != PrismaticCore.Supercritical; core++)
        {
            Console.Out.WriteLine($"Current core: {core}");
            Console.Out.WriteLine($"Current chroma: {chroma}");
            required = core.RequiredChroma();
            if (chroma != required)
            {
                PathNode? path;
                ChromaDictionary<State> result = ShortestPathResolver.Resolve(comparer, chroma, ChromaticCapacitor.Any);
                path = result[required].Path;
                if (chroma != Chroma.Red)
                {
                    ChromaDictionary<State> result2 = ShortestPathResolver.Resolve(comparer, Chroma.Red, ChromaticCapacitor.Any);
                    PathNode? path2 = result2[required].Path;
                    if (path is not null && path2 is not null && comparer.Compare(result[required].Core, result2[required].Core) > 0)
                    {
                        path = path2;
                        Console.Out.WriteLine($" **(Reset)-> Red");
                    }
                    else
                    {
                        path ??= path2;
                    }
                }
                if (path is null)
                {
                    Console.Out.WriteLine($" * Unreachable *");
                    return;
                }
                path.WriteFormatted(Console.Out);
            }
            Console.Out.WriteLine(" => Chromatic Transcendence");
            chroma = core.NextChroma();
        }
        Console.Out.WriteLine($"Current core: {PrismaticCore.Supercritical}");
        Console.Out.WriteLine($"Current chroma: {chroma}");
    }
}