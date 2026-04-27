namespace MoniChromaticTranscendencePathCalculator;

static class Extensions
{
    extension(Chroma chroma)
    {
        public Chroma Add(int offset)
        {
            offset %= (int)Chroma.MaxValue;
            return (Chroma)(((int)chroma + offset + (int)Chroma.MaxValue) % (int)Chroma.MaxValue);
        }
        public Chroma Subtract(int offset)
        {
            offset %= (int)Chroma.MaxValue;
            return (Chroma)(((int)chroma - offset + (int)Chroma.MaxValue) % (int)Chroma.MaxValue);
        }
    }
    extension(ChromaticOperation operation)
    {
        public static ChromaticOperation FromCapacitor(ChromaticCapacitor capacitor)
        {
            return capacitor switch
            {
                ChromaticCapacitor.Red => ChromaticOperation.RedChromaticCapacitor,
                ChromaticCapacitor.Yellow => ChromaticOperation.YellowChromaticCapacitor,
                ChromaticCapacitor.Green => ChromaticOperation.GreenChromaticCapacitor,
                ChromaticCapacitor.Cyan => ChromaticOperation.CyanChromaticCapacitor,
                ChromaticCapacitor.Blue => ChromaticOperation.BlueChromaticCapacitor,
                ChromaticCapacitor.Magenta => ChromaticOperation.MagentaChromaticCapacitor,
                _ => ChromaticOperation.None
            };
        }
        public string ToDisplayString()
        {
            return operation switch
            {
                ChromaticOperation.None => "None",
                ChromaticOperation.ChromaticStabilizer => "Chromatic Stabilizer",
                ChromaticOperation.RedChromaticCapacitor => "Chromatic Capacitor: Red",
                ChromaticOperation.YellowChromaticCapacitor => "Chromatic Capacitor: Yellow",
                ChromaticOperation.GreenChromaticCapacitor => "Chromatic Capacitor: Green",
                ChromaticOperation.CyanChromaticCapacitor => "Chromatic Capacitor: Cyan",
                ChromaticOperation.BlueChromaticCapacitor => "Chromatic Capacitor: Blue",
                ChromaticOperation.MagentaChromaticCapacitor => "Chromatic Capacitor: Magenta",
                _ => throw new InvalidOperationException("Invalid chromatic operation")
            };
        }
    }
    extension(ChromaticCapacitor)
    {
        public static ChromaticCapacitor FromChroma(Chroma chroma)
        {
            if ((int)chroma % 2 != 0)
                return ChromaticCapacitor.None;
            return (ChromaticCapacitor)(1 << ((int)chroma / 2));
        }
    }
    extension(PrismaticCore core)
    {
        public Chroma RequiredChroma()
        {
            return core switch
            {
                PrismaticCore.Inert => Chroma.Red,
                PrismaticCore.Red => Chroma.Yellow,
                PrismaticCore.Yellow => Chroma.Green,
                PrismaticCore.Green => Chroma.Cyan,
                PrismaticCore.Cyan => Chroma.Blue,
                PrismaticCore.Blue => Chroma.Magenta,
                PrismaticCore.Active => Chroma.Orange,
                PrismaticCore.Orange => Chroma.Lime,
                PrismaticCore.Lime => Chroma.Teal,
                PrismaticCore.Teal => Chroma.Azure,
                PrismaticCore.Azure => Chroma.Indigo,
                PrismaticCore.Indigo => Chroma.Pink,
                _ => Chroma.MaxValue
            };
        }
        public Chroma NextChroma()
        {
            return core switch
            {
                PrismaticCore.Inert => Chroma.Green,
                PrismaticCore.Red => Chroma.Red,
                PrismaticCore.Yellow => Chroma.Blue,
                PrismaticCore.Green => Chroma.Green,
                PrismaticCore.Cyan => Chroma.Red,
                PrismaticCore.Blue => Chroma.Blue,
                PrismaticCore.Active => Chroma.Green,
                PrismaticCore.Orange => Chroma.Indigo,
                PrismaticCore.Lime => Chroma.Blue,
                PrismaticCore.Teal => Chroma.Orange,
                PrismaticCore.Azure => Chroma.Red,
                PrismaticCore.Indigo => Chroma.Teal,
                _ => Chroma.MaxValue
            };
        }
    }
}