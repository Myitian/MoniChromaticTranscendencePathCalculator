namespace MoniChromaticTranscendencePathCalculator;

sealed class PathNode(Chroma chroma, ChromaticOperation operation = ChromaticOperation.None, PathNode? previous = null)
{
    public Chroma Chroma { get; } = chroma;
    public ChromaticOperation Operation { get; } = operation;
    public PathNode? Previous { get; } = previous;

    public void WriteFormatted(TextWriter writer)
    {
        Previous?.WriteFormatted(writer);
        if (Operation != ChromaticOperation.None)
        {
            if (Previous != null)
                writer.Write($" --({Operation.ToDisplayString()})-> ");
            writer.WriteLine(Chroma.ToString());
        }
    }
}