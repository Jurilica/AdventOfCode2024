namespace AdventOfCode2024.Solutions.Day12;

public static class Day12Solution
{
    private static readonly List<List<char>> InputCopy = ParseInput();
    private static int _edgeCount = 0;
    
    public static long SolveFirstPart()
    {
        var input = ParseInput();
        var result = 0L;
        
        for (var i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] == '#')
                {
                    continue;
                }
                
                var numberOfCells = NumberOfCells(input, input[i][j], i, j);
                result += _edgeCount * numberOfCells;
                _edgeCount = 0;
            }
        }
        
        return result;
    }
    
    private static int NumberOfCells(List<List<char>> input, char c, int x, int y)
    {
        if (x < 0)
        {
            return 0;
        }

        if (x >= input.Count)
        {
            return 0;
        }

        if (y < 0)
        {
            return 0;
        }

        if (y >= input[0].Count)
        {
            return 0;
        }

        if (input[x][y] != c)
        {
            return 0;
        }
        
        input[x][y] = '#';
        PopulateEdgeCount(x, y, c);
        
        return NumberOfCells(input, c, x - 1, y) 
           + NumberOfCells(input, c, x + 1, y)
           + NumberOfCells(input, c, x, y - 1)
           + NumberOfCells(input, c, x, y + 1)
           + 1; // current
    }
    
    private static void PopulateEdgeCount(int x, int y, char c)
    {
        var cnt = 0;
        
        // there is nothing on top
        if (x - 1 < 0)
        {
            cnt++;
        }
        else if (InputCopy[x - 1][y] != c)
        {
            cnt++;
        }
        
        // there is nothing on left
        if (y - 1 < 0)
        {
            cnt++;
        } 
        else if (InputCopy[x][y - 1] != c)
        {
            cnt++;
        }
        
        // there is nothing on right
        if (y + 1 >= InputCopy[x].Count )
        {
            cnt++;
        } 
        else if (InputCopy[x][y + 1] != c)
        {
            cnt++;
        }
        
        // there is nothing on bottom
        if (x + 1 >= InputCopy.Count)
        {
            cnt++;
        }
        else if (InputCopy[x + 1][y] != c)
        {
            cnt++;
        }

        _edgeCount += cnt;
    }
    
    private static List<List<char>> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day12/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines
            .Select(x => x.ToCharArray().ToList())
            .ToList();
    }
}