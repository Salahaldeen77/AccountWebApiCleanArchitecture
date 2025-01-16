using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Emails.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Emails.Commands.Handlers
{
    public class EmailsCommandHandler : ResponseHandler,
                                      IRequestHandler<SendEmailCommand, Response<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _stringlocalizer;
        private readonly IEmailsService _emailsService;
        #endregion

        #region Constructors
        public EmailsCommandHandler(IStringLocalizer<SharedResources> stringlocalizer,
                                    IEmailsService emailsService) : base(stringlocalizer)
        {
            _stringlocalizer = stringlocalizer;
            _emailsService = emailsService;
        }
        #endregion
        #region Handel Functions
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailsService.SendEmail(request.Email, request.Message, null);
            if (result == "Success") return Success("Send message Successfully");
            else return BadRequest<string>(Meta: "Operation to send email failed " + result);
        }
        #endregion
    }
}
