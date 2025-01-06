using AccountWeb.Core.Bases;
using AccountWeb.Data.Responses;
using MediatR;

namespace AccountWeb.Core.Features.Authorization.Queries.Models
{
    public class ManageUserClaimsQuery : IRequest<Response<ManageUserClaimsResponse>>
    {
        public int UserId { get; set; }
    }
}
