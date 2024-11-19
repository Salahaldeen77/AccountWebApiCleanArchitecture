using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Accounts.Commands.Validators
{
    public class DeleteAccountValidator : AbstractValidator<DeleteAccountCommand>
    {
        #region Fields
        private readonly IAccountService _accountService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public DeleteAccountValidator(IAccountService accountService, IStringLocalizer<SharedResources> localizer)
        {
            _accountService = accountService;
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (Key, CancellationToken) => !await _accountService.IsAccountHaveTransactionByIdAsync(Key))
                .WithMessage("You Can't delete the Account he has Trnasaction ^_^");
        }
        #endregion
    }
}
