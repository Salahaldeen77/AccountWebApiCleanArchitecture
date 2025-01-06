using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Authorization.Queries.Responses;
using MediatR;

namespace AccountWeb.Core.Features.Authorization.Queries.Models
{
    public class GetRoleByIdQuery : IRequest<Response<GetRoleByIdResponse>>
    {
        public int Id { get; set; }
    }
}
