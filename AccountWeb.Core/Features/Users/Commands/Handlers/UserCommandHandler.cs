using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
                                                     , IRequestHandler<EditeUserCommand, Response<string>>
                                                     , IRequestHandler<DeleteUserCommand, Response<string>>
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
        public async Task<Response<string>> Handle(EditeUserCommand request, CancellationToken cancellationToken)
        {
            //check user exist
            var oldUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (oldUser == null) return NotFound<string>($"The UserId:{request.Id} is not found ");

            //The Map in Edite Should be like this code
            var newUser = _mapper.Map(request, oldUser);

            //Update
            var result = await _userManager.UpdateAsync(newUser);
            //check Success Updated
            if (!result.Succeeded) return BadRequest<string>($"Failed,error update UserId:{newUser.Id}");

            return Success($"Updated Successfuly UserId:{newUser.Id}");

        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check User is Exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null) return NotFound<string>($"The UserId:{request.Id} is not found ");
            //Delete User
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest<string>($"Failed,error Delete UserId:{user.Id}");

            return Success($"Deleted Successfuly UserId:{user.Id}");

        }
        #endregion


    }
}
