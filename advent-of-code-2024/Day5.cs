namespace advent_of_code_2024;

public class Day5(Day day)
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
        var rules = rawLines.Where(x => x.Contains("|")).Select(x => x.Split("|").Select(int.Parse).ToArray()).ToArray();
        var pages = rawLines.Where(x => x.Contains(",")).Select(x => x.Split(",").Select(int.Parse).ToArray()).ToArray();
        var rulesDict = CreateRulesDictionary(rules);

        var sum = pages.Where(page => IsOrdered(rulesDict, page)).Sum(page => page[page.Length / 2]);
        
        Console.WriteLine(sum);
    }
    
    private void Part2()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());
        var rules = rawLines.Where(x => x.Contains("|")).Select(x => x.Split("|").Select(int.Parse).ToArray()).ToArray();
        var pages = rawLines.Where(x => x.Contains(",")).Select(x => x.Split(",").Select(int.Parse).ToArray()).ToArray();
        var rulesDict = CreateRulesDictionary(rules);

        var sum = pages.Where(page => !IsOrdered(rulesDict, page)).Select(page =>
        {
            var isFixed = false;
            var orderedPages = new List<int>();
            while (isFixed == false)
            {
                orderedPages = OrderPages(rulesDict, orderedPages.Count == 0 ? page : orderedPages.ToArray());
                isFixed = IsOrdered(rulesDict, orderedPages.ToArray());
            }

            return orderedPages;
        }).Sum(page => page[page.Count / 2]);
        
        
        Console.WriteLine(sum);
    }

    private List<int> OrderPages(Dictionary<int, int[]> rulesDict, int[] pages)
    {
        var ordered = new List<int>();
        foreach (var page in pages)
        {
            if (ordered.Count == 0) ordered.Add(page);
            else
            {
                foreach (var lookedAtPage in ordered.ToList())
                {
                    if (!rulesDict.ContainsKey(page))
                    {
                        if (!ordered.Contains(page)) ordered.Add(page);
                    }
                    else if (rulesDict[page].Contains(lookedAtPage))
                    {
                        if (ordered.Contains(page)) ordered.Remove(page);
                        ordered.Insert(ordered.IndexOf(lookedAtPage), page);
                    }
                    else
                    {
                        if (!ordered.Contains(page)) ordered.Add(page);
                    }
                }
            }
        }

        return ordered;
    }

    private bool IsOrdered(Dictionary<int, int[]> rulesDict, int[] pages)
    {
        var lookedAt = new List<int>();
        foreach (var page in pages)
        {
            if (lookedAt.Count == 0) lookedAt.Add(page);
            else
            {
                foreach (var lookedAtPage in lookedAt.ToList())
                {
                    if (!rulesDict.ContainsKey(page))
                    {
                        lookedAt.Add(page);
                        continue;
                    }
                    if (rulesDict[page].Contains(lookedAtPage))
                    {
                        return false;
                    }
                    lookedAt.Add(page);
                }
            }
        }

        return true;
    }

    private Dictionary<int, int[]> CreateRulesDictionary(int[][] rules)
    {
        return rules
            .GroupBy(pair => pair[0])
            .ToDictionary(
                group => group.Key,
                group => group.Select(pair => pair[1]).ToArray()
            );
    }
}