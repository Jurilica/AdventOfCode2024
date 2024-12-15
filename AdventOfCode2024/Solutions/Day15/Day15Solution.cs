namespace AdventOfCode2024.Solutions.Day15;

public static class Day15Solution
{
    public static long SolveFirstPart()
    {
        var (map, moves) = ParseInput();

        var (x, y) = GetStartingPosition(map);
        
        var result = 0L;

        foreach (var move in moves)
        {
            if (move == '<')
            {
               (x, y) = MoveRobot(map, x, y, -1, 0);
            }
            else if (move == '>')
            {
                (x, y) = MoveRobot(map, x, y, 1, 0);
            }
            else if (move == '^')
            {
                (x, y) = MoveRobot(map, x, y, 0, -1);
            }
            else if (move == 'v')
            {
                (x, y) = MoveRobot(map, x, y, 0, 1);
            }
        }

        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == 'O')
                {
                    result += i * 100 + j;
                }
            }
        }
        
        return result;
    }

    private static (int X, int Y) MoveRobot(List<List<char>> map, int x, int y, int xDirection, int yDirection)
    {
        // hit the wall
        if (map[y + yDirection][x + xDirection] == '#')
        {
            return (x, y);
        }

        // there is nothing in front
        if (map[y + yDirection][x + xDirection] == '.')
        {
            map[y][x] = '.';
            map[y + yDirection][x + xDirection] = '@';
            
            return (x + xDirection, y + yDirection);
        }

        // there is box in front
        if (TryMoveBox(map, x + xDirection, y + yDirection, xDirection, yDirection))
        {
            map[y][x] = '.';
            map[y + yDirection][x + xDirection] = '@';
            
            return (x + xDirection, y + yDirection);
        }
        
        return (x, y);
    }

    private static bool TryMoveBox(List<List<char>> map, int x, int y, int xDirection, int yDirection)
    {
        if (map[y + yDirection][x + xDirection] == '.')
        {
            map[y + yDirection][x + xDirection] = 'O';
            return true;
        }
        
        if (map[y + yDirection][x + xDirection] == '#')
        {
            return false;
        }
        
        return TryMoveBox(map, x + xDirection, y + yDirection, xDirection, yDirection);
    }
    
    private static (int X, int Y) GetStartingPosition(List<List<char>> input)
    {
        var startChar = '@';
        
        var startingLine = input
            .Single(x => x.Contains(startChar));
        
        var staringYPoint = input.IndexOf(startingLine);
        var startingXPoint = startingLine.IndexOf(startChar);
        
        return (startingXPoint, staringYPoint);
    }
    
    private static (List<List<char>> Map, string Moves) ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day15/input.txt");
        var lines = file.Split(Environment.NewLine);
        
         var map = lines
            .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.ToCharArray().ToList())
            .ToList();

         var moves = lines
             .SkipWhile(x => !string.IsNullOrWhiteSpace(x))
             .Where(x => !string.IsNullOrWhiteSpace(x))
             .ToList();
         
         return (map, string.Join(string.Empty, moves));
    }
}