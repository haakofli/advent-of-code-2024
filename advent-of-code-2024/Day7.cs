namespace advent_of_code_2024;

public class Day7(Day day)
{
    public void FindSolution()
    {
        Console.WriteLine("Part 1:");
        Part1();
    }

    private void Part1()
    {
        var equations = GetEquations();
        var sum = equations.Where(x => CanSolveEquation(x)).Sum(x => x.Result);
        Console.WriteLine(sum);
    }
    
    private bool CanSolveEquation(Equation equation)
    {
        var combinations = GetCombinations(equation.Numbers.Count - 1);

        foreach (var combination in combinations)
        {
            var result = equation.Numbers.First();

            if (equation.Numbers.Count() == 1)
            {
                if (equation.Numbers.First() == equation.Result) return true;
                else return false;
            }
            for (int i = 0; i < combination.Count; i++)
            {
                if (i + 1 < equation.Numbers.Count) 
                {
                    if (combination[i] == '+')
                        result += equation.Numbers[i + 1];
                    else if (combination[i] == '*')
                        result *= equation.Numbers[i + 1];
                }
            }

            if (result == equation.Result) return true;
        }

        return false;
    }
    
    private List<List<char>> GetCombinations(int count)
    {
        var operators = new[] { '+', '*' };

        if (count == 0) return [new()];

        var previousCombinations = GetCombinations(count - 1);
        var results = new List<List<char>>();

        foreach (var combination in previousCombinations)
        {
            foreach (var op in operators)
            {
                results.Add([..combination, op]);
            }
        }
        
        return results;
    }

    private List<Equation> GetEquations()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());
        var equations = new List<Equation>();
        foreach (var line in rawLines)
        {
            var result = long.Parse(line.Split(":")[0]);
            var numbers = line.Split(":")[1].Split(" ").Skip(1).Select(long.Parse).ToList();
            equations.Add(new Equation(result, numbers));
        }

        return equations;
    }

    private record Equation(long Result, List<long> Numbers)
    {
        public long Result { get; set; } = Result;
        public List<long> Numbers { get; set; } = Numbers;
    }
}