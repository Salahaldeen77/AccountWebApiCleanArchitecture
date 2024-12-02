using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Users.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<EditeUserCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UpdateUserValidator(IStringLocalizer<SharedResources> localizer, UserManager<User> userManager)
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

        }
        public void ApplyCustomValidationsRules()
        {
            //RuleFor(x=> x.Email)
            //    .MustAsync(async (model, Key, CancellationToken) => !await _userManager.IsEmailConfirmedAsync)
            //    .WithMessage("AccountNumber is Exist choose another AccountNumber #_#");
        }
        #endregion
    }
}
