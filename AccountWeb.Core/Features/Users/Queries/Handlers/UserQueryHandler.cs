using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Users.Queries.Models;
using AccountWeb.Core.Features.Users.Queries.Responses;
using AccountWeb.Core.Resources;
using AccountWeb.Core.Wrappers;
using AccountWeb.Data.Entities.Identity;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Users.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler, IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationResponse>>
                                                   , IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;

        #endregion


        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                            IMapper mapper,
                            UserManager<User> userManager) : base(stringLocalizer)
        {
            _sharedResources = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public async Task<PaginatedResult<GetUserPaginationResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedResult = await _mapper.ProjectTo<GetUserPaginationResponse>(users)
                                             .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedResult;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.FindByIdAsync(request.Id.ToString());
            var user = _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null) return NotFound<GetUserByIdResponse>("");

            var userMapp = _mapper.Map<GetUserByIdResponse>(user);
            return Success(userMapp);
        }
        #endregion


    }
}
