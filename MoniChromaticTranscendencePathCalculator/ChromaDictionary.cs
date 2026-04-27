using System.Collections;

namespace MoniChromaticTranscendencePathCalculator;

struct ChromaDictionary<T>
{
    private ChromaArray<T> values;
    public readonly ChromaArray<T> Values => values;
    public T this[Chroma key]
    {
        readonly get => values[(int)key];
        set => values[(int)key] = value;
    }
}