using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;

namespace Project.Application.Hubs;

public class SlotHub : Hub  
{
    private readonly IHttpContextAccessor _contextAccessor;
    public SlotHub(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined ");
    }

    public async Task Isa()
    {
        var user = _contextAccessor.HttpContext.User;
        var d = user;
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined ");
    }
}
