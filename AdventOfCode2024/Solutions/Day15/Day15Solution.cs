namespace AdventOfCode2024.Solutions.Day15;

public static class Day15Solution
{
    private static readonly List<List<char>> MapCopy = new();
    
    public static long SolveFirstPart()
    {
        var (map, moves) = ParseInput();
        var (x, y) = GetStartingPosition(map);
        
        var result = 0L;

        foreach (var move in moves)
        {
            if (move == '<')
            {
               (x, y) = MoveRobotFirstPart(map, x, y, -1, 0);
            }
            else if (move == '>')
            {
                (x, y) = MoveRobotFirstPart(map, x, y, 1, 0);
            }
            else if (move == '^')
            {
                (x, y) = MoveRobotFirstPart(map, x, y, 0, -1);
            }
            else if (move == 'v')
            {
                (x, y) = MoveRobotFirstPart(map, x, y, 0, 1);
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

    public static long SolveSecondPart()
    {
        var(fistMap, moves) = ParseInput();
        var map = new List<List<char>>();

        for (var i = 0; i < fistMap.Count; i++)
        {
            var row = new List<char>();
            for (var j = 0; j < fistMap[i].Count; j++)
            {
                if (fistMap[i][j] == 'O')
                {
                    row.AddRange(['[',']']);
                }
                else if (fistMap[i][j] == '.')
                {
                    row.AddRange(['.','.']);
                }
                else if (fistMap[i][j] == '@')
                {
                    row.AddRange(['@','.']);
                }
                else if (fistMap[i][j] == '#')
                {
                    row.AddRange(['#','#']);
                }
            }
            map.Add(row);
        }
        
        var (x, y) = GetStartingPosition(map);
        var result = 0L;

        foreach (var move in moves)
        {
            if (move == '<')
            {
                (x, y) = MoveRobotSecondPart(map, x, y, -1, 0);
            }
            else if (move == '>')
            {
                (x, y) = MoveRobotSecondPart(map, x, y, 1, 0);
            }
            else if (move == '^')
            {
                (x, y) = MoveRobotSecondPart(map, x, y, 0, -1);
            }
            else if (move == 'v')
            {
                (x, y) = MoveRobotSecondPart(map, x, y, 0, 1);
            }
        }
        
        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == '[')
                {
                    result += i * 100 + j;
                }
            }
        }
        
        return result;
    }

    private static (int X, int Y) MoveRobotFirstPart(List<List<char>> map, int x, int y, int xDirection, int yDirection)
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
        if (TryMoveBoxFirstPart(map, x + xDirection, y + yDirection, xDirection, yDirection))
        {
            map[y][x] = '.';
            map[y + yDirection][x + xDirection] = '@';
            
            return (x + xDirection, y + yDirection);
        }
        
        return (x, y);
    }

    private static bool TryMoveBoxFirstPart(List<List<char>> map, int x, int y, int xDirection, int yDirection)
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
        
        return TryMoveBoxFirstPart(map, x + xDirection, y + yDirection, xDirection, yDirection);
    }

    private static (int X, int Y) MoveRobotSecondPart(List<List<char>> map, int x, int y, int xDirection, int yDirection)
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
        
        MapCopy.Clear();
        foreach (var row in map)
        {
            MapCopy.Add([..row]);
        }

        // there is box in front
        if (TryMoveBoxSecondPart(map, x + xDirection, y + yDirection, xDirection, yDirection))
        {
            map[y][x] = '.';
            map[y + yDirection][x + xDirection] = '@';
            
            return (x + xDirection, y + yDirection);
        }
        
        // reset arry
        map.Clear();
        foreach (var row in MapCopy)
        {
            map.Add([..row]);
        }

        return (x, y);
    }

    private static bool TryMoveBoxSecondPart(List<List<char>> map, int x, int y, int xDirection, int yDirection)
    {
        var boxStartingPoint = map[y][x] == '[' 
            ? (X: x, Y: y)
            : (X: x - 1, Y: y);
        
        var boxEndPoint = map[y][x] == '[' 
            ? (X: x + 1, Y: y)
            : (X: x, Y: y);

        if (map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == '#')
        {
            return false;
        }

        if (map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] == '#')
        {
            return false;
        }

        var listOfBoxes = new List<(int X, int Y)>();
        
        // check for box on top and bottom
        if (yDirection != 0)
        {
            if (map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == '['
                || map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == ']')
            {
                listOfBoxes.Add((boxStartingPoint.X + xDirection, boxStartingPoint.Y + yDirection));
            }
            
            if (map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] == '[')
            {
                listOfBoxes.Add((boxEndPoint.X + xDirection, boxEndPoint.Y + yDirection));
            }
        }
        
        // check for box on left and right
        if (xDirection != 0)
        {
            // right
            if (map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] == '['
                && xDirection == 1)
            {
                listOfBoxes.Add((boxEndPoint.X + xDirection, boxEndPoint.Y + yDirection));
            }
            
            // left
            if (map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == ']'
                && xDirection == -1)
            {
                listOfBoxes.Add((boxStartingPoint.X + xDirection, boxStartingPoint.Y + yDirection));
            }
        }
        
        var isTrue = listOfBoxes
            .All(p => TryMoveBoxSecondPart(map, p.X, p.Y, xDirection, yDirection));

        if (!isTrue)
        {
            return false;
        }
        
        // move up or down without box
        if (map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == '.'
            && map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] == '.'
            && yDirection != 0)
        {
            map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] = '[';
            map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] = ']';
            map[boxStartingPoint.Y][boxStartingPoint.X] = '.';
            map[boxEndPoint.Y][boxEndPoint.X] = '.';
            return true;
        }
        
        // move right without box
        if (map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == ']'
            && map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] == '.'
            && xDirection == 1)
        {
            map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] = '[';
            map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] = ']';
            map[boxStartingPoint.Y][boxStartingPoint.X] = '.';
            return true;
        }
        
        // move left without box
        if (map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] == '.'
            && map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] == '['
            && xDirection == -1)
        {
            map[boxStartingPoint.Y + yDirection][boxStartingPoint.X + xDirection] = '[';
            map[boxEndPoint.Y + yDirection][boxEndPoint.X + xDirection] = ']';
            map[boxEndPoint.Y][boxEndPoint.X] = '.';
            return true;
        }
        
        return isTrue;
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