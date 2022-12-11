using System.Text.RegularExpressions;

namespace Year2022;

public class Monkey
{
    public static List<Monkey> Monkeys = new();

    private readonly Int32 Number;
    private Int64? CurrentItem;
    private readonly Queue<Int64> HoldingItems = null!;

    private readonly Operation Operation;
    public readonly Int32 TestDivisibleBy;
    private readonly Int32 ThrowIfTrue;
    private readonly Int32 ThrowIfFalse;
    public Int64 InspectCount { get; private set; }

    public Monkey(Int32 number, Queue<Int64> holdingItems, Operation operation, Int32 testDivisibleBy,
        Int32 throwIfTrue,
        Int32 throwIfFalse)
    {
        Number = number;
        CurrentItem = holdingItems.Dequeue();
        HoldingItems = holdingItems;
        Operation = operation;
        TestDivisibleBy = testDivisibleBy;
        ThrowIfTrue = throwIfTrue;
        ThrowIfFalse = throwIfFalse;
    }

    public Monkey(String str)
    {
        String[] lines = str.Split('\n');

        foreach (String line in lines)
        {
            if (line.StartsWith("Monkey "))
            {
                Number = Int32.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
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

                HoldingItems = new Queue<Int64>(Regex.Matches(line, @"\d+")
                    .Select(x => Int64.Parse(x.ToString())));
                CurrentItem = HoldingItems.Dequeue();
            }
            else if (line.StartsWith("  Operation: "))
            {
                Operation = new Operation(line.Replace("  Operation: ", ""));
            }
            else if (line.StartsWith("  Test: "))
            {
                TestDivisibleBy = Int32.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
            else if (line.StartsWith("    If true: "))
            {
                ThrowIfTrue = Int32.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
            else if (line.StartsWith("    If false: "))
            {
                ThrowIfFalse = Int32.Parse(Regex.Match(line, @"\d+").Groups[0].Value);
            }
        }
    }

    private void ReceiveItem(Int32 item)
    {
        if (CurrentItem == null)
        {
            CurrentItem = item;
            return;
        }

        HoldingItems.Enqueue(item);
    }

    private void ThrowItem(Int32 monkeyNumber)
    {
        Monkey monkey = Monkeys.First(m => m.Number == monkeyNumber);
        Int32 currentItem = (Int32) CurrentItem!;
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

    private void Inspect(Boolean divideBy3 = true)
    {
        ExecuteOperation();
        if (divideBy3) CurrentItem /= 3;
        InspectCount++;
    }

    public Int64[] GetHoldingItems()
    {
        if (CurrentItem == null)
        {
            return HoldingItems.ToArray();
        }

        Int64[] result = new Int64[HoldingItems.Count + 1];
        result[0] = (Int64) CurrentItem;
        HoldingItems.ToArray().CopyTo(result, 1);
        return result;
    }

    public void ExecuteTurn(Boolean divideBy3 = true)
    {
        while (CurrentItem != null)
        {
            Inspect(divideBy3);
            ThrowItem(CurrentItem % TestDivisibleBy == 0 ? ThrowIfTrue : ThrowIfFalse);
        }
    }
}