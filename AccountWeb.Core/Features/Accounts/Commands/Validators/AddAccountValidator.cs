﻿using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Accounts.Commands.Validators
{
    public class AddAccountValidator : AbstractValidator<AddAccountCommand>
    {
        #region Fields
        private readonly IAccountService _accountService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddAccountValidator(IAccountService accountService, IStringLocalizer<SharedResources> localizer)
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
            RuleFor(x => x.AccountNumber)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .LessThan(9999999).WithMessage(" Max Length is 7 +_+");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull])
                .MaximumLength(200).WithMessage("Maxmum Length is 200")
                .MinimumLength(3).WithMessage("Name Minmum Length is 3 +_+");

            RuleFor(x => x.OpeningBalance)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.NotNull]);

        }
        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.AccountNumber)
                .MustAsync(async (Key, CancellationToken) => !await _accountService.IsAccountNumberExist(Key))
                .WithMessage(" is Exist choose another AccountNumber #_#");
        }
        #endregion

    }
}
