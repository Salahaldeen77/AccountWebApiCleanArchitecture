using AccountWeb.Core.Features.Authentication.Commands.Models;
using AccountWeb.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public SignInValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidationsRules()
        {

        }
        #endregion

    }
}
