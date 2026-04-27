namespace MoniChromaticTranscendencePathCalculator;

public interface IGetter<T, TValue>
{
    public static abstract TValue Get(T obj);
}