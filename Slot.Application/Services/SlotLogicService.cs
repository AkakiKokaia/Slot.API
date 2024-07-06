using Newtonsoft.Json;
using Slot.Shared.Interfaces;

namespace Slot.Application.Services;

public class SlotLogicService : ISlotLogicService
{
    private static readonly Random Random = new Random();
    public class SymbolData
    {
        public SymbolData(int id, string name, decimal[] multiplier)
        {
            Id = id;
            Name = name;
            Multiplier = multiplier;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal[] Multiplier { get; set; }
    }

    private static readonly SymbolData[] Symbols =
    [
        new SymbolData(1, "Cherry", [1.0m, 2.5m, 5.0m]),
        new SymbolData(2, "Lemon", [0.5m, 1.5m, 3.0m]),
        new SymbolData(3, "Orange", [0.8m, 2.0m, 4.0m]),
        new SymbolData(4, "Plum", [0.7m, 1.8m, 3.5m]),
        new SymbolData(5, "Bell", [1.2m, 2.8m, 6.0m]),
        new SymbolData(6, "Bar", [1.5m, 3.0m, 7.0m]),
        new SymbolData(7, "Seven", [2.0m, 5.0m, 10.0m])
    ];

    private static readonly int[][] Paylines =
    [
            [0, 1, 2, 3, 4], // Top row
            [5, 6, 7, 8, 9], // Middle row
            [10, 11, 12, 13, 14], // Bottom row
            [0, 6, 12, 8, 4], // Diagonal top-left to bottom-right
            [10, 6, 2, 8, 14]  // Diagonal bottom-left to top-right
    ];

    public string[,] GenerateSlotResult()
    {
        var result = new string[3, 5];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                result[i, j] = Symbols[Random.Next(Symbols.Length)].Name;
            }
        }
        return result;
    }

    public decimal CalculateWinAmount(string[,] result, decimal betAmount)
    {
        decimal totalWin = 0.0m;

        foreach (var payline in Paylines)
        {
            var symbolsOnLine = payline.Select(index => result[index / 5, index % 5]).ToArray();
            var firstSymbol = symbolsOnLine[0];
            int consecutiveCount = 1;

            for (int i = 1; i < symbolsOnLine.Length; i++)
            {
                if (symbolsOnLine[i] == firstSymbol)
                {
                    consecutiveCount++;
                }
                else
                {
                    break;
                }
            }

            if (consecutiveCount >= 3)
            {
                var multiplier = GetMultiplier(firstSymbol, consecutiveCount);
                totalWin += betAmount * multiplier;
            }
        }

        return totalWin;
    }

    public decimal GetMultiplier(string symbol, int count)
    {
        var symbolData = Symbols.FirstOrDefault(s => s.Name == symbol);
        if (symbolData != null && count >= 3 && count <= 5)
        {
            return symbolData.Multiplier[count - 3];
        }
        return 0.0m;
    }

    public string ConvertResultToString(string[,] result)
    {
        return JsonConvert.SerializeObject(result);
    }
}