using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Core.Wrappers;
using AccountWeb.Data.Enums;
using MediatR;

namespace AccountWeb.Core.Features.Accounts.Queries.Models
{
    public class GetAccountPaginatedListQuery : IRequest<PaginatedResult<GetAccountPaginatedListListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public AccountOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
