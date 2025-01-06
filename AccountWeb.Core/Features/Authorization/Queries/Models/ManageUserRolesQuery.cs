using AccountWeb.Core.Bases;
using AccountWeb.Data.Responses;
using MediatR;

namespace AccountWeb.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRolesQuery : IRequest<Response<ManageUserRolesResponse>>
    {
        public int UserId { get; set; }
    }
}
