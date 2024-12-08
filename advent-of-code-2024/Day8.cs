namespace advent_of_code_2024;

public class Day8(Day day)
{
    public void FindSolution()
    {
        Console.WriteLine("Part 1:");
        Part1();
        // Console.WriteLine("Part 2:");
        // Part2();
    }

    private void Part1()
    {
        var map = CreateMap();
        var antennas = GetAntennas(map);
        foreach (var antenna in antennas)
        {
            var nodes = antenna.Value;
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    CreateAntiNodes(nodes[i], nodes[j], map);
                }
            }
        }

        var sum = map.SelectMany(x => x).Count(x => x.Value == "#");
        Console.WriteLine(sum);
    }

    private void CreateAntiNodes(Node node1, Node node2, Node[][] map)
    {
        double dx = node2.PosX - node1.PosX;
        double dy = node2.PosY - node1.PosY;
        
        double distance = Math.Sqrt(dx * dx + dy * dy);
        
        double unitX = dx / distance;
        double unitY = dy / distance;
        
        var pos1X = (int)Math.Round(node1.PosX - unitX * distance);
        var pos1Y = (int)Math.Round(node1.PosY - unitY * distance);
        var pos2X = (int)Math.Round(node2.PosX + unitX * distance);
        var pos2Y = (int)Math.Round(node2.PosY + unitY * distance);
        
        foreach (var node in map.SelectMany(x => x))
        {
            if ((node.PosX == pos1X && node.PosY == pos1Y) ||
                (node.PosX == pos2X && node.PosY == pos2Y))
            {
                node.Value = "#";
            }
        }
    }

    private Dictionary<string, List<Node>> GetAntennas(Node[][] map)
    {
        return map
            .SelectMany(x => x)
            .Where(node => node.Value != ".")
            .GroupBy(node => node.Value)
            .ToDictionary(group => group.Key, group => group.ToList());
    }

    private Node[][] CreateMap()
    {
        var rawLines = new ReadInput().ReadInputFile(day.GetInputFilePath());
        return rawLines.Select((line, indexY) => line.Select((x, indexX) => new Node
        {
            PosX = indexX,
            PosY = indexY,
            Value = x.ToString()
        }).ToArray()).ToArray();
    }

    private void PrintMap(Node[][] map)
    {
        Console.WriteLine(string.Join("\n", map.Select(line => string.Concat(line.Select(node => node.Value)))));
    }
    
    private record Node
    {
        public int PosX { get; set; }
        public int PosY { get; set; }
        public string Value { get; set; } = String.Empty;
    }
}