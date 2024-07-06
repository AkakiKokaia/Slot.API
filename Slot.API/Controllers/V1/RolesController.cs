using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slot.Application.Features.Roles.AddRole;
using Slot.Application.Features.Roles.DeleteRole;
using Slot.Application.Features.Roles.GetRoleById;
using Slot.Application.Features.Roles.GetRoles;
using Slot.Application.Features.Roles.UpdateRole;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.API.Controllers.V1
{
    [Tags("Roles")]
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(Response<GetRolesQueryResponse>), 200)]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQuery request) => Ok(await Mediator.Send(request));

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(Response<GetRoleByIdQueryResponse>), 200)]
        public async Task<IActionResult> GetRoleById(Guid Id) => Ok(await Mediator.Send(new GetRoleByIdQuery(Id)));

        [HttpPost]
        [ProducesResponseType(typeof(Response<Guid>), 200)]
        public async Task<Response<Guid>> AddRole(AddRoleCommand request) => await Mediator.Send(request);

        [HttpPut]
        [ProducesResponseType(typeof(Response<Unit>), 200)]
        public async Task UpdateRole(UpdateRoleCommand request) => await Mediator.Send(request);

        [HttpDelete]
        [ProducesResponseType(typeof(Response<Unit>), 200)]
        public async Task DeleteRole(DeleteRoleCommand request) => await Mediator.Send(request);
    }
}
