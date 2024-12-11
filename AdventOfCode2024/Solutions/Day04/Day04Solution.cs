namespace AdventOfCode2024.Solutions.Day04;

public static class Day04Solution
{ 
    public static int SolveFirstPart()
    {
        var count = 0;
        var lines = ParseInput();

        var wordsToCheck = new[]
        {
            "XMAS",
            "SAMX"
        };

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            
            for (var j = 0; j < line.Length; j++)
            {
                foreach (var wordToCheck in wordsToCheck)
                {
                    if (line[j] == wordToCheck[0])
                    {
                        if (CheckHorizontal(line, j, wordToCheck)) count++;
                        if (CheckVertical(lines, i, j, wordToCheck)) count++;
                        if (CheckRightDiagonal(lines, i, j, wordToCheck)) count++;
                        if (CheckLeftDiagonal(lines, i, j, wordToCheck)) count++;
                    }
                }
            }
        }
        
        return count;
    }

    public static int SolveSecondPart()
    {
        var count = 0;
        var lines = ParseInput();

        var combinations = new List<(string Word1, string Word2)>
        {
            ("MAS", "MAS"),
            ("MAS", "SAM"),
            ("SAM", "MAS"),
            ("SAM", "SAM")
        };
        
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            
            for (var j = 0; j < line.Length; j++)
            {
                if (line[j] == 'S' || line[j] == 'M')
                {
                    foreach (var combination in combinations)
                    {
                        if (CheckRightDiagonal(lines, i, j, combination.Word1) 
                            && CheckLeftDiagonal(lines, i, j + 2, combination.Word2))
                        {
                            count++;
                            break;
                        }
                    }
                    
                    
                }
            }
        }
        
        return count;
    }

    private static bool CheckHorizontal(string line, int index, string word)
    {
        var count = 0;
        for (var i = index; i < line.Length; i++)
        {
            if (line[i] != word[count++])
            {
                return false;
            }
            
            if (count == word.Length)
            {
                return true;
            }
        }
        
        return false;
    }

    private static bool CheckVertical(List<string> lines, int x, int y, string word)
    {
        var count = 0;
        for (var i = x; i < lines.Count; i++)
        {
            if (lines[i][y] != word[count++])
            {
                return false;
            }
            
            if (count == word.Length)
            {
                return true;
            }
        }
        
        return false;
    }
    
    private static bool CheckRightDiagonal(List<string> lines, int x, int y, string word)
    {
        var count = 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (x + i >= lines.Count)
            {
                return false;
            }

            if (y + i >= lines[x + i].Length)
            {
                return false;
            }
            
            if (lines[x + i][y + i] != word[count++])
            {
                return false;
            }
            
            if (count == word.Length)
            {
                return true;
            }
        }
        
        return false;
    }
    
    private static bool CheckLeftDiagonal(List<string> lines, int x, int y, string word)
    {
        var count = 0;
        for (var i = 0; i < word.Length; i++)
        {
            if (x + i >= lines.Count)
            {
                return false;
            }

            if (y - i < 0)
            {
                return false;
            }
            
            if (lines[x + i][y - i] != word[count++])
            {
                return false;
            }
            
            if (count == word.Length)
            {
                return true;
            }
        }
        
        return false;
    }
    
    private static List<string> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day04/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines.ToList();
    }
}