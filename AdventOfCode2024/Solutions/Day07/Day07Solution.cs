namespace AdventOfCode2024.Solutions.Day07;

public static class Day07Solution
{
    public static long SolveFirstPart()
    {
        return CalculateResult(2);
    }
    
    public static long SolveSecondPart()
    {
        return CalculateResult(3);
    }

    private static long CalculateResult(int numberOfOperators)
    {
        var input = ParseInput();
        var sum = 0L;
        
        foreach (var line  in input)
        {
            var splitLine = line.Split(':');
            
            var result = long.Parse(splitLine[0]);
            var numbers = splitLine[1].Split(' ')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(long.Parse)
                .ToList();

            var combinations = GenerateCombinations(numbers.Count - 1, numberOfOperators);

            foreach (var combination in combinations)
            {
                var reconciliationResult = numbers[0];

                for (var i = 0; i < combination.Length; i++)
                {
                    if (combination[i] == '0')
                    {
                        reconciliationResult += numbers[i + 1];
                    }
                    else if(combination[i] == '1')
                    {
                        reconciliationResult *= numbers[i + 1];
                    }
                    else
                    {
                        var numberOfDigits = numbers[i + 1].ToString().Length;
                        reconciliationResult = reconciliationResult * (long)Math.Pow(10, numberOfDigits) + numbers[i + 1];
                    }
                }
                
                if (result == reconciliationResult)
                {
                    sum += result;
                    break;
                }
            }
        }
        
        return sum;
    }

    private static List<string> GenerateCombinations(int n, int baseValue)
    {
        var result = new List<string>();

        var totalCombinations = (int)Math.Pow(baseValue, n);

        for (var i = 0; i < totalCombinations; i++)
        {
            var combination = string.Empty;
            var num = i;

            for (var j = 0; j < n; j++)
            {
                combination = (num % baseValue) + combination;
                num /= baseValue;
            }

            result.Add(combination);
        }

        return result;
    }

    private static List<string> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day07/input.txt");
        var lines = file.Split(Environment.NewLine);

        return lines.ToList();
    }
}