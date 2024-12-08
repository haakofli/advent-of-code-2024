namespace advent_of_code_2024;

public record Day
{
    public required string FileName { get; init; }
    public required int DayNr { get; init; }
    // private const string RootPath = "../../../../advent-of-code-2024/Inputs";
    private const string RootPath = "Inputs";

    public static Day Day1 => new Day
    {
        DayNr = 1,
        FileName = "day1.txt",
    };

    public static Day Day2 => new Day
    {
        DayNr = 2,
        FileName = "day2.txt",
    };
    
    public static Day Day3 => new Day
    {
        DayNr = 3,
        FileName = "day3.txt",
    };

    public static Day Day4 => new Day
    {
        DayNr = 4,
        FileName = "day4.txt",
    };
    
    public static Day Day5 => new Day
    {
        DayNr = 5,
        FileName = "day5.txt",
    };
    
    public static Day Day6 => new Day
    {
        DayNr = 6,
        FileName = "day6.txt",
    };
    
    public static Day Day7 => new Day
    {
        DayNr = 7,
        FileName = "day7.txt",
    };
    
    public static Day Day8 => new Day
    {
        DayNr = 8,
        FileName = "day8.txt",
    };
    
    public string GetInputFilePath()
    {
        return $"{RootPath}/{FileName}";
    }
}

