using MoniChromaticTranscendencePathCalculator;

CustomOrderedStructComparer<Cost,
    Cost.ChromaticStabilizerUsedGetter, int,
    Cost.ChromaticCapacitorUsedGetter, int,
    Cost.DepthGetter, int> comparer = new();

Chroma chroma = Chroma.Red;
for (PrismaticCore core = PrismaticCore.Inert; core != PrismaticCore.Supercritical; core++)
{
    Console.Out.WriteLine($"Current core: {core}");
    Console.Out.WriteLine($"Current chroma: {chroma}");
    Chroma required = core.RequiredChroma();
    if (chroma != required)
    {
        PathNode? path;
        ShortestPathResolver.Resolve(comparer, out ChromaDictionary<State> result, chroma, ChromaticCapacitor.Any);
        path = result[required].Path;
        if (chroma != Chroma.Red)
        {
            ShortestPathResolver.Resolve(comparer, out ChromaDictionary<State> result2, Chroma.Red, ChromaticCapacitor.Any);
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