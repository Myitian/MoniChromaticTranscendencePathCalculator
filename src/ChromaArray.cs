using System.Runtime.CompilerServices;

namespace MoniChromaticTranscendencePathCalculator;

[InlineArray((int)Chroma.MaxValue)]
public struct ChromaArray<T>
{
    private T _;
}