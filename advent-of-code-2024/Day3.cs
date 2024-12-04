using System.Text.RegularExpressions;

namespace advent_of_code_2024;

public class Day3(Day day)
{
    public void FindSolution()
    {
        Console.WriteLine("Part 1:");
        Part1();
        Console.WriteLine("Part 2:");
        Part2();
    }

    private void Part1()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());
        var multiplications = new List<string>();
        foreach (var line in rawLines)
        {
            multiplications.AddRange(FindOperationsPart1(line));
        }

        var sum = MultiplyMultiplications(multiplications.ToArray());
        Console.WriteLine(sum);
    }

    private void Part2()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());
        var multiplications = new List<string>();

        foreach (var line in rawLines)
        {
            multiplications.AddRange(FindOperationsPart2(line));

        }
        var sum = MultiplyMultiplicationsPart2(multiplications.ToArray());
        Console.WriteLine(sum);
    }

    private string[] FindOperationsPart2(string inputLine)
    {
        string pattern = @"mul\(\d+,\d+\)|do\(\)|don't\(\)";
        MatchCollection matches = Regex.Matches(inputLine, pattern);
        return matches.Select(x => x.Value).ToArray();
    }

    private string[] FindOperationsPart1(string inputLine)
    {
        string pattern = @"mul\(\d+,\d+\)";
        MatchCollection matches = Regex.Matches(inputLine, pattern);
        return matches.Select(x => x.Value).ToArray();
    }

    private int MultiplyMultiplicationsPart2(string[] multiplications)
    {
        var filteredMultiplication = new List<string>();
        bool disabled = false;
        foreach (var multiplication in multiplications)
        {
            if (multiplication == "do()") disabled = false;
            else if (multiplication == "don't()") disabled = true;
            else
            {
                if (!disabled) filteredMultiplication.Add(multiplication);
            }
        }

        return MultiplyMultiplications(filteredMultiplication.ToArray());
    }
    
    private int MultiplyMultiplications(string[] multiplications)
    {
        var sum = 0;
        foreach (var multiplication in multiplications)
        {
            var num1 = int.Parse(multiplication.Split("mul(")[1].Split(",")[0]);
            var num2 = int.Parse(multiplication.Split("mul(")[1].Split(",")[1].Split(")")[0]);
            sum += num1 * num2;
        }

        return sum;
    }
}