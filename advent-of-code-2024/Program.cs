using advent_of_code_2024;

var day = Day.Day5;

switch (day.DayNr)
{
    case 1:
        new Day1(day).FindSolution();
        break;
    case 2:
        new Day2(day).FindSolution();
        break;
    case 3:
        new Day3(day).FindSolution();
        break;
    case 4:
        new Day4(day).FindSolution();
        break;
    case 5:
        new Day5(day).FindSolution();
        break;
    default:
        Console.WriteLine("Not implemented yet");
        break;
};