using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Project.Application.Features.Slot.Command;

namespace Project.Application.Hubs;

public class SlotHub : Hub  
{
    private readonly HttpContext _context;
    private readonly IMediator _mediator;
    public SlotHub(IHttpContextAccessor contextAccessor, IMediator mediator)
    {
        _context = contextAccessor.HttpContext;
        _mediator = mediator;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined ");
    }

    public async Task Spin(decimal betAmount)
    {
        var userIdClaim = _context?.User?.Claims?.FirstOrDefault(c => c.Type == "UserID");

        if (userIdClaim != null)
        {
            var userId = userIdClaim.Value;
            var spinCommand = new SpinCommand(betAmount);
            var result = await _mediator.Send(spinCommand);
            var test = Clients.User(userId);
            await Clients.Client(Context.ConnectionId).SendAsync("ReceiveSpinResult", result);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "UserID claim not found.");
        }
    }
}
