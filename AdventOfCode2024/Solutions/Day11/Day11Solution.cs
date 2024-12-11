namespace AdventOfCode2024.Solutions.Day11;

public static class Day11Solution
{
    public static long Solve(int numberOfBlinks)
    {
        var input = ParseInput();
        var dict = new Dictionary<long, long>();

        foreach (var number in input)
        {
            AddOrUpdateDictionary(dict, number);
        }
        
        for (var i = 0; i < numberOfBlinks; i++)
        {
            Blink(dict);
        }
        
        return dict.Values.Sum();
    }

    private static void Blink(Dictionary<long, long> dict)
    {
        var tempDict = new Dictionary<long, long>(dict);
        dict.Clear();
        
        foreach (var number in tempDict.Keys)
        {
            var count = tempDict[number];
            var numToString = number.ToString();
    
            if (number == 0)
            {
                AddOrUpdateDictionary(dict, 1, count);
            }
            else if (numToString.Length % 2 == 0)
            {
                var firstNumber = numToString.Substring(0, numToString.Length / 2);
                AddOrUpdateDictionary(dict, long.Parse(firstNumber), count);
            
                var secondNumber = numToString.Substring(numToString.Length / 2);
                AddOrUpdateDictionary(dict, long.Parse(secondNumber), count);
            }
            else
            {
                AddOrUpdateDictionary(dict, number * 2024, count);
            }   
        }
    }

    private static void AddOrUpdateDictionary(Dictionary<long, long> dict, long number)
    {
        if (dict.TryGetValue(number, out _))
        {
            dict[number]++;
            return;
        }
        
        dict.Add(number, 1);
    }
    
    private static void AddOrUpdateDictionary(Dictionary<long, long> dict, long number, long count)
    {
        if (dict.TryGetValue(number, out _))
        {
            dict[number] += count;
            return;
        }
        
        dict.Add(number, count);
    }
    
    private static List<long> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day11/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines
            .SelectMany(x => x.Split(" "))
            .Select(long.Parse)
            .ToList();
    }
}