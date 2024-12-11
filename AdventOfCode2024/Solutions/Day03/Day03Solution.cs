using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions.Day03;

public static class Day03Solution
{
    public static long SolveFirstPart()
    {
        var input = ParseInput();

        var result = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)")
            .Matches(input)
            .Select(x => int.Parse(x.Groups[1].Value) * int.Parse(x.Groups[2].Value))
            .Sum();

        return result;
    }

    public static long SolveSecondPart()
    {
        var input = ParseInput();

        var matches = new Regex(@"mul\((-?\d+),(-?\d+)\)|do\(\)|don't\(\)").Matches(input);
        var result = 0;
        var enabled = true;
        
        foreach (Match match in matches)
        {
            if (match.Groups[0].Value.StartsWith("mul("))
            {
                result += enabled 
                    ? int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value) 
                    : 0;
            }
            else if (match.Groups[0].Value == "do()")
            {
                enabled = true;
            }
            else if (match.Groups[0].Value == "don't()")
            {
                enabled = false;
            }
        }

        return result;
    }

    private static string ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day03/input.txt")
            .Replace(Environment.NewLine, " ");
        
        return file;
    }
}