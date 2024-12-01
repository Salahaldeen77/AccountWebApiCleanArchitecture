using AccountWeb.Core.Features.Users.Queries.Responses;
using AccountWeb.Core.Wrappers;
using MediatR;

namespace AccountWeb.Core.Features.Users.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<PaginatedResult<GetUserPaginationResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
