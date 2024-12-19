using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authentication.Queries.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<AuthorizeUserQuery, Response<string>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        public AuthenticationQueryHandler(IAuthenticationService auhenticationServer, IStringLocalizer<SharedResources> stringlocalizer) : base(stringlocalizer)
        {
            _authenticationService = auhenticationServer;
        }
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ValidateToken(request.AccessToken);
            if (response == "NotExpired") return Success(response);

            return Unauthorized<string>(_stringlocalizer[SharedResourcesKeys.TokenIsExpired]);

        }
    }
}
