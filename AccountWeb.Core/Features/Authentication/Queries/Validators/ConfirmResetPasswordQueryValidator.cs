using AccountWeb.Core.Features.Authentication.Queries.Models;
using AccountWeb.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Authentication.Queries.Validators
{
    public class ConfirmResetPasswordQueryValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public ConfirmResetPasswordQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .EmailAddress().WithMessage(_localizer[SharedResourcesKeys.IncorrectEmailFormate]);
        }
        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}
