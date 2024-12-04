namespace advent_of_code_2024;

public class ReadInput
{
    public string[] ReadInputFile(string path)
    {
        return File.ReadAllLines(path);
    }
}