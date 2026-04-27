namespace MoniChromaticTranscendencePathCalculator;

interface IGetter<T, TValue>
{
    public static abstract TValue Get(T obj);
}