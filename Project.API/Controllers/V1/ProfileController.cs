using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Account.Deposit;
using Project.Application.Features.Account.Withdraw;
using Project.Application.Features.Profile.ChangePassword;
using Project.Application.Features.Profile.DeleteProfile;
using Project.Application.Features.Profile.GetProfile;
using Project.Application.Features.Profile.UpdateProfile;
using Project.Application.Features.Slot.Command;
using Project.Domain.Aggregates.Transactions;
using Project.Shared.Configuration.Wrappers;

namespace Project.API.Controllers.V1;

[Tags("Profile")]
[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(Response<GetProfileQueryResponse>), 200)]
    public async Task<IActionResult> GetProfile([FromQuery] GetProfileQuery request) => Ok(await Mediator.Send(request));

    [HttpPut]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task UpdateProfile(UpdateProfileCommand request) => await Mediator.Send(request);

    [HttpDelete]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task DeleteProfile(DeleteProfileCommand request) => await Mediator.Send(request);

    [HttpPost(nameof(ChangePassword))]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task ChangePassword(ChangePasswordCommand request) => await Mediator.Send(request);

    [HttpPut(nameof(Deposit))]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task Deposit(DepositCommand request) => await Mediator.Send(request);

    [HttpPut(nameof(Withdraw))]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task Withdraw(WithdrawCommand request) => await Mediator.Send(request);


    [HttpPost(nameof(Slot))]
    [ProducesResponseType(typeof(Response<Transaction>), 200)]
    public async Task Slot(SpinCommand request) => await Mediator.Send(request);
}
