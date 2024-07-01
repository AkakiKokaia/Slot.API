using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Account.CreateUser;
using Project.Application.Features.Account.SignIn;
using Project.Shared.Configuration.Wrappers;

namespace Project.API.Controllers.V1;

[Tags("Account")]
[Route("api/v1/[controller]")]
[ApiController]
public class AccountController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost(nameof(CreateUser))]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task CreateUser(CreateUserCommand request) => await Mediator.Send(request);

    [AllowAnonymous]
    [HttpPost(nameof(SignIn))]
    [ProducesResponseType(typeof(Response<Unit>), 200)]
    public async Task<IActionResult> SignIn(SignInCommand request) => Ok(await Mediator.Send(request));
}