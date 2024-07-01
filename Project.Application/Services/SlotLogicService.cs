using Project.Shared.Interfaces;

namespace Project.Application.Services;

public class SlotLogicService : ISlotLogicService
{
    private static readonly string[] Symbols = { "Cherry", "Lemon", "Orange", "Bell" };
    private static readonly Random Random = new Random();

    public string GenerateSlotResult()
    {
        var result = new string[3];
        for (int i = 0; i < 3; i++)
        {
            result[i] = Symbols[Random.Next(Symbols.Length)];
        }
        return string.Join(",", result);
    }

    public decimal CalculateWinAmount(string result)
    {
        var symbols = result.Split(',');
        var groupedSymbols = symbols.GroupBy(s => s).ToList();

        // Payout rules:
        // 3 matching symbols: 10x bet
        // 2 matching symbols: 1x bet
        // 1 or no matching symbols: 0x bet
        if (groupedSymbols.Any(g => g.Count() == 3))
        {
            return 10.0m;
        }
        if (groupedSymbols.Any(g => g.Count() == 2))
        {
            return 1.0m;
        }
        return 0.0m;
    }
}
