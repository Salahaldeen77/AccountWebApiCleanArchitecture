using AccountWeb.Core.Bases;
using AccountWeb.Data.Responses;
using MediatR;

namespace AccountWeb.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthResult>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
