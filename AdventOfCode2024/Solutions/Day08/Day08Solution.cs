using System.Runtime.Intrinsics.X86;

namespace AdventOfCode2024.Solutions.Day08;

public static class Day08Solution
{
    public static int SolveFirstPart()
    {
        return Solve(false);
    }
    
    public static int SolveSecondPart()
    {
        return Solve(true);
    }

    private static int Solve(bool addInWholeLine)
    {
        var input = ParseInput();
        var antinodeMap = ParseInput();

        var alreadyDoneAntenas = new HashSet<char>();

        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Count; j++)
            {
                if (input[i][j] == '.')
                {
                    continue;
                }

                if (alreadyDoneAntenas.Contains(input[i][j]))
                {
                    continue;
                }
                
                var allIndexes = GetIndexesWithSameChar(input, input[i][j]);

                if (addInWholeLine)
                {
                    AddAntinodeToMapInLine(allIndexes, antinodeMap);
                }
                else
                {
                    AddAntinodeToMap(allIndexes, antinodeMap);   
                }
                alreadyDoneAntenas.Add(input[i][j]);
            }
        }
        
        return antinodeMap
            .Select(x => x
                .Where(y => y == '#')
                .Count())
            .Sum();
    }

    private static List<(int X, int Y)> GetIndexesWithSameChar(List<List<char>> input, char c)
    {
        return input
            .SelectMany((innerList, outerIndex) => innerList
                .Select((c, innerIndex) =>
                    new
                    {
                        X = outerIndex, 
                        Y = innerIndex,
                        C = c
                    })
                .Where(x => x.C == c)
                .Select(x => (x.X, x.Y)))
            .ToList();
    }
    
    private static void AddAntinodeToMapInLine(List<(int X, int Y)> indexes, List<List<char>> antinodeMap)
    {
        foreach (var index in indexes)
        {
            foreach (var innerIndex in indexes)
            {
                if (innerIndex.X == index.X && innerIndex.Y == index.Y)
                {
                    continue;
                }
               
                var xDiff = index.X - innerIndex.X;
                var yDiff = index.Y - innerIndex.Y;
                
                MarkAntinodeInLine(index, antinodeMap, xDiff, yDiff);
                MarkAntinodeInLine(index, antinodeMap, -xDiff, -yDiff);
            }
        }
    }

    private static void MarkAntinodeInLine((int X, int Y) index, List<List<char>> antinodeMap, int xDiff, int yDiff)
    {
        var location = index;
        while (true)
        {
            if (location.X < 0)
            {
                break;
            }

            if (location.Y < 0)
            {
                break;
            }

            if (location.X >= antinodeMap.Count)
            {
                break;
            }

            if (location.Y >= antinodeMap[0].Count)
            {
                break;
            }
            
            antinodeMap[location.X][location.Y] = '#';
            location = (location.X + xDiff, location.Y + yDiff);
        }
    }

    private static void AddAntinodeToMap(List<(int X, int Y)> indexes, List<List<char>> antinodeMap)
    {
        foreach (var index in indexes)
        {
            foreach (var innerIndex in indexes)
            {
                if (innerIndex.X == index.X && innerIndex.Y == index.Y)
                {
                    continue;
                }
               
                var xDiff = index.X - innerIndex.X;
                var yDiff = index.Y - innerIndex.Y;

                var possibleAntinodeLocations = new List<(int X, int Y)>
                {
                    (index.X + xDiff, index.Y + yDiff),
                    (innerIndex.X - xDiff, innerIndex.Y - yDiff)
                };

                foreach (var antinodeLocation in possibleAntinodeLocations)
                {
                    if (antinodeLocation.X < 0)
                    {
                        continue;
                    }

                    if (antinodeLocation.Y < 0)
                    {
                        continue;
                    }

                    if (antinodeLocation.X >= antinodeMap.Count)
                    {
                        continue;
                    }

                    if (antinodeLocation.Y >= antinodeMap[0].Count)
                    {
                        continue;
                    }
                    
                    antinodeMap[antinodeLocation.X][antinodeLocation.Y] = '#';
                }
            }
        }
    }
    
    private static List<List<char>> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day08/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines
            .Select(x => x.ToCharArray().ToList())
            .ToList();
    }
}