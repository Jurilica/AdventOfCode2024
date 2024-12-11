namespace AdventOfCode2024.Solutions.Day02;

public static class Day02Solution
{
    public static int SolveFirstPart()
    {
        var lines = ParseInput();
        var count = 0;

        foreach (var line in lines)
        {
            if (CheckIfListIsValid(line))
            {
                count++;
            }
        }
        
        return count;
    }
    
    public static int SolveSecondPart()
    {
        var lines = ParseInput();
        var count = 0;

        foreach (var line in lines)
        {
            for (var i = 0; i < line.Count; i++)
            {
                var newList = CopyListWithRemovedElement(line, i);
                if (CheckIfListIsValid(newList))
                {
                    count++;
                    break;
                }
            }
        }
        
        return count;
    }

    private static List<int> CopyListWithRemovedElement(List<int> list, int index)
    {
        var copy = new List<int>(list);
        copy.RemoveAt(index);
        
        return copy;
    }

    private static bool CheckIfListIsValid(List<int> list)
    {
        var sign = 0;
        
        for (var i = 1; i < list.Count; i++)
        {
            var diff = list[i] - list[i - 1];
            
            if (Math.Abs(diff) is < 1 or > 3)
            {
                return false;
            }

            if (diff * sign < 0)
            {
                return false;
            }
            
            sign = diff > 0 ? 1 : -1;
        }
        
        return true;
    }
    
    private static List<List<int>> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day02/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines
            .Select(line => line.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToList())
            .ToList();
    }
}