namespace advent_of_code_2024;

public class Day6(Day day)
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
        var map = CreateMap();
        map = PopulateNeighbors(map);
        var startingNode = map.SelectMany(x => x).First(node => node.CurrentValue == "^");
        TraverseMap(startingNode);
        var sum = map.SelectMany(x => x).Count(x => x.CurrentValue == "X");
        Console.WriteLine(sum);
        PrintMap(map);
    }

    private void Part2()
    {
        
    }

    private void TraverseMap(Node startingNode)
    {
        var nextNode = GetNextNode(startingNode);

        while (nextNode != null)
        {
            nextNode = GetNextNode(nextNode);
        }
    }

    private Node? GetNextNode(Node currentNode)
    {
        var nextNode = currentNode.NextNode();
        if (nextNode == null)
        {
            currentNode.CurrentValue = "X";
            return nextNode;
        }
        if (nextNode.IsObstruction)
        {
            currentNode.Rotate90Degrees();
            return GetNextNode(currentNode);
        }
        
        nextNode.CurrentValue = currentNode.CurrentValue;
        currentNode.CurrentValue = "X";
        return nextNode;
    }

    private Node[][] CreateMap()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());
        return rawLines.Select(line => line.Select(x => new Node(x.ToString())).ToArray()).ToArray();
    }

    private Node[][] PopulateNeighbors(Node[][] map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map.First().Length; j++)
            {
                map[i][j].Left = j-1 >= 0 ? map[i][j-1] : null;
                map[i][j].Right = j+1 < map[i].Length ? map[i][j+1] : null;
                map[i][j].Top = i-1 >= 0 ? map[i-1][j] : null;
                map[i][j].Bottom = i+1 < map.Length ? map[i+1][j] : null;
            }
        }

        return map;
    }
    
    private void PrintMap(Node[][] map)
    {
        Console.WriteLine(string.Join("\n", map.Select(line => string.Concat(line.Select(node => node.CurrentValue)))));
    }

    private class Node(string InitialValue)
    {
        public string CurrentValue { get; set; } = InitialValue;
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node? Top { get; set; }
        public Node? Bottom { get; set; }
        public bool IsObstruction => CurrentValue == "#";

        public Node? NextNode()
        {
            if (CurrentValue == ">") return Right;
            if (CurrentValue == "v") return Bottom;
            if (CurrentValue == "<") return Left;
            if (CurrentValue == "^") return Top;
            return null;
        }

        public void Rotate90Degrees()
        {
            if (CurrentValue == ">") CurrentValue = "v";
            else if (CurrentValue == "v") CurrentValue = "<";
            else if (CurrentValue == "<") CurrentValue = "^";
            else if (CurrentValue == "^") CurrentValue = ">";
            else { Console.WriteLine($"Tries to rotate: {CurrentValue}"); }
        }
    }
}