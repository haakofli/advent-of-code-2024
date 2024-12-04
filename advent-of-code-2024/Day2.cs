namespace advent_of_code_2024;

public class Day2(Day day)
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
        var sum = 0;
        foreach (var line in rawLines)
        {
            var levels = line.Split(' ').Select(int.Parse).ToArray();
            if (AllDecrease(levels) || AllIncrease(levels))
            {
                sum++;
            }
        }
        
        Console.WriteLine(sum);
    }

    private void Part2()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());

        var sum = 0;
        foreach (var line in rawLines)
        {
            var levels = line.Split(' ').Select(int.Parse).ToArray();
            if (AllDecrease(levels) || AllIncrease(levels))
            {
                sum++;
            }
            else
            {
                for (int i = 0; i < levels.Length; i++)
                {
                    var subLevel = levels.ToList();
                    subLevel.RemoveAt(i);
                    if (AllDecrease(subLevel.ToArray()) || AllIncrease(subLevel.ToArray()))
                    {
                        sum++;
                        break;
                    }
                }
            }
        }
        Console.WriteLine(sum);
    }
    
    private bool AllIncrease(int[] level)
    {
        var prev = level.First() - 1;
        foreach (var value in level)
        {
            if (value - prev > 3 || value - prev <= 0) return false;
            prev = value;
        }

        return true;
    }

    private bool AllDecrease(int[] level)
    {
        var prev = level.First() + 1;
        foreach (var value in level)
        {
            if (prev - value > 3 || prev - value <= 0) return false;
            prev = value;
        }

        return true;
    }
}