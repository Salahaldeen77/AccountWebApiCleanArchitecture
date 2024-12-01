using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Users.Queries.Responses;
using MediatR;

namespace AccountWeb.Core.Features.Users.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }

        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
