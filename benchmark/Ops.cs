using BenchmarkDotNet.Attributes;

namespace MoniChromaticTranscendencePathCalculator.Benchmark;

[MediumRunJob]
[MeanColumn]
[MemoryDiagnoser]
[MarkdownExporter]
[DisassemblyDiagnoser]
public class Benchmarks
{
    static readonly CustomOrderedStructComparer<Cost,
        Cost.ChromaticStabilizerUsedGetter, int,
        Cost.ChromaticCapacitorUsedGetter, int,
        Cost.DepthGetter, int> comparer = new();

    [Benchmark(Baseline = true)]
    public void Default()
    {
        Chroma chroma = Chroma.Red;
        for (PrismaticCore core = PrismaticCore.Inert; core != PrismaticCore.Supercritical; core++)
        {
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
                    }
                    else
                    {
                        path ??= path2;
                    }
                }
                if (path is null)
                {
                    return;
                }
            }
            chroma = core.NextChroma();
        }
    }
}