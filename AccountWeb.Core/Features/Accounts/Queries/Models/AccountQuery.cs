using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Accounts.Queries.Responses;
using MediatR;

namespace AccountWeb.Core.Features.Accounts.Queries.Models
{
    public class GetAccountByIdQuery : IRequest<Response<GetSingleAccountResponse>>
    {
        public int Id { get; set; }
        public GetAccountByIdQuery(int id)
        {
            Id = id;
        }

    }
    public class GetAccountListResponseQuery : IRequest<Response<List<GetAccountListResponse>>>
    {
    }
}
