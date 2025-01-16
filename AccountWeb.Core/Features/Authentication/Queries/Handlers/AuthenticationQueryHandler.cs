using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authentication.Queries.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : ResponseHandler,
                                              IRequestHandler<AuthorizeUserQuery, Response<string>>,
                                              IRequestHandler<ConfirmEmailQuery, Response<string>>,
                                              IRequestHandler<ConfirmResetPasswordQuery, Response<string>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        public AuthenticationQueryHandler(IAuthenticationService auhenticationServer, IStringLocalizer<SharedResources> stringlocalizer) : base(stringlocalizer)
        {
            _authenticationService = auhenticationServer;
            _stringlocalizer = stringlocalizer;
        }
        public async Task<Response<string>> Handle(AuthorizeUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.ValidateToken(request.AccessToken);
            if (response == "NotExpired") return Success(response);

            return Unauthorized<string>(_stringlocalizer[SharedResourcesKeys.TokenIsExpired]);

        }

        public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var confirmEmailResult = await _authenticationService.ConfirmEmail(request.UserId, request.Code);
            if (confirmEmailResult == "Success") return Success("Confirm email is successful");
            else
                return BadRequest<string>(confirmEmailResult);
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmResetPassword(request.Code, request.Email);
            if (result == "Success")
                return Success("");
            else
                return BadRequest<string>(result);
        }
    }
}
