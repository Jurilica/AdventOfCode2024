namespace AdventOfCode2024.Solutions.Day05;

public static class Day05Solution
{
    public static int SolveFirstPart()
    {
        var (books, mappedRules, _) = ParseInput();

        var sum = 0;
        foreach (var book in books)
        {
            var isValid = IsValidOrder(book, mappedRules);

            if (isValid)
            {
                sum += book[book.Count / 2];
            }
        }

        return sum;
    }

    public static int SolveSecondPart()
    {
        var (books, mappedRules, rules) = ParseInput();
        
        var sum = 0;
        var notValidBooks = books
            .Where(x => !IsValidOrder(x, mappedRules))
            .ToList();

        foreach (var book in notValidBooks)
        {
            var validRules = rules
                .Where(x => book.Contains(x[0]))
                .Where(x => book.Contains(x[1]))
                .GroupBy(x => x.First(), x => x.Last())
                .Select(x => 
                    new
                    {
                        Key = x.Key,
                        Count = x.Count()
                    })
                    .ToList();
            
            var result = book
                .Select(x =>
                {
                    var match = validRules
                        .Where(y => y.Key == x)
                        .FirstOrDefault();
                    
                    return match != null 
                        ? (match.Key, match.Count) 
                        : (x, 0);
                })
                .OrderBy(x => x.Item2)
                .Select(x => x.Item1)
                .ToList();

            sum += result[result.Count / 2];
        }

        return sum;
    }

    private static bool IsValidOrder(List<int> book, Dictionary<int, List<int>> mappedRules)
    {
        var hasSet = new HashSet<int>();
        
        foreach (var page in book)
        {
            if(!mappedRules.TryGetValue(page, out var pageRules))
            {
                hasSet.Add(page);
                continue;
            }
                
            foreach (var pageRule in pageRules)
            {
                if (hasSet.Contains(pageRule))
                {
                    return false;
                }
            }
                
            hasSet.Add(page);
        }

        return true;
    }
    
    private static (List<List<int>> books, Dictionary<int, List<int>> mappedRules, List<List<int>> rules) ParseInput()
    {
        var file = File.ReadAllText("Solutions/Day05/input.txt");
        var lines = file.Split(Environment.NewLine);
        
        var rules = lines
            .TakeWhile(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Split('|')
                .Select(int.Parse)
                .ToList())
            .ToList();
        
        var books = lines
            .Skip(rules.Count + 1)
            .Select(x => x.Split(',')
                .Select(int.Parse)
                .ToList())
            .ToList();
        
        var mappedRules = rules
            .GroupBy(x => x.First(), x => x.Last())
            .ToDictionary(x => x.Key, x => x.ToList());

        return (books, mappedRules, rules);
    }
}