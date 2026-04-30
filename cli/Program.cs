using MoniChromaticTranscendencePathCalculator;

Console.WriteLine($"{Chroma.Red} => {Chroma.Green}");
PlanSolver.GetPath(Chroma.Red, Chroma.Green)?.WriteFormatted(Console.Out);
Console.WriteLine($"{Chroma.Red} => {Chroma.Blue}");
PlanSolver.GetPath(Chroma.Red, Chroma.Blue)?.WriteFormatted(Console.Out);
Console.WriteLine($"{Chroma.Red} => {Chroma.Teal}");
PlanSolver.GetPath(Chroma.Red, Chroma.Teal)?.WriteFormatted(Console.Out);
Console.WriteLine($"{Chroma.Red} => {Chroma.Orange}");
PlanSolver.GetPath(Chroma.Red, Chroma.Orange)?.WriteFormatted(Console.Out);

Console.WriteLine();

PrismaticCore initialCore = PrismaticCore.Active;
Chroma[] initialChromas = new Chroma[5]; // machine count
int[] executionPlan = new int[6]; // step count
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
         * Cost: {c} / {c2}
        """);
    PlanSolver.GetPath(from, to)?.WriteFormatted(Console.Out);
}