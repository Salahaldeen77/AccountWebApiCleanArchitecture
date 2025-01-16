using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AccountWeb.Service.Abstracts;
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
        private readonly IUserService _userService;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager,
                                  IUserService userService) : base(stringLocalizer)
        {
            _sharedResources = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //Mapping 
            var identityUser = _mapper.Map<User>(request);
            //Create 
            var CreateResult = await _userService.AddUserAsync(identityUser, request.Password);
            //Failed
            if (CreateResult != "Success")
                return BadRequest<string>(Meta: CreateResult);

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
