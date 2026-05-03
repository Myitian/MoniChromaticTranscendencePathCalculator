using MoniChromaticTranscendencePathCalculator;

while (true)
{
    Console.WriteLine("""
        Select an option:
        [0] exit
        [1] chroma transform path
        [2] execution plan
        """);
    try
    {
        switch (Console.ReadLine().AsSpan().Trim())
        {
            case "0":
                return;
            case "1":
                {
                    Chroma from = EnumInput<Chroma>("From chroma...", 0, Chroma.MaxValue - 1);
                    Chroma to = EnumInput<Chroma>("To chroma...", 0, Chroma.MaxValue - 1);
                    Console.WriteLine($"{from} => {to}");
                    PlanSolver.GetPath(from, to)?.WriteFormatted(Console.Out);
                    break;
                }
            case "2":
                PrismaticCore initialCore = EnumInput("Initial PrismaticCore:", PrismaticCore.Inert, PrismaticCore.Supercritical);
                int machineCount = Int32Input("Machine count:", 1, 16);
                int stepCount = Int32Input("Step count:", 1, 12);

                Chroma[] initialChromas = new Chroma[machineCount];
                int[] executionPlan = new int[stepCount];
                Cost cost = PlanSolver.FindBestExecutionPlan(initialCore, initialChromas, executionPlan);
                Console.WriteLine($"""
                Cost: {cost}
                Initial Chromas: {string.Join(", ", initialChromas)}
                Execution Plan:  {string.Join(", ", executionPlan)}

                """);
                Cost c2 = Cost.Zero;
                PrismaticCore core = initialCore;
                PrismaticCore final = initialCore + executionPlan.Length;
                foreach ((int id, Chroma from, Chroma to) in PlanSolver.SimulateExecutionPlan(initialCore, initialChromas, executionPlan))
                {
                    if (core < final)
                    {
                        Console.WriteLine($"Core: {core}");
                        core++;
                    }
                    else if (core == final)
                    {
                        Console.WriteLine($"Core: {core}");
                        Console.WriteLine("==================");
                        core++;
                    }
                    Cost c = PlanSolver.GetCost(from, to);
                    c2 += c;
                    Console.WriteLine($"""
                    Machine #{id} from {from} to {to}
                     * Cost: {c} / Total: {c2}
                    """);
                    PlanSolver.GetPath(from, to)?.WriteFormatted(Console.Out);
                }
                break;
            default:
                Console.WriteLine("Invalid input. Please enter a number from the menu.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex}");
    }
    Console.WriteLine();
}

static int Int32Input(string prompt, int minValue, int maxValue)
{
    while (true)
    {
        Console.WriteLine(prompt);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int result) && result >= minValue && result <= maxValue)
            return result;
        Console.WriteLine($"Invalid input. Please enter an integer between {minValue} and {maxValue}.");
    }
}
static TEnum EnumInput<TEnum>(string prompt, TEnum minValue, TEnum maxValue) where TEnum : struct, Enum
{
    while (true)
    {
        Console.WriteLine(prompt);
        foreach ((int i, TEnum value) in EnumInfo<TEnum>.Values.Index())
        {
            if (Comparer<TEnum>.Default.Compare(value, minValue) < 0
                || Comparer<TEnum>.Default.Compare(value, maxValue) > 0)
                continue;
            Console.WriteLine($"[{EnumInfo<TEnum>.RawValues.GetValue(i)}]\t{value}");
        }
        string? input = Console.ReadLine();
        if (Enum.TryParse<TEnum>(input, true, out TEnum result)
            && Comparer<TEnum>.Default.Compare(result, minValue) >= 0
            && Comparer<TEnum>.Default.Compare(result, maxValue) <= 0)
            return result;
        Console.WriteLine($"Invalid input. Please enter a valid {typeof(TEnum).Name} value.");
    }
}
static class EnumInfo<TEnum> where TEnum : struct, Enum
{
    public static readonly TEnum[] Values = Enum.GetValues<TEnum>();
    public static readonly Array RawValues = Enum.GetValuesAsUnderlyingType<TEnum>();
}