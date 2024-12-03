using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Users.Commands.Validators
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public ChangeUserPasswordValidator(IStringLocalizer<SharedResources> localizer, UserManager<User> userManager)
        {
            _localizer = localizer;
            _userManager = userManager;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);


            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Password is Not Equal Confirm Password!!");


        }
        public void ApplyCustomValidationsRules()
        {
        }
        #endregion
    }
}
