using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authorization.Queries.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Responses;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
    {
        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringlocalizer,
                                  IAuthorizationService authorizationService,
                                  UserManager<User> userManager) : base(stringlocalizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
        #endregion
        #region Handel Functions
        public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserClaimsResponse>();
            var result = await _authorizationService.ManageUserClaimsDataAsync(user);
            return Success(result);
        }
        #endregion

    }
}