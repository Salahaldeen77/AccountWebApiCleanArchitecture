﻿using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authentication.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Responses;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler,
                                                IRequestHandler<SignInCommand, Response<JwtAuthResult>>,
                                                IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>,
                                                IRequestHandler<SendResetPasswordCommand, Response<string>>,
                                                IRequestHandler<ResetPasswordCommand, Response<string>>
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
        public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is exist or not
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null) return BadRequest<JwtAuthResult>(_stringlocalizer[SharedResourcesKeys.UserNameIsNotExist]);
            //Try To Sign In
            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            //If Failed Return Password is worng
            if (!signInResult.Succeeded) return BadRequest<JwtAuthResult>(_stringlocalizer[SharedResourcesKeys.PasswordNotCorrect]);
            //check confirm email
            if (!user.EmailConfirmed) return BadRequest<JwtAuthResult>(_stringlocalizer[SharedResourcesKeys.EmailNotConfirmed]);

            //Generate Token
            var result = await _authenticationService.GetJWTToken(user);
            //return Token
            return Success(result);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //#Read Token to get Claims
            var jwtToken = _authenticationService.ReadJWTToken(request.AccessToken); //Decode accessToken
            //#Validate Token And Get UserId
            var (valide, ExpireDateRefreshToken) = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);

            if (valide != "Success") return Unauthorized<JwtAuthResult>(_stringlocalizer[valide]);
            //get user
            var userId = _authenticationService.GetUserId(jwtToken);
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return NotFound<JwtAuthResult>();


            var result = await _authenticationService.GetRefreshAccessToken(user, request.RefreshToken, (DateTime)ExpireDateRefreshToken);
            return Success(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            if (result == "Success")
                return Success("Send code is Successful");

            return BadRequest<string>(result);


        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Email, request.Password);
            if (result == "Success")
                return Success("Reset Password is Successful");

            return BadRequest<string>(result);

        }
        #endregion
    }
}
