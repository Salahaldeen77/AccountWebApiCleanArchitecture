using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authorization.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler,
                                      IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly IAuthorizationService _authorizationService;
        #endregion

        #region Constructors
        public ClaimsCommandHandler(IStringLocalizer<SharedResources> stringlocalizer,
                                  IAuthorizationService authorizationService) : base(stringlocalizer)
        {
            _stringlocalizer = stringlocalizer;
            _authorizationService = authorizationService;
        }
        #endregion
        #region Handel Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaimsAsync(request);
            if (result == "NotFound") return NotFound<string>("User Is NotFound");
            else if (result == "Success") return Success("Updated Claims Successfully");
            else return BadRequest<string>(result);
        }
        #endregion

    }
}