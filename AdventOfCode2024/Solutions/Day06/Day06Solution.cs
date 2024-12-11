namespace AdventOfCode2024.Solutions.Day06;

public static class Day06Solution
{
    private static List<(int XChange, int YChange)> Directions =
    [
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    ];
    
    public static int SolveFirstPart()
    {
        var input = ParseInput();

        var (x, y) = GetStartingPosition(input);

        var currentDirectionIndex = 0;

        while (true)
        {
            input[x][y] = 'X';
            
            var direction = Directions[currentDirectionIndex % 4];
            
            var newX = x + direction.XChange;
            var newY = y + direction.YChange;

            if (IsOutOfMap(input, newX, newY))
            {
                break;
            }

            if (input[newX][newY] == '#')
            {
                currentDirectionIndex++;
                continue;
            }
            
            x += direction.XChange;
            y += direction.YChange;
        }

        return input
            .Sum(s => s.Count(c => c == 'X'));
    }
    
    public static int SolveSecondPart()
    {
        var input = ParseInput();

        var count = 0;

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] != '.')
                {
                    continue;
                }
                
                input[i][j] = '#';
                
                if (IsStuckInLoop(input))
                {
                    count++;
                }
                
                input[i][j] = '.';
            }
        }
        
        return count;
    }

    private static bool IsStuckInLoop(List<List<char>> input)
    {
        var (x, y) = GetStartingPosition(input);
        var currentDirectionIndex = 0;

        var alreadyVisited = new HashSet<(int X, int Y, int DirectionIndex)>();
        
        while (true)
        {
            var directionIndex = currentDirectionIndex % 4;

            if(alreadyVisited.TryGetValue((x, y, directionIndex), out _))
            {
                return true;
            }
            
            alreadyVisited.Add((x, y, directionIndex));
            
            var direction = Directions[directionIndex];
            
            var newX = x + direction.XChange;
            var newY = y + direction.YChange;
        
            if (IsOutOfMap(input, newX, newY))
            {
                break;
            }
            
            if (input[newX][newY] == '#')
            {
                currentDirectionIndex++;
                continue;
            }
            
            x = newX;
            y = newY;
        }
        
        return false;
    }

    private static (int X, int Y) GetStartingPosition(List<List<char>> input)
    {
        var startChar = '^';
        
        var startingLine = input
            .Single(x => x.Contains(startChar));
        
        var startingXPoint = input.IndexOf(startingLine);
        var staringYPoint = startingLine.IndexOf(startChar);
        
        return (startingXPoint, staringYPoint);
    }

    private static bool IsOutOfMap(List<List<char>> input, int x, int y)
    {
        return x < 0 
               || y < 0 
               || x >= input.Count 
               || y >= input[0].Count;
    }
    
    private static List<List<char>> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day06/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines
            .Select(x => x.ToCharArray().ToList())
            .ToList();
    }
}