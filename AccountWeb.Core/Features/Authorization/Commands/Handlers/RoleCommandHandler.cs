using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authorization.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler,
                                                IRequestHandler<AddRoleCommand, Response<string>>
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
        #endregion

    }
}
