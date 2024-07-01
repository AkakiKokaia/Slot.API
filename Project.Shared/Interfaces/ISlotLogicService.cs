namespace Project.Shared.Interfaces;

public interface ISlotLogicService
{
    string GenerateSlotResult();
    decimal CalculateWinAmount(string result);
}
