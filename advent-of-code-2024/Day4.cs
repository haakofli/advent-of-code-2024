namespace advent_of_code_2024;

public class Day4(Day day)
{
    public void FindSolution()
    {
        var input = new ReadInput().ReadInputFile(day.GetInputFilePath());
        var matrix = CreateMatrixOfLetters(input);
        Console.WriteLine("Part 1:");
        Part1(matrix);
        Console.WriteLine("Part 2:");
        Part2(matrix);
    }

    private void Part1(List<List<Letter>> matrix)
    {
        var sum = 0;
        foreach (var row in matrix)
        {
            foreach (var letter in row)
            {
                if (letter.IsStartPoint)
                {
                    var directions = letter.GetDirections("XMAS");
                    foreach (var direction in directions)
                    {
                        if (letter.Search("XMAS", "X", direction))
                        {
                            sum++;
                        }
                    }
                }
            }
        }
        
        Console.WriteLine(sum);
    }
    
    private void Part2(List<List<Letter>> matrix)
    {
        var sum = 0;
        foreach (var row in matrix)
        {
            foreach (var letter in row)
            {
                if (letter.CouldBeMiddle)
                {
                    if (letter.IsXmasCross())
                    {
                        sum++;
                    }
                }
            }
        }
        
        Console.WriteLine(sum);
    }

    private record Letter(string _letter)
    {
        public readonly string _letter = _letter;
        public Letter? RightTop { get; set; }
        public Letter? Right { get; set; }
        public Letter? RightBottom { get; set; }
        public Letter? Top { get; set; }
        public Letter? LeftTop { get; set; }
        public Letter? Left { get; set; }
        public Letter? LeftBottom { get; set; }
        public Letter? Bottom { get; set; }

        public bool CouldBeMiddle => _letter == "A";

        public bool IsXmasCross()
        {
            bool rightToLeft = false;
            bool leftToRight = false;
            if (RightTop?._letter == "M" && LeftBottom?._letter == "S") { rightToLeft = true; }
            if (RightTop?._letter == "S" && LeftBottom?._letter == "M") { rightToLeft = true; }
            if (LeftTop?._letter == "M" && RightBottom?._letter == "S") { leftToRight = true; }
            if (LeftTop?._letter == "S" && RightBottom?._letter == "M") { leftToRight = true; }
            
            return rightToLeft && leftToRight;
        }
        
        public bool IsStartPoint => _letter == "X";

        public bool Search(string goalWord, string searchWord, string direction)
        {
            if (goalWord == searchWord) return true;
            Letter? nextLetter = null;
            nextLetter = direction switch
            {
                "right" => Right,
                "rightTop" => RightTop,
                "rightBottom" => RightBottom,
                "left" => Left,
                "leftTop" => LeftTop,
                "leftBottom" => LeftBottom,
                "top" => Top,
                "bottom" => Bottom,
                _ => null
            };

            if (nextLetter is null) return false;
            
            var lookingForLetter = goalWord[searchWord.Length].ToString();
            if (nextLetter._letter == lookingForLetter)
            {
                return nextLetter.Search(goalWord, searchWord + lookingForLetter, direction);
            }

            return false;
        }
        
        public string[] GetDirections(string goalWord)
        {
            var nextChar = goalWord[1].ToString();
            var possibleDirections = new List<string>();
            if (RightTop?._letter == nextChar) possibleDirections.Add("rightTop");
            if (RightBottom?._letter == nextChar) possibleDirections.Add("rightBottom");
            if (Right?._letter == nextChar) possibleDirections.Add("right");
            if (Left?._letter == nextChar) possibleDirections.Add("left");
            if (LeftTop?._letter == nextChar) possibleDirections.Add("leftTop");
            if (LeftBottom?._letter == nextChar) possibleDirections.Add("leftBottom");
            if (Top?._letter == nextChar) possibleDirections.Add("top");
            if (Bottom?._letter == nextChar) possibleDirections.Add("bottom");
            return possibleDirections.ToArray();
        }
    }
    
    private List<List<Letter>> CreateMatrixOfLetters(string[] input)
    {
        var allLetters = new List<List<Letter>>();

        for (int i = 0; i < input.Length; i++)
        {
            var row = new List<Letter>();
            for (int j = 0; j < input[i].Length; j++)
            {
                row.Add(new Letter(input[i][j].ToString()));
            }
            allLetters.Add(row);
        }

        var rows = allLetters.Count;
        var cols = allLetters[0].Count;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                var letter = allLetters[i][j];
                letter.Right = j + 1 < cols ? allLetters[i][j + 1] : null;
                letter.RightTop = (i - 1 >= 0 && j + 1 < cols) ? allLetters[i - 1][j + 1] : null;
                letter.RightBottom = (i + 1 < rows && j + 1 < cols) ? allLetters[i + 1][j + 1] : null;
                letter.Left = j - 1 >= 0 ? allLetters[i][j - 1] : null;
                letter.LeftTop = (i - 1 >= 0 && j - 1 >= 0) ? allLetters[i - 1][j - 1] : null;
                letter.LeftBottom = (i + 1 < rows && j - 1 >= 0) ? allLetters[i + 1][j - 1] : null;
                letter.Top = i - 1 >= 0 ? allLetters[i - 1][j] : null;
                letter.Bottom = i + 1 < rows ? allLetters[i + 1][j] : null;
            }
        }

        return allLetters;
    }
}