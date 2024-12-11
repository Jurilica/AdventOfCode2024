namespace AdventOfCode2024.Solutions.Day01;

public static class Day01Solution
{
    public static long SolveFirstPart()
    {
        var numbers = ParseInput();
        var firstList = numbers[0];
        var secondList = numbers[1];
        
        firstList.Sort();
        secondList.Sort();

        var sum = 0;
        
        for (var i = 0; i < firstList.Count; i++)
        {
            sum += Math.Abs(firstList[i] - secondList[i]);
        }
        
        return sum;
    }
    
    public static long SolveSecondPart()
    {
        var numbers = ParseInput();
        var firstList = numbers[0];
        var secondList = numbers[1];
        
        var sum = 0;

        foreach (var number in firstList)
        {
            var countInList2 = secondList.Count(x => x == number);
            
            sum += countInList2 * number;
        }
        
        return sum;
    }
    private static List<List<int>> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day01/input.txt");
        var lines = file.Split(Environment.NewLine);
        
        var firstList = new List<int>();
        var secondList = new List<int>();

        foreach (var line in lines)
        {
            var numbers = line.Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(int.Parse)
                .ToList();
            
            firstList.Add(numbers[0]);
            secondList.Add(numbers[1]);
        }
        
        return [firstList, secondList];
    }
}