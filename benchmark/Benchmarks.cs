using BenchmarkDotNet.Attributes;

namespace MoniChromaticTranscendencePathCalculator.Benchmark;

[MediumRunJob]
[MeanColumn]
[MemoryDiagnoser]
[MarkdownExporter]
[DisassemblyDiagnoser]
public class Benchmarks
{
    // tests ChromaSolver
    [Benchmark(Baseline = true)]
    public Cost Basic()
    {
        Cost cost = Cost.Zero;
        Chroma chroma = Chroma.Red;
        for (PrismaticCore core = PrismaticCore.Inert; core != PrismaticCore.Supercritical; core++)
        {
            Chroma required = core.RequiredChroma();
            if (chroma != required)
            {
                ChromaSolver.FindBestPath(out ChromaArray<State> result, chroma, ChromaticCapacitor.Any);
                PathNode? path = result[(int)required].Path;
                Cost c = result[(int)required].Cost;
                if (chroma != Chroma.Red)
                {
                    ChromaSolver.FindBestPath(out ChromaArray<State> result2, Chroma.Red, ChromaticCapacitor.Any);
                    PathNode? path2 = result2[(int)required].Path;
                    Cost c2 = result2[(int)required].Cost;
                    if (path is null || (path is not null && path2 is not null && c > c2))
                    {
                        path = path2;
                        c = c2;
                    }
                }
                if (path is null)
                    return Cost.MaxValue;
                cost += c;
            }
            chroma = core.NextChroma();
        }
        return cost;
    }
    // tests PlanSolver
    [Benchmark]
    public Cost OneMachine_FullExecutionPlan()
    {
        PrismaticCore initialCore = PrismaticCore.Inert;
        Span<Chroma> initialChromas = stackalloc Chroma[1];
        Span<int> executionPlan = stackalloc int[12];
        return PlanSolver.FindBestExecutionPlan(initialCore, initialChromas, executionPlan);
    }
    [Benchmark]
    public Cost TwoMachines_FullExecutionPlan()
    {
        PrismaticCore initialCore = PrismaticCore.Inert;
        Span<Chroma> initialChromas = stackalloc Chroma[2];
        Span<int> executionPlan = stackalloc int[12];
        return PlanSolver.FindBestExecutionPlan(initialCore, initialChromas, executionPlan);
    }
    [Benchmark]
    public Cost OneMachine_HalfExecutionPlan()
    {
        PrismaticCore initialCore = PrismaticCore.Inert;
        Span<Chroma> initialChromas = stackalloc Chroma[1];
        Span<int> executionPlan = stackalloc int[6];
        return PlanSolver.FindBestExecutionPlan(initialCore, initialChromas, executionPlan);
    }
    [Benchmark]
    public Cost TwoMachines_HalfExecutionPlan()
    {
        PrismaticCore initialCore = PrismaticCore.Inert;
        Span<Chroma> initialChromas = stackalloc Chroma[2];
        Span<int> executionPlan = stackalloc int[6];
        return PlanSolver.FindBestExecutionPlan(initialCore, initialChromas, executionPlan);
    }
    [Benchmark]
    public Cost ThreeMachines_HalfExecutionPlan()
    {
        PrismaticCore initialCore = PrismaticCore.Inert;
        Span<Chroma> initialChromas = stackalloc Chroma[3];
        Span<int> executionPlan = stackalloc int[6];
        return PlanSolver.FindBestExecutionPlan(initialCore, initialChromas, executionPlan);
    }
}