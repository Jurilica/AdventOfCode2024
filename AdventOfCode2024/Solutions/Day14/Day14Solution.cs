using System.Text.RegularExpressions;

namespace AdventOfCode2024.Solutions.Day14;

public static class Day14Solution
{
    public static long SolveFirstPart()
    {
        var input = ParseInput();
        var xSize = 101;
        var ySize = 103;
        var middleX = xSize / 2;
        var middleY = ySize / 2;

        var sumOfFirstQuadrant = 0;
        var sumOfSecondQuadrant = 0;
        var sumOfThirdQuadrant = 0;
        var sumOfFourthQuadrant = 0;
        
        foreach (var guard in input)
        {
            var newX = (guard.PositionX + (guard.VelocityX * 100 % xSize)) % xSize;
            var newY = (guard.PostionY + (guard.VelocityY * 100 % ySize)) % ySize;

            if (newX < 0)
            {
                newX = xSize + newX;
            }
            
            if (newY < 0)
            {
                newY = ySize + newY;
            }

            if (newX < middleX && newY < middleY)
            {
                sumOfFirstQuadrant++;
            }
            else if(newX > middleX && newY < middleY)
            {
                sumOfSecondQuadrant++;
            }
            else if (newX < middleX && newY > middleY)
            {
                sumOfThirdQuadrant++;
            }
            else if (newX > middleX && newY > middleY)
            {
                sumOfFourthQuadrant++;
            }
        }
        
        return sumOfFirstQuadrant * sumOfSecondQuadrant * sumOfThirdQuadrant * sumOfFourthQuadrant;
    }
    
    private static List<(int PositionX, int PostionY, int VelocityX, int VelocityY)> ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day14/input.txt");
        
        var regex = @"p=(-?\d+),(-?\d+)\s+v=(-?\d+),(-?\d+)";
        var groups = file.Split(Environment.NewLine);

        var list = new List<(int PositionX, int PostionY, int VelocityX, int VelocityY)>();
        foreach (var group in groups)
        {
            var match = Regex.Match(group, regex);
            list.Add((
                int.Parse(match.Groups[1].Value),
                int.Parse(match.Groups[2].Value),
                int.Parse(match.Groups[3].Value),
                int.Parse(match.Groups[4].Value)));
        }
        return list;
    }
}