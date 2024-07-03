using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Project.Application.Features.Slot.Command;
using Project.Application.Features.Slot.Query.DataModels;
using Project.Application.Hubs;
using Project.Domain.Aggregates.Transactions;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Domain.Aggregates.Users.Entity;
using Project.Shared.Interfaces;
using Project.Test.Mocks;
using System.Security.Claims;

namespace Project.Test.Features.Slot.Command;

public class SpinCommandHandlerTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
    private readonly Mock<ISlotLogicService> _slotLogicServiceMock;
    private readonly Mock<IHubContext<SlotHub>> _hubContextMock;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SpinCommandHandlerTests()
    {
        _userManagerMock = new Mock<UserManager<User>>(
            Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
        _transactionRepositoryMock = new Mock<ITransactionRepository>();
        _slotLogicServiceMock = new Mock<ISlotLogicService>();
        _hubContextMock = new Mock<IHubContext<SlotHub>>();
        _httpContextAccessor = MockHttpContextAccessor.GetMockedHttpContextAccessor();
    }

    [Fact]
    public async Task Handle_Success()
    {
        try
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Balance = 1000m };
            var betAmount = 100m;
            var winAmount = 200m;
            var expectedBalance = 1000m - betAmount + winAmount;

            _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("UserID", userId.ToString())
            }));

            _userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _slotLogicServiceMock.Setup(x => x.GenerateSlotResult()).Returns(new string[3, 5] { { "A", "B", "C", "D", "E" }, { "A", "B", "C", "D", "E" }, { "A", "B", "C", "D", "E" } });
            _slotLogicServiceMock.Setup(x => x.CalculateWinAmount(It.IsAny<string[,]>(), betAmount)).Returns(winAmount);
            _slotLogicServiceMock.Setup(x => x.ConvertResultToString(It.IsAny<string[,]>())).Returns("A,B,C,D,E;A,B,C,D,E;A,B,C,D,E");
            _transactionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Transaction>())).Returns(Task.CompletedTask);
            _transactionRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _userManagerMock.Setup(x => x.UpdateAsync(user)).ReturnsAsync(IdentityResult.Success);

            var handler = new SpinCommandHandler(_userManagerMock.Object, _transactionRepositoryMock.Object, _slotLogicServiceMock.Object, _httpContextAccessor, _hubContextMock.Object);

            // Act
            var result = await handler.Handle(new SpinCommand(betAmount), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedBalance, result.CurrentBalance);
            Assert.Equal(winAmount, result.WinAmount);
            Assert.Equal(betAmount, result.BetAmount);
            Assert.Equal("A,B,C,D,E;A,B,C,D,E;A,B,C,D,E", result.SlotResult);
        }
        catch (Exception ex)
        {
            // Log the exception message
            Console.WriteLine($"Test failed: {ex.Message}");
            throw;
        }
    }

    [Fact]
    public async Task Handle_InsufficientBalance()
    {
        try
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Balance = 50m }; // Insufficient balance for the bet amount
            var betAmount = 100m;

            _httpContextAccessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("UserID", userId.ToString())
            }));

            _userManagerMock.Setup(x => x.FindByIdAsync(userId.ToString())).ReturnsAsync(user);
            _slotLogicServiceMock.Setup(x => x.GenerateSlotResult()).Returns(new string[3, 5] { { "A", "B", "C", "D", "E" }, { "A", "B", "C", "D", "E" }, { "A", "B", "C", "D", "E" } });
            _slotLogicServiceMock.Setup(x => x.CalculateWinAmount(It.IsAny<string[,]>(), betAmount)).Returns(0m);
            _slotLogicServiceMock.Setup(x => x.ConvertResultToString(It.IsAny<string[,]>())).Returns("A,B,C,D,E;A,B,C,D,E;A,B,C,D,E");

            var handler = new SpinCommandHandler(_userManagerMock.Object, _transactionRepositoryMock.Object, _slotLogicServiceMock.Object, _httpContextAccessor, _hubContextMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(new SpinCommand(betAmount), CancellationToken.None));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
            throw;
        }
    }
}
