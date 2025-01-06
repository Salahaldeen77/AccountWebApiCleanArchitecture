using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authorization.Queries.Models;
using AccountWeb.Core.Features.Authorization.Queries.Responses;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using AccountWeb.Data.Responses;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResponse>>>,
                                                    IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
                                                    IRequestHandler<ManageUserRolesQuery, Response<ManageUserRolesResponse>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public RoleQueryHandler(IStringLocalizer<SharedResources> stringlocalizer,
                                  IAuthorizationService authorizationService,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringlocalizer)
        {
            _stringlocalizer = stringlocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _userManager = userManager;
        }




        #endregion
        #region Handel Functions
        public async Task<Response<List<GetRolesListResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesListAsync();
            var result = _mapper.Map<List<GetRolesListResponse>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null) return NotFound<GetRoleByIdResponse>("Role Is Not Exist");
            var result = _mapper.Map<GetRoleByIdResponse>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResponse>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRolesResponse>();
            var result = await _authorizationService.ManageUserRolesDataAsync(user);
            return Success(result);
        }
        #endregion

    }
}