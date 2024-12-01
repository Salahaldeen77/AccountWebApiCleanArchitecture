using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Users.Commands.Validatiors
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddUserValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
            // .Matches(@"gmail.com").WithMessage("The Email no true");


            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .MaximumLength(200).WithMessage("Maxmum Length is 200")
                .MinimumLength(5).WithMessage("Name Minmum Length is 5 +_+");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .MaximumLength(200).WithMessage("Maxmum Length is 200")
                .MinimumLength(3).WithMessage("Name Minmum Length is 3 +_+");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .MaximumLength(200).WithMessage("Maxmum Length is 200")
                .MinimumLength(3).WithMessage(" Minmum Length is 3 +_+");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Password is Not Equal Confirm Password!!");

        }
        public void ApplyCustomValidationsRules()
        {

        }
        #endregion
    }
}
