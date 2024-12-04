namespace advent_of_code_2024;

public class Day1(Day day)
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

        var leftList = new List<int>(rawLines.Length);
        var rightList = new List<int>(rawLines.Length);
    
        foreach (var line in rawLines)
        {
            var parts = line.Split(["   "], StringSplitOptions.None);
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[1]));
        }

        leftList.Sort();
        rightList.Sort();
        
        var total = 0;
        for (int i = 0; i < leftList.Count; i++)
        {
            total += Math.Abs(leftList[i] - rightList[i]);
        }

        Console.WriteLine(total);
    }
    
    private void Part2()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());

        var leftList = new List<int>(rawLines.Length);
        var rightList = new Dictionary<int, int>(rawLines.Length);
    
        foreach (var line in rawLines)
        {
            var parts = line.Split(["   "], StringSplitOptions.None);
            leftList.Add(int.Parse(parts[0]));
            if (rightList.ContainsKey(int.Parse(parts[1])))
            {
                rightList[int.Parse(parts[1])]++;
            }
            else
            {
                rightList[int.Parse(parts[1])] = 1;
            }
        }

        var total = 0;
        foreach (var val in leftList)
        {
            if (rightList.ContainsKey(val))
            {
                total += rightList[val] * val;
            }
        }
        
        Console.WriteLine(total);
    }
}