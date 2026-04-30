namespace MoniChromaticTranscendencePathCalculator;

public static class ChromaSolver
{
    public static void FindBestPath(
        out ChromaArray<State> result,
        Chroma initialChroma = Chroma.Red,
        ChromaticCapacitor initialCapacitors = ChromaticCapacitor.None)
    {
        result = new();
        ChromaArray<HashSet<ChromaticCapacitor>> visited = new();
        for (Chroma i = 0; i < Chroma.MaxValue; i++)
        {
            result[(int)i] = new()
            {
                Chroma = i,
                Capacitors = ChromaticCapacitor.FromChroma(i),
                Cost = Cost.MaxValue,
            };
            visited[(int)i] = [];
        }

        Queue<State> queue = [];
        queue.Enqueue(new()
        {
            Chroma = initialChroma,
            Capacitors = initialCapacitors | ChromaticCapacitor.FromChroma(initialChroma),
            Path = new(initialChroma)
        });
        while (queue.Count > 0)
        {
            State state = queue.Dequeue();

            HashSet<ChromaticCapacitor> variableState = visited[(int)state.Chroma];
            if (!variableState.Add(state.Capacitors))
                continue;
            if (state.Cost < result[(int)state.Chroma].Cost)
                result[(int)state.Chroma] = state;
            UseChromaticStabilizer(in state, out State next);
            queue.Enqueue(next);
            if (TryUseChromaticCapacitor(in state, out next, Chroma.Red))
                queue.Enqueue(next);
            if (TryUseChromaticCapacitor(in state, out next, Chroma.Yellow))
                queue.Enqueue(next);
            if (TryUseChromaticCapacitor(in state, out next, Chroma.Green))
                queue.Enqueue(next);
            if (TryUseChromaticCapacitor(in state, out next, Chroma.Cyan))
                queue.Enqueue(next);
            if (TryUseChromaticCapacitor(in state, out next, Chroma.Blue))
                queue.Enqueue(next);
            if (TryUseChromaticCapacitor(in state, out next, Chroma.Magenta))
                queue.Enqueue(next);
        }
    }
    static State NextState(State state, Chroma chroma, ChromaticOperation operation)
    {
        // structs are passed by value
        state.Path = new(chroma, operation, state.Path);
        state.Chroma = chroma;
        state.Capacitors |= ChromaticCapacitor.FromChroma(chroma);
        state.Cost.Depth++;
        if (operation is ChromaticOperation.ChromaticStabilizer)
            state.Cost.ChromaticStabilizerUsed++;
        return state;
    }
    static void UseChromaticStabilizer(ref readonly State state, out State result)
    {
        Chroma newChroma = ((int)state.Chroma % 4) switch
        {
            0 => state.Chroma.Subtract(2),
            1 => state.Chroma.Subtract(1),
            2 => state.Chroma.Add(2),
            _ => state.Chroma.Add(1)
        };
        result = NextState(state, newChroma, ChromaticOperation.ChromaticStabilizer);
    }
    static bool TryUseChromaticCapacitor(ref readonly State state, out State result, Chroma capacitorChroma)
    {
        ChromaticCapacitor capacitor = ChromaticCapacitor.FromChroma(capacitorChroma);
        if (capacitor == ChromaticCapacitor.None || !state.Capacitors.HasFlag(capacitor))
        {
            result = default;
            return false;
        }
        Chroma newChroma;
        if (state.Chroma == capacitorChroma.Add(2))
            newChroma = capacitorChroma.Add(1);
        else if (state.Chroma == capacitorChroma.Subtract(2))
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