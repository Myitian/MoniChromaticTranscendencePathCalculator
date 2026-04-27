namespace MoniChromaticTranscendencePathCalculator;

[Flags]
public enum ChromaticCapacitor
{
    None,
    Red = 0b000001,
    Yellow = 0b000010,
    Green = 0b000100,
    Cyan = 0b001000,
    Blue = 0b010000,
    Magenta = 0b100000,
    Any = Red | Yellow | Green | Cyan | Blue | Magenta
}