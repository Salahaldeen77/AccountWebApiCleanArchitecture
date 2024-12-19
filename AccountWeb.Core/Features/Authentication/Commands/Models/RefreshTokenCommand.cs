using AccountWeb.Core.Bases;
using AccountWeb.Data.Helpers;
using MediatR;

namespace AccountWeb.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthResult>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
