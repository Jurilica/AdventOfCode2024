namespace AdventOfCode2024.Solutions.Day09;

public static class Day09Solution
{
    public static long SolveFirstPart()
    {
        var strArray = GetProcessedInput();

        var left = 0;
        var right = strArray.Count - 1;
        
        while (true)
        {
            while (strArray[left] != ".")
            {
                left++;
            }

            while (strArray[right] == ".")
            {
                right--;
            }

            if (left >= right)
            {
                break;
            }
            
            strArray[left] = strArray[right];
            strArray[right] = ".";
        }

        var checkSum = 0L;

        for (var i = 0; i < strArray.Count; i++)
        {
            if (strArray[i] == ".")
            {
                break;
            }
            
            checkSum += int.Parse(strArray[i]) * i;
        }

        return checkSum;
    }
    
    public static long SolveSecondPart()
    {
        var strArray = GetProcessedInput();
        
        for (var right = strArray.Count - 1; right >= 0; right--)
        {
            if(strArray[right] == ".")
            {
                continue;
            }
            
            var spaceNeeded = 0;

            while (strArray[right] == strArray[right - spaceNeeded])
            {
                spaceNeeded++;

                if (right - spaceNeeded < 0)
                {
                    break;
                }
            }
            
            for (var left = 0; left < right; left++)
            {
                if (strArray[left] != ".")
                {
                    continue;
                }

                var spaceAvailable = 0;
                
                while (strArray[left + spaceAvailable] == ".")
                {
                    spaceAvailable++;

                    if (left + spaceAvailable >= right)
                    {
                        break;
                    }
                }

                if (spaceAvailable >= spaceNeeded)
                {
                    for (var i = 0; i < spaceNeeded; i++)
                    {
                        strArray[left + i] = strArray[right - i];
                        strArray[right - i] = ".";
                    }
                }
                
                left += spaceAvailable - 1;
            }

            right -= spaceNeeded - 1;
        }
        var checkSum = 0L;

        for (var i = 0; i < strArray.Count; i++)
        {
            if (strArray[i] == ".")
            {
                continue;
            }
            
            checkSum += int.Parse(strArray[i]) * i;
        }

        return checkSum;
    }

    private static List<string> GetProcessedInput()
    {
        var input = ParseInput();

        var number = 0;
        var isEmptySpace = false;

        var strArray = new List<string>();

        foreach (var item in input)
        {
            var count = int.Parse(item.ToString());
            if (isEmptySpace)
            {
                for (var i = 0; i < count; i++)
                {
                    strArray.Add(".");
                }
            }
            else
            {
                for (var i = 0; i < count; i++)
                {
                    strArray.Add(number.ToString());
                }

                number++;
            }
            
            isEmptySpace = !isEmptySpace;
        }
        
        return strArray;
    }
    
    private static string ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day09/input.txt");
        
        return file;
    }
}