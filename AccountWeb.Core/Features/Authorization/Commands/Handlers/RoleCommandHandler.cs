using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authorization.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
                                      IRequestHandler<AddRoleCommand, Response<string>>,
                                      IRequestHandler<EditRoleCommand, Response<string>>,
                                      IRequestHandler<DeleteRoleCommand, Response<string>>,
                                      IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public RoleCommandHandler(IStringLocalizer<SharedResources> stringlocalizer,
                                  IAuthorizationService authorizationService) : base(stringlocalizer)
        {
            _stringlocalizer = stringlocalizer;
            _authorizationService = authorizationService;
        }


        #endregion
        #region Handel Functions
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success") return Success("");
            return BadRequest<string>(_stringlocalizer[SharedResourcesKeys.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "NameIsExist") return BadRequest<string>("Name Exist To Another Role");
            else if (result == "Success") return Success((string)_stringlocalizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>("Role is already in use");
            else if (result == "Success") return Success((string)_stringlocalizer[SharedResourcesKeys.Deleted]);
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserRolesAsync(request);
            if (result == "NotFound") return NotFound<string>("User Is NotFound");
            else if (result == "Success") return Success("Updated Roles Successfully");
            else return BadRequest<string>(result);
        }
        #endregion

    }
}
