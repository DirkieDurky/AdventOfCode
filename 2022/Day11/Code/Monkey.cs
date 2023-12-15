using System.Text.RegularExpressions;

namespace Year2022;

public class Monkey
{
    public static List<Monkey> Monkeys = new();

    private readonly int Number;
    private long? CurrentItem;
    private readonly Queue<long> HoldingItems = null!;

    private readonly Operation Operation;
    public readonly int TestDivisibleBy;
    private readonly int ThrowIfTrue;
    private readonly int ThrowIfFalse;
    public long InspectCount { get; private set; }

    public Monkey(int number, Queue<long> holdingItems, Operation operation, int testDivisibleBy,
        int throwIfTrue,
        int throwIfFalse)
    {
        Number = number;
        CurrentItem = holdingItems.Dequeue();
        HoldingItems = holdingItems;
        Operation = operation;
        TestDivisibleBy = testDivisibleBy;
        ThrowIfTrue = throwIfTrue;
        ThrowIfFalse = throwIfFalse;
    }

    public Monkey(string str)
    {
        string[] lines = str.Split('\n');

        foreach (string line in lines)
        {
            if (line.StartsWith("Monkey "))
            {
                Number = int.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
            else if (line.StartsWith("  Starting items: "))
            {
                // var matches = Regex.Matches(line, @"\d+");
                // foreach (var match in matches)
                // {
                //     Console.WriteLine(match);
                // }
                //
                // Console.WriteLine();

                HoldingItems = new Queue<long>(Regex.Matches(line, @"\d+")
                    .Select(x => long.Parse(x.ToString())));
                CurrentItem = HoldingItems.Dequeue();
            }
            else if (line.StartsWith("  Operation: "))
            {
                Operation = new Operation(line.Replace("  Operation: ", ""));
            }
            else if (line.StartsWith("  Test: "))
            {
                TestDivisibleBy = int.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
            else if (line.StartsWith("    If true: "))
            {
                ThrowIfTrue = int.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
            else if (line.StartsWith("    If false: "))
            {
                ThrowIfFalse = int.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
        }
    }

    private void ReceiveItem(int item)
    {
        if (CurrentItem == null)
        {
            CurrentItem = item;
            return;
        }

        HoldingItems.Enqueue(item);
    }

    private void ThrowItem(int monkeyNumber)
    {
        Monkey monkey = Monkeys.First(m => m.Number == monkeyNumber);
        int currentItem = (int)CurrentItem!;
        monkey.ReceiveItem(currentItem);
        if (HoldingItems.Count > 0)
        {
            CurrentItem = HoldingItems.Dequeue();
        }
        else
        {
            CurrentItem = null;
        }
    }

    private void ExecuteOperation()
    {
        CurrentItem = Operation.Operator switch
        {
            '*' => (CurrentItem * Operation.Value) % Day11.ModNumber,
            '+' => (CurrentItem + Operation.Value) % Day11.ModNumber,
            '^' => (CurrentItem * CurrentItem) % Day11.ModNumber,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    private void Inspect(bool divideBy3 = true)
    {
        ExecuteOperation();
        if (divideBy3) CurrentItem /= 3;
        InspectCount++;
    }

    public long[] GetHoldingItems()
    {
        if (CurrentItem == null)
        {
            return HoldingItems.ToArray();
        }

        long[] result = new long[HoldingItems.Count + 1];
        result[0] = (long)CurrentItem;
        HoldingItems.ToArray().CopyTo(result, 1);
        return result;
    }

    public void ExecuteTurn(bool divideBy3 = true)
    {
        while (CurrentItem != null)
        {
            Inspect(divideBy3);
            ThrowItem(CurrentItem % TestDivisibleBy == 0 ? ThrowIfTrue : ThrowIfFalse);
        }
    }
}
