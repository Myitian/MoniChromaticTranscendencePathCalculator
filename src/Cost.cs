using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace MoniChromaticTranscendencePathCalculator;

public struct Cost(int chromaicStabilizerUsed, int chromaticCapacitorUsed) : IComparable<Cost>, IEquatable<Cost>,
    IComparisonOperators<Cost, Cost, bool>,
    IAdditionOperators<Cost, Cost, Cost>,
    ISubtractionOperators<Cost, Cost, Cost>,
    IMultiplyOperators<Cost, int, Cost>,
    IDivisionOperators<Cost, int, Cost>,
    IUnaryPlusOperators<Cost, Cost>,
    IUnaryNegationOperators<Cost, Cost>
{
    public int ChromaticStabilizerUsed = chromaicStabilizerUsed;
    public int Depth = chromaticCapacitorUsed;

    public static readonly Cost Zero = new(0, 0);
    public static readonly Cost MaxValue = new(int.MaxValue, int.MaxValue);

    public readonly int CompareTo(Cost other)
    {
        int result = ChromaticStabilizerUsed.CompareTo(other.ChromaticStabilizerUsed);
        if (result != 0) return result;
        return Depth.CompareTo(other.Depth);
    }
    public readonly bool Equals(Cost other)
        => ChromaticStabilizerUsed == other.ChromaticStabilizerUsed
        && Depth == other.Depth;
    public override readonly bool Equals([NotNullWhen(true)] object? obj)
        => obj is Cost other && Equals(other);
    public override readonly int GetHashCode()
        => (ChromaticStabilizerUsed << 16) | (Depth & 0xFFFF);
    public override readonly string ToString()
        => $"{ChromaticStabilizerUsed} (Depth {Depth})";
    public static bool operator ==(Cost left, Cost right)
        => left.Equals(right);
    public static bool operator !=(Cost left, Cost right)
        => !left.Equals(right);
    public static bool operator <(Cost left, Cost right)
        => left.ChromaticStabilizerUsed < right.ChromaticStabilizerUsed
        || (left.ChromaticStabilizerUsed == right.ChromaticStabilizerUsed && left.Depth < right.Depth);
    public static bool operator >(Cost left, Cost right)
        => left.ChromaticStabilizerUsed > right.ChromaticStabilizerUsed
        || (left.ChromaticStabilizerUsed == right.ChromaticStabilizerUsed && left.Depth > right.Depth);
    public static bool operator <=(Cost left, Cost right)
        => left.ChromaticStabilizerUsed < right.ChromaticStabilizerUsed
        || (left.ChromaticStabilizerUsed == right.ChromaticStabilizerUsed && left.Depth <= right.Depth);
    public static bool operator >=(Cost left, Cost right)
        => left.ChromaticStabilizerUsed > right.ChromaticStabilizerUsed
        || (left.ChromaticStabilizerUsed == right.ChromaticStabilizerUsed && left.Depth >= right.Depth);
    public static Cost operator +(Cost left, Cost right)
        => new(left.ChromaticStabilizerUsed + right.ChromaticStabilizerUsed, left.Depth + right.Depth);
    public static Cost operator -(Cost left, Cost right)
        => new(left.ChromaticStabilizerUsed - right.ChromaticStabilizerUsed, left.Depth - right.Depth);
    public static Cost operator *(Cost left, int right)
        => new(left.ChromaticStabilizerUsed * right, left.Depth * right);
    public static Cost operator *(int left, Cost right)
        => new(left * right.ChromaticStabilizerUsed, left * right.Depth);
    public static Cost operator /(Cost left, int right)
        => new(left.ChromaticStabilizerUsed / right, left.Depth / right);
    public static Cost operator +(Cost value)
        => value;
    public static Cost operator -(Cost value)
        => new(-value.ChromaticStabilizerUsed, -value.Depth);
    public void operator +=(Cost value)
    {
        ChromaticStabilizerUsed += value.ChromaticStabilizerUsed;
        Depth += value.Depth;
    }
    public void operator -=(Cost value)
    {
        ChromaticStabilizerUsed -= value.ChromaticStabilizerUsed;
        Depth -= value.Depth;
    }
    public void operator *=(int value)
    {
        ChromaticStabilizerUsed *= value;
        Depth *= value;
    }
    public void operator /=(int value)
    {
        ChromaticStabilizerUsed /= value;
        Depth /= value;
    }
}