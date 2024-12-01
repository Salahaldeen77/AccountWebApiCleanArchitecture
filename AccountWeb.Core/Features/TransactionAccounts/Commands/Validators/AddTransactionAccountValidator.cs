using AccountWeb.Core.Features.TransactionAccounts.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.TransactionAccounts.Commands.Validators
{
    public class AddTransactionAccountValidator : AbstractValidator<AddTransactionAccountCommand>
    {
        #region Fields
        private readonly ITransactionAccountService _transactionAccountService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddTransactionAccountValidator(ITransactionAccountService transactionAccountService, IStringLocalizer<SharedResources> localizer)
        {
            _transactionAccountService = transactionAccountService;
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();

        }
        #endregion
        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.TransactionId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
            RuleFor(x => x.Amount)
                //.WithMessage("Amount must not be Less than 0")
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);
        }
        public void ApplyCustomValidationsRules()
        {

            RuleFor(x => x.AccountId)
                .MustAsync(async (Key, CancellationToken) => await _transactionAccountService.IsAccountExistByIdAsync(Key))
                .WithMessage($"The Account is Not Found ^_^");
            RuleFor(x => x.TransactionId)
                .MustAsync(async (Key, CancellationToken) => await _transactionAccountService.IsTransactionExistByIdAsync(Key))
                .WithMessage($"The TransactionId is Not Found ^_^");

            //check if AccountId == TransferredToAccountId return exception
            When(x => x.TransferredToAccountId == x.AccountId, () =>
            {
                RuleFor(x => x.TransferredToAccountId)
                   .Must((Key) => false).WithMessage("Must be different AccountId And TransferredToAccountId");
            });
        }
        #endregion

    }

}
