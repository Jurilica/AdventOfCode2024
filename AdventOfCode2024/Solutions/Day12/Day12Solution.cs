namespace AdventOfCode2024.Solutions.Day12;

public static class Day12Solution
{
    private static readonly List<(int X, int Y)> Points =
    [
        (-1, 0),
        (0, 1),
        (1, 0),
        (0, -1)
    ];
    private static readonly List<List<char>> InputCopy = ParseInput();
    private static int _edgeCount = 0;
    private static int _cornerCount = 0;
    
    public static long SolveFirstPart()
    {
        return Solve(false);
    }

    public static long SolveSecondPart()
    {
        return Solve(true);
    }

    private static long Solve(bool useCorners)
    {
        var input = ParseInput();
        var result = 0L;
        
        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] == '#')
                {
                    continue;
                }
                
                var area = CalculateArea(input, input[i][j], i, j);
                if (useCorners)
                {
                    result += area * _cornerCount;   
                }
                else
                {
                    result += area * _edgeCount;
                }
                
                _cornerCount = 0;
                _edgeCount = 0;
            }
        }
        
        return result;

    }
    
    private static int CalculateArea(List<List<char>> input, char c, int x, int y)
    {
        if (IsOutOfMap(x, y))
        {
            return 0;
        }
        
        if (input[x][y] != c)
        {
            return 0;
        }
        
        input[x][y] = '#';
        PopulateEdgeAndCornerCount(x, y, c);
        
        return CalculateArea(input, c, x - 1, y) 
           + CalculateArea(input, c, x + 1, y)
           + CalculateArea(input, c, x, y - 1)
           + CalculateArea(input, c, x, y + 1)
           + 1; // current
    }
    
    private static void PopulateEdgeAndCornerCount(int x, int y, char c)
    {
        var edgeCnt = GetNumberOfEdges(x, y, c); 
        _edgeCount += edgeCnt;
        
        var cornerCnt = GetNumberOfCorners(x, y, c);
        _cornerCount += cornerCnt;
    }

    private static int GetNumberOfEdges(int x, int y, char c)
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
        
        return cnt;
    }
    
    private static int GetNumberOfCorners(int x, int y, char c)
    {
        var cnt = 0;
        
        for (var i = 0; i < Points.Count; i++)
        {
            var firstPoint = Points[i];
            var secondPoint = Points[(i + 1) % Points.Count];
            
            var firstNewPoint = (X: x + firstPoint.X, Y: y + firstPoint.Y);
            var secondNewPoint = (X: x + secondPoint.X, Y: y + secondPoint.Y);
            var thirdNewPoint = (X: x + firstPoint.X + secondPoint.X, Y: y + firstPoint.Y + secondPoint.Y);
            
            if (IsExteriorCorner(firstNewPoint, secondNewPoint, c))
            {
                cnt++;
            }

            if (IsInteriorCorner(firstNewPoint, secondNewPoint, thirdNewPoint, c))
            {
                cnt++;
            }
        }
        
        return cnt;
    }

    private static bool IsExteriorCorner((int X, int Y) firstPoint,(int X, int Y) secondPoint, char c)
    {
        //exterior corner
        //..
        //.A
        if (IsOutOfMap(firstPoint.X, firstPoint.Y) && IsOutOfMap(secondPoint.X, secondPoint.Y))
        {
            return true;
        }

        if (IsOutOfMap(firstPoint.X, firstPoint.Y))
        {
            return InputCopy[secondPoint.X][secondPoint.Y] != c;
        }

        if (IsOutOfMap(secondPoint.X, secondPoint.Y))
        {
            return InputCopy[firstPoint.X][firstPoint.Y] != c;
        }

        if (InputCopy[firstPoint.X][firstPoint.Y] != c && InputCopy[secondPoint.X][secondPoint.Y] != c)
        {
            return true;
        }
        
        return false;
    }

    private static bool IsInteriorCorner((int X, int Y) firstPoint, (int X, int Y) secondPoint, (int X, int Y) thirdPoint,  char c)
    {
        //interior corner
        //CC
        //C.
        if (IsOutOfMap(firstPoint.X, firstPoint.Y))
        {
            return false;
        }

        if (IsOutOfMap(secondPoint.X, secondPoint.Y))
        {
            return false;
        }

        if (IsOutOfMap(thirdPoint.X, thirdPoint.Y))
        {
            return InputCopy[firstPoint.X][firstPoint.Y] == c && InputCopy[secondPoint.X][secondPoint.Y] == c;
        }

        if (InputCopy[thirdPoint.X][thirdPoint.Y] != c)
        {
            return InputCopy[firstPoint.X][firstPoint.Y] == c && InputCopy[secondPoint.X][secondPoint.Y] == c;
        }
        
        return false;
    }

    private static bool IsOutOfMap(int x, int y)
    {
        if (x < 0)
        {
            return true;
        }

        if (x >= InputCopy.Count)
        {
            return true;
        }

        if (y < 0)
        {
            return true;
        }

        if (y >= InputCopy[0].Count)
        {
            return true;
        }
        
        return false;
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