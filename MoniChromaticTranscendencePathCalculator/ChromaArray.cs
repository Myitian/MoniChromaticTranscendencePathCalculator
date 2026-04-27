using System.Runtime.CompilerServices;

namespace MoniChromaticTranscendencePathCalculator;

[InlineArray((int)Chroma.MaxValue)]
struct ChromaArray<T>
{
    private T _;
}