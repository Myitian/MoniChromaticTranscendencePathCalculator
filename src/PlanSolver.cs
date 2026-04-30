using System.Numerics;
using System.Runtime.InteropServices;

namespace MoniChromaticTranscendencePathCalculator;

public static class PlanSolver
{
    private static ChromaArray<ChromaArray<State>> _cachedPath = new();
    static PlanSolver()
    {
        for (Chroma chroma = 0; chroma < Chroma.MaxValue; chroma++)
            ChromaSolver.FindBestPath(out _cachedPath[(int)chroma], chroma, ChromaticCapacitor.Any);
    }
    public static Cost GetCost(Chroma from, Chroma to)
    {
        return _cachedPath[(int)from][(int)to].Cost;
    }
    public static PathNode? GetPath(Chroma from, Chroma to)
    {
        return _cachedPath[(int)from][(int)to].Path?.DeepClone();
    }
    public static bool Increment<T>(Span<T> digits, T radix)
        where T : struct, IIncrementOperators<T>, IEquatable<T>
    {
        for (int i = 0; i < digits.Length; i++)
        {
            digits[i]++;
            if (radix.Equals(digits[i]))
                digits[i] = default;
            else
                return false;
        }
        return true;
    }
    public static Cost CalculateCost(
        PrismaticCore initialCore,
        scoped ReadOnlySpan<Chroma> initialChromas,
        scoped ReadOnlySpan<int> executionPlan)
    {
        Cost total = Cost.Zero;
        PrismaticCore currentCore = initialCore;
        Span<Chroma> currentChromas = stackalloc Chroma[initialChromas.Length];
        initialChromas.CopyTo(currentChromas);
        foreach (int id in executionPlan)
        {
            if (currentCore >= PrismaticCore.Supercritical)
                break;
            ref Chroma currentChroma = ref currentChromas[id];
            total += GetCost(currentChroma, currentCore.RequiredChroma());
            currentChroma = currentCore.NextChroma();
            currentCore++;
        }
        for (int i = 0; i < currentChromas.Length; i++)
            total += GetCost(currentChromas[i], initialChromas[i]);
        return total;
    }
    public static Cost FindBestInitialChromas(
        PrismaticCore initialCore,
        scoped Span<Chroma> initialChromas,
        scoped ReadOnlySpan<int> executionPlan)
    {
        Cost best = Cost.MaxValue;
        Span<Chroma> currentInitialChromas = stackalloc Chroma[initialChromas.Length];
        currentInitialChromas.Clear();
        do
        {
            Cost current = CalculateCost(initialCore, currentInitialChromas, executionPlan);
            if (current < best)
            {
                best = current;
                currentInitialChromas.CopyTo(initialChromas);
            }
        }
        while (!Increment(MemoryMarshal.Cast<Chroma, int>(currentInitialChromas), (int)Chroma.MaxValue));
        return best;
    }
    public static Cost FindBestExecutionPlan(
        PrismaticCore initialCore,
        scoped Span<Chroma> initialChromas,
        scoped Span<int> executionPlan)
    {
        // Currently using a brute-force method. No pruning methods are applied.
        // Time complexity is O(12^n * n^m), where n is the number of machines and m is the length of execution plan.
        Cost best = Cost.MaxValue;
        Span<Chroma> currentInitialChromas = stackalloc Chroma[initialChromas.Length];
        Span<int> currentExecutionPlan = stackalloc int[executionPlan.Length];
        executionPlan.Clear();
        do
        {
            Cost current = FindBestInitialChromas(initialCore, currentInitialChromas, currentExecutionPlan);
            if (current < best)
            {
                best = current;
                currentInitialChromas.CopyTo(initialChromas);
                currentExecutionPlan.CopyTo(executionPlan);
            }
        }
        while (!Increment(currentExecutionPlan, initialChromas.Length));
        return best;
    }
    public static IEnumerable<(int Id, Chroma From, Chroma To)> SimulateExecutionPlan(
        PrismaticCore initialCore,
        ReadOnlyMemory<Chroma> initialChromas,
        ReadOnlyMemory<int> executionPlan)
    {
        PrismaticCore currentCore = initialCore;
        Chroma[] currentChromas = new Chroma[initialChromas.Length];
        initialChromas.CopyTo(currentChromas);
        for (int i = 0; i < executionPlan.Length; i++)
        {
            int id = executionPlan.Span[i];
            if (currentCore >= PrismaticCore.Supercritical)
                break;
            Chroma from = currentChromas[id];
            Chroma to = currentCore.RequiredChroma();
            yield return (id, from, to);
            currentChromas[id] = currentCore.NextChroma();
            currentCore++;
        }
        for (int id = 0; id < currentChromas.Length; id++)
        {
            Chroma from = currentChromas[id];
            Chroma to = initialChromas.Span[id];
            yield return (id, from, to);
        }
    }
}