using AccountWeb.Core.Bases;
using MediatR;

namespace AccountWeb.Core.Features.Authentication.Commands.Models
{
    public class SendResetPasswordCommand : IRequest<Response<string>>
    {
        public string Email { get; set; }
    }
}
