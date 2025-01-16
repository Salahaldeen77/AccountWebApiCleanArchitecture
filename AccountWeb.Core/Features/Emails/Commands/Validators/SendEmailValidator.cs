using AccountWeb.Core.Features.Emails.Commands.Models;
using AccountWeb.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Emails.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _Localizer;
        #endregion

        #region Constructors
        public SendEmailValidator(IStringLocalizer<SharedResources> Localizer)
        {
            _Localizer = Localizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_Localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_Localizer[SharedResourcesKeys.Required])
                .EmailAddress().WithMessage(_Localizer[SharedResourcesKeys.IncorrectEmailFormate]);
        }
        #endregion
    }
}
