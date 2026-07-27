namespace Slot.Shared.Interfaces;

public interface ISlotLogicService
{
    decimal CalculateWinAmount(string[,] result, decimal betAmount);
    decimal GetMultiplier(string symbol, int count);
    bool IsBonusWin();
    string[,] GenerateSlotResult();
    string ConvertResultToString(string[,] result);
}
