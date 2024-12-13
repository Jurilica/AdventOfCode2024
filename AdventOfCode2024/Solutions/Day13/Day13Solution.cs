using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions.Day13;

public static class Day13Solution
{
    public static long SolveFirstPart()
    {
        return Solve(true);
    }
    
    public static long SolveSecondPart()
    {
        return Solve(false);
    }
    
    private static long Solve(bool isFirstPart)
    {
        var games = ParseInput();
        var result = 0L;
        
        foreach (var game in games)
        {
            var addToPrize = isFirstPart ? 0 : 10000000000000;
            
            var newButtonBX = 3 * game.ButtonBX;
            var newButtonBY = 3 * game.ButtonBY;
            var newPrizeX = 3 * (game.PrizeX + addToPrize);
            var newPrizeY = 3 * (game.PrizeY + addToPrize);
            
            // AX + BX = C
            // AY + BY = C
            decimal determinant = game.ButtonAX * newButtonBY - game.ButtonAY * newButtonBX;

            if (determinant == 0)
            {
                continue;
            }
            
            var x = (newPrizeX * newButtonBY - newPrizeY * newButtonBX) / determinant;
            var y = (game.ButtonAX * newPrizeY - game.ButtonAY * newPrizeX) / determinant;

            // check if result is int
            if (x % 1 != 0 || y % 1 != 0)
            {
                continue;
            }
            
            result += (long)x + (long)y;
        }
        
        return result;
    }
    
    private static List<(long ButtonAX, long ButtonAY, long ButtonBX, long ButtonBY, long PrizeX, long PrizeY)> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day13/input.txt");
        
        var buttonARegex = @"Button A: X\+(\d+), Y\+(\d+)";
        var buttonBRegex = @"Button B: X\+(\d+), Y\+(\d+)";
        var prizeRegex = @"Prize: X=(\d+), Y=(\d+)";

        // Split the input by empty lines to handle each group separately
        var groups = file.Split(["\r\n\r\n", "\n\n"], StringSplitOptions.None);

        var list = new List<(long ButtonAX, long ButtonAY, long ButtonBX, long ButtonBY, long PrizeX, long PrizeY)>();
        foreach (var group in groups)
        {
            var buttonAX = 0;
            var buttonAY = 0;
            var buttonBX = 0;
            var buttonBY = 0;
            var prizeX = 0;
            var prizeY = 0;
            
            var buttonAMatch = Regex.Match(group, buttonARegex);
            if (buttonAMatch.Success)
            {
                buttonAX = int.Parse(buttonAMatch.Groups[1].Value);
                buttonAY = int.Parse(buttonAMatch.Groups[2].Value);
            }

            var buttonBMatch = Regex.Match(group, buttonBRegex);
            if (buttonBMatch.Success)
            {
                buttonBX = int.Parse(buttonBMatch.Groups[1].Value);
                buttonBY = int.Parse(buttonBMatch.Groups[2].Value);
            }

            var prizeMatch = Regex.Match(group, prizeRegex);
            if (prizeMatch.Success)
            {
                prizeX = int.Parse(prizeMatch.Groups[1].Value);
                prizeY = int.Parse(prizeMatch.Groups[2].Value);
            }

            list.Add((buttonAX, buttonAY, buttonBX, buttonBY, prizeX, prizeY));
        }

        return list;
    }
}