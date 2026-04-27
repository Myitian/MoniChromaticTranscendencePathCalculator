namespace MoniChromaticTranscendencePathCalculator;

static class ShortestPathResolver
{
    public static ChromaDictionary<State> Resolve(
        IComparer<CoreState> comparer,
        Chroma initialChroma = Chroma.Red,
        ChromaticCapacitor initialCapacitors = ChromaticCapacitor.None)
    {
        ChromaDictionary<State> best = GenerateBestDictionary();
        ChromaDictionary<HashSet<ChromaticCapacitor>> visited = GenerateVisitedDictionary();

        Queue<State> queue = [];
        queue.Enqueue(new()
        {
            Core = new(initialChroma, initialCapacitors | ChromaticCapacitor.FromChroma(initialChroma)),
            Path = new(initialChroma)
        });
        while (queue.Count > 0)
        {
            State state = queue.Dequeue();

            HashSet<ChromaticCapacitor> variableState = visited[state.Core.Chroma];
            if (!variableState.Add(state.Core.Capacitors))
                continue;
            if (comparer.Compare(state.Core, best[state.Core.Chroma].Core) < 0)
                best[state.Core.Chroma] = state;

            UseChromaticStabilizer(state, out State result);
            queue.Enqueue(result);
            if (TryUseChromaticCapacitor(state, out result, Chroma.Red))
                queue.Enqueue(result);
            if (TryUseChromaticCapacitor(state, out result, Chroma.Yellow))
                queue.Enqueue(result);
            if (TryUseChromaticCapacitor(state, out result, Chroma.Green))
                queue.Enqueue(result);
            if (TryUseChromaticCapacitor(state, out result, Chroma.Cyan))
                queue.Enqueue(result);
            if (TryUseChromaticCapacitor(state, out result, Chroma.Blue))
                queue.Enqueue(result);
            if (TryUseChromaticCapacitor(state, out result, Chroma.Magenta))
                queue.Enqueue(result);
        }
        return best;

    }
    static ChromaDictionary<State> GenerateBestDictionary()
    {
        ChromaDictionary<State> dict = new();
        for (Chroma i = 0; i < Chroma.MaxValue; i++)
        {
            dict[i] = new()
            {
                Core = new(i, ChromaticCapacitor.FromChroma(i))
                {
                    ChromaticStabilizerUsed = int.MaxValue,
                    ChromaticCapacitorUsed = int.MaxValue,
                    Depth = int.MaxValue
                }
            };
        }
        return dict;
    }
    static ChromaDictionary<HashSet<ChromaticCapacitor>> GenerateVisitedDictionary()
    {
        ChromaDictionary<HashSet<ChromaticCapacitor>> dict = new();
        for (Chroma i = 0; i < Chroma.MaxValue; i++)
            dict[i] = [];
        return dict;
    }
    static State NextState(State state, Chroma chroma, ChromaticOperation operation)
    {
        State newState = new()
        {
            Core = state.Core with
            {
                Chroma = chroma,
                Depth = state.Core.Depth + 1,
                Capacitors = state.Core.Capacitors | ChromaticCapacitor.FromChroma(chroma)
            },
            Path = new(chroma, operation, state.Path)
        };
        switch (operation)
        {
            case ChromaticOperation.None:
                break;
            case ChromaticOperation.ChromaticStabilizer:
                newState.Core.ChromaticStabilizerUsed++;
                break;
            default:
                newState.Core.ChromaticCapacitorUsed++;
                break;
        }
        return newState;
    }
    static void UseChromaticStabilizer(State state, out State result)
    {
        Chroma newChroma = ((int)state.Core.Chroma % 4) switch
        {
            0 => state.Core.Chroma.Subtract(2),
            1 => state.Core.Chroma.Subtract(1),
            2 => state.Core.Chroma.Add(2),
            _ => state.Core.Chroma.Add(1)
        };
        result = NextState(state, newChroma, ChromaticOperation.ChromaticStabilizer);
    }
    static bool TryUseChromaticCapacitor(State state, out State result, Chroma capacitorChroma)
    {
        ChromaticCapacitor capacitor = ChromaticCapacitor.FromChroma(capacitorChroma);
        if (capacitor == ChromaticCapacitor.None || !state.Core.Capacitors.HasFlag(capacitor))
        {
            result = default;
            return false;
        }
        Chroma newChroma;
        if (state.Core.Chroma == capacitorChroma.Add(2))
            newChroma = capacitorChroma.Add(1);
        else if (state.Core.Chroma == capacitorChroma.Subtract(2))
            newChroma = capacitorChroma.Subtract(1);
        else
        {
            result = default;
            return false;
        }
        result = NextState(state, newChroma, ChromaticOperation.FromCapacitor(capacitor));
        return true;
    }
}