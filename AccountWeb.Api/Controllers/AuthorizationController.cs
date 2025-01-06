using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Authorization.Commands.Models;
using AccountWeb.Core.Features.Authorization.Queries.Models;
using AccountWeb.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AccountWeb.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost(Router.AuthorizationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpPut(Router.AuthorizationRouting.Edit)]
        public async Task<IActionResult> Edit([FromForm] EditRoleCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpDelete(Router.AuthorizationRouting.Delete)]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return NewResult(response);
        }
        [HttpGet(Router.AuthorizationRouting.RoleList)]
        public async Task<IActionResult> GetRoleList()
        {
            var response = await Mediator.Send(new GetRolesListQuery());
            return Ok(response);
        }

        [SwaggerOperation(Summary = "Idجلب الصلاحية عن طريق ال", OperationId = "RoleById")]
        [HttpGet(Router.AuthorizationRouting.GetById)]
        public async Task<IActionResult> GetRoleById([FromRoute] int Id)
        {
            var response = await Mediator.Send(new GetRoleByIdQuery() { Id = Id });
            return Ok(response);
        }

        [SwaggerOperation(Summary = "إدارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
        [HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
        public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
        {
            var response = await Mediator.Send(new ManageUserRolesQuery() { UserId = userId });
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "تعديل صلاحيات المستخدمين", OperationId = "UpdateUserRoles")]
        [HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "إدارة صلاحيات الإستخدام", OperationId = "ManageUserClaims")]
        [HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
        public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
        {
            var response = await Mediator.Send(new ManageUserClaimsQuery() { UserId = userId });
            return NewResult(response);
        }

        [SwaggerOperation(Summary = "تعديل صلاحيات الإستخدام للمستخدمين", OperationId = "UpdateUserClaims")]
        [HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
        public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
