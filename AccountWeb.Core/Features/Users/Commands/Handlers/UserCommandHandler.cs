using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _sharedResources = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }

        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if email exist
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null) return BadRequest<string>("Failed, Email is Exist");
            //if UserName is Exist
            var userByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userByUserName != null) return BadRequest<string>("Failed, UserName is Exist");

            //Mapping 
            var identityUser = _mapper.Map<User>(request);

            //Create 
            var CreateResult = await _userManager.CreateAsync(identityUser, request.Password);
            //Failed
            if (!CreateResult.Succeeded) return BadRequest<string>("Failed to add user", CreateResult.Errors.FirstOrDefault().Description);

            //Success
            return Created("");

        }
        #endregion


    }
}
