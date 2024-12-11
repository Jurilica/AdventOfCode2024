namespace AdventOfCode2024.Solutions.Day10;

public static class Day10Solution
{ 
    public static int SolveFirstPart()
    {
        var input = ParseInput();
        var cnt = 0;

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] == 0)
                {
                    cnt += CheckTrail(input, i, j, 0)
                        .Distinct()
                        .Count();
                }
            }
        }
        
        return cnt;
    }
    
    public static int SolveSecondPart()
    {
        var input = ParseInput();
        var cnt = 0;

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] == 0)
                {
                    cnt += CheckTrail(input, i, j, 0).Count;
                }
            }
        }
        
        return cnt;
    }
    
    private static List<(int X, int Y)> CheckTrail(List<List<int>> input, int x, int y, int currentNumber)
    {
        if (!CheckIfOutOfMap(input, x, y))
        {
            return [];
        }
        
        if (input[x][y] != currentNumber)
        {
            return [];
        }

        if (input[x][y] == 9)
        {
            return [(x, y)];
        }
        
        var list = new List<(int X, int Y)>();
        list.AddRange(CheckTrail(input, x - 1, y, currentNumber + 1));
        list.AddRange(CheckTrail(input, x + 1, y, currentNumber + 1));
        list.AddRange(CheckTrail(input, x, y -1, currentNumber + 1));
        list.AddRange(CheckTrail(input, x, y + 1, currentNumber + 1));

        return list;
    }
    
    private static bool CheckIfOutOfMap(List<List<int>> input, int x, int y)
    {
        if (x < 0 || x >= input.Count)
        {
            return false;
        }

        if (y < 0 || y >= input[0].Count)
        {
            return false;
        }
        
        return true;
    }
    
    private static List<List<int>> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day10/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines
            .Select(line => line
                .Where(x => !string.IsNullOrWhiteSpace(x.ToString()))
                .Select(x => int.Parse(x.ToString()))
                .ToList())
            .ToList();
    }
}