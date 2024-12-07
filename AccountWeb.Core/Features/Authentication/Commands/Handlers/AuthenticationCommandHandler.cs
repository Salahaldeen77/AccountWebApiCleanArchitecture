using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authentication.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
        IRequestHandler<SignInCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        #endregion

        #region Constructors
        public AuthenticationCommandHandler(IStringLocalizer<SharedResources> stringlocalizer
                                            , UserManager<User> userManager
                                            , SignInManager<User> signInManager
                                            , IAuthenticationService authenticationService) : base(stringlocalizer)
        {
            _stringlocalizer = stringlocalizer;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }


        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<string>(_stringlocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            //Try To Sign In
            var signInResult = _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //If Failed Return Password is worng
            if (!signInResult.IsCompletedSuccessfully) return BadRequest<string>(_stringlocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            //Generate Token
            var accessToken = await _authenticationService.GetJWTToken(user);
            //return Token
            return Success(accessToken);
        }
        #endregion
    }
}
