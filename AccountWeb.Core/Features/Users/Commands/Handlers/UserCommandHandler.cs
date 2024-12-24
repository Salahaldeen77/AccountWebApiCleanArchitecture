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
                                                     , IRequestHandler<ChangeUserPasswordCommand, Response<string>>
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

            //Adding Role User
            await _userManager.AddToRoleAsync(identityUser, "User");

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
            //check if UserName is Exist and Except oldUser id. 
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);
            if (userByUserName != null) return BadRequest<string>("Failed, UserName is Exist");
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
            if (!result.Succeeded) return BadRequest<string>($"Failed,error Delete UserId:{user.Id}", result.Errors.FirstOrDefault().Description);

            return Success($"Deleted Successfuly UserId:{user.Id}");

        }

        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //check User is Exist
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null) return NotFound<string>($"The UserId:{request.Id} is not found ");
            //change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //return result
            if (!result.Succeeded) return BadRequest<string>($"Failed,error Change Password", result.Errors.FirstOrDefault().Description);
            return Success($"Change Password is Successfuly ");


        }
        #endregion


    }
}
