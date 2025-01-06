using AccountWeb.Core.Bases;
using AccountWeb.Data.Requests;
using MediatR;

namespace AccountWeb.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserRolesCommand : UpdateUserRolesRequest, IRequest<Response<string>>
    {
    }
}
